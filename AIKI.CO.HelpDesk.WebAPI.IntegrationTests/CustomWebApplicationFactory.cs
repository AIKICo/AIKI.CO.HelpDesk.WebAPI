using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace AIKI.CO.HelpDesk.WebAPI.IntegrationTests
{
    public class CustomWebApplicationFactory:WebApplicationFactory<Program>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            var env = new LaunchSettingsFixture();
            var builder = base.CreateHostBuilder();
            builder.ConfigureLogging(loggingBuilder =>
            {

            });
            return builder;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(IHostedService));
            });
        }
    }
}