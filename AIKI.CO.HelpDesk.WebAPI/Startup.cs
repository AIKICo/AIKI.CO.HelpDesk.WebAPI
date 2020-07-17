using System;
using System.Collections.Generic;
using System.Diagnostics;
using AIKI.CO.HelpDesk.WebAPI.AutoMapperSettings;
using AIKI.CO.HelpDesk.WebAPI.BuilderExtensions;
using AIKI.CO.HelpDesk.WebAPI.Models;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System.IO.Compression;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using AIKI.CO.HelpDesk.WebAPI.HubController;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;
using Raven.Client.Http;
using Serilog;
using Serilog.Context;
using Serilog.Events;

namespace AIKI.CO.HelpDesk.WebAPI
{
    public sealed class Startup
    {
        private static X509Certificate2 logServerCertificate;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new PostgreSqlConnectionStringBuilder(Configuration["DATABASE_URL"])
            {
                Pooling = true,
                TrustServerCertificate = true,
                SslMode = SslMode.Require
            };

            services.AddDbContext<dbContext>(options => { options.UseNpgsql(builder.ConnectionString); })
                .AddUnitOfWork<dbContext>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins(
                            "https://aiki-helpdesk-v1.firebaseapp.com",
                            "https://localhost:5001",
                            "https://localhost:5002",
                            "http://localhost:5002",
                            "http://localhost:8080")
                        .AllowCredentials();
                });
            });

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(@"DataProtectionKeys/"))
                .SetApplicationName("AIKI.CO.HelpDesk")
                .SetDefaultKeyLifetime(TimeSpan.FromDays(14));
            ;
            services.AddResponseCaching();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(action =>
            {
                action.EnableForHttps = true;
                action.Providers.Add<BrotliCompressionProvider>();
                action.Providers.Add<GzipCompressionProvider>();
            });

            services.AddAutoMapper(typeof(HelpdeskMapper));
            services.RegisterServices(Configuration);

            services.AddAikiSwagger(Configuration);
            services.AddApiVersioning(config =>
            {
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            services.AddTokenAuthentication(Configuration);
            services.AddAuthorization();
            services.AddControllers()
                .AddNewtonsoftJson(x =>
                    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSignalR();
            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else app.UseHsts();

            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            Serilog.Debugging.SelfLog.Enable(Console.Error);

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.RavenDB(CreateRavenDocStore(env), errorExpiration: TimeSpan.FromDays(90))
                .CreateLogger();
            app.Use(async (httpContext, next) =>
            {
                var username = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : "anonymous";
                LogContext.PushProperty("User", username);
                var ip = httpContext.Connection.RemoteIpAddress.ToString();
                LogContext.PushProperty("IP", !string.IsNullOrWhiteSpace(ip) ? ip : "unknown");

                await next.Invoke();
            });

            loggerFactory.AddSerilog();
            Log.Information("Startup");

            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseResponseCaching();
            app.UseCors();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSerilogRequestLogging();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
                endpoints.MapHub<TicketAlarmHub>("/ticketalarmhub", options =>
                {
                });
            });
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "AIKI Help Desk API");
                options.RoutePrefix = string.Empty;
            });
        }

        private static IDocumentStore CreateRavenDocStore(IWebHostEnvironment env)
        {
            RequestExecutor.RemoteCertificateValidationCallback += CertificateCallback;
            if (env.IsDevelopment())
                logServerCertificate =
                    new X509Certificate2($"{Directory.GetCurrentDirectory()}/certificate/HelpDeskLog.pfx",
                        "Mveyma6303$");
            else
                logServerCertificate =
                    new X509Certificate2($"{Directory.GetCurrentDirectory()}/certificate/HelpDeskLog.pfx",
                        "Mveyma6303$");
            var docStore = new DocumentStore
            {
                Urls = new[] {"https://a.free.aiki.ravendb.cloud"},
                Database = "HelpDeskLog",
                Certificate = logServerCertificate
            };
            docStore.Initialize();
            return docStore;
        }

        private static bool CertificateCallback(object sender, X509Certificate cert, X509Chain chain,
            SslPolicyErrors errors)
        {
            return true;
        }
    }
}