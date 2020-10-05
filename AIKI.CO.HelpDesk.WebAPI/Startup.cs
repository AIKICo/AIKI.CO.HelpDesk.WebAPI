using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Localization.Routing;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Localization;
using AIKI.CO.HelpDesk.WebAPI.AutoMapperSettings;
using AIKI.CO.HelpDesk.WebAPI.BuilderExtensions;
using AIKI.CO.HelpDesk.WebAPI.Extensions;
using AIKI.CO.HelpDesk.WebAPI.HubController;
using AIKI.CO.HelpDesk.WebAPI.Models;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Raven.Client.Documents;
using Raven.Client.Http;
using Serilog;
using Serilog.Context;
using Serilog.Debugging;

namespace AIKI.CO.HelpDesk.WebAPI
{
    public sealed class Startup
    {
        private static X509Certificate2 _logServerCertificate;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

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
                            "https://aiki-ticket.ir",
                            "https://www.aiki-ticket.ir",
                            "http://localhost:5003")
                        .AllowCredentials();
                });
            });

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(@"DataProtectionKeys/"))
                .SetApplicationName("AIKI.CO.HelpDesk")
                .SetDefaultKeyLifetime(TimeSpan.FromDays(14));

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
            services.AddSignalR();
            services.AddMemoryCache();

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("fa-IR")
                    };
                    options.DefaultRequestCulture = new RequestCulture(culture: "fa-IR", uiCulture: "fa-IR");
                    options.SupportedCultures = supportCultures;
                    options.SupportedUICultures = supportCultures;
                    options.RequestCultureProviders = new[]
                    {
                        new AIKI.CO.HelpDesk.WebAPI.Extensions.RouteDataRequestCultureProvider
                            {IndexOfCulture = 1, IndexofUICulture = 1}
                    };
                }
            );
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("culture", typeof(LanguageRouteConstraint));
            });
            services.AddControllers()
                .AddNewtonsoftJson(x =>
                    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "AIKI Help Desk API");
                    options.RoutePrefix = string.Empty;
                    SelfLog.Enable(msg => Debug.WriteLine(msg));
                    SelfLog.Enable(Console.Error);
                });
            }
            else
            {
                app.UseHsts();
            }
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
            app.UseHttpsRedirection();
            
            app.UseResponseCompression();
            app.UseResponseCaching();
            app.UseCors();
            var localizeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizeOptions.Value);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSerilogRequestLogging();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{culture:culture}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
                endpoints.MapHub<TicketAlarmHub>("/ticketalarmhub", options => { });
            });
        }

        private static IDocumentStore CreateRavenDocStore(IWebHostEnvironment env)
        {
            RequestExecutor.RemoteCertificateValidationCallback += CertificateCallback;
            if (env.IsDevelopment())
                _logServerCertificate =
                    new X509Certificate2($"{Directory.GetCurrentDirectory()}/certificate/HelpDeskLog.pfx",
                        "Mveyma6303$");
            else
                _logServerCertificate =
                    new X509Certificate2($"{Directory.GetCurrentDirectory()}/certificate/HelpDeskLog.pfx",
                        "Mveyma6303$");
            var docStore = new DocumentStore
            {
                Urls = new[] {"https://a.free.aiki.ravendb.cloud"},
                Database = "HelpDeskLog",
                Certificate = _logServerCertificate
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