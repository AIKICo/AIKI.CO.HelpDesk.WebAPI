using System;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Http;
using Serilog;

namespace AIKI.CO.HelpDesk.WebAPI
{
    public sealed class Program
    {
        private static X509Certificate2 logServerCertificate;
        public static void Main(string[] args)
        {
            var log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.RavenDB(CreateRavenDocStore())
                .CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true);
                    configApp.AddEnvironmentVariables("ASPNETCORE_");
                    configApp.AddCommandLine(args);
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
        
        private static IDocumentStore CreateRavenDocStore()
        {
            RequestExecutor.RemoteCertificateValidationCallback += CertificateCallback;
            logServerCertificate = new X509Certificate2($"{Directory.GetCurrentDirectory()}/certificate/HelpDeskLog.pfx", "Mveyma6303$");
            var docStore = new DocumentStore
            {
                Urls = new[] { "https://a.free.aiki.ravendb.cloud" },
                Database = "HelpDeskLog",
                Certificate = logServerCertificate
            };
            docStore.Initialize();
            return docStore;
        }
        private static bool CertificateCallback(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
    }
}