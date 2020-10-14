using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace AIKI.CO.HelpDesk.WebAPI.IntegrationTests
{
    public class CustomWebApplicationFactory:WebApplicationFactory<Program>
    {
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(Services =>
            {
                Services.RemoveAll(typeof(IHostedService));
            });
        }
    }
}