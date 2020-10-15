using System;
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AIKI.CO.HelpDesk.WebAPI.IntegrationTests
{
    public static class TestsHttpClient
    {
        private static readonly Lazy<HttpClient> _serviceProviderBuilder =
            new Lazy<HttpClient>(getHttpClient, LazyThreadSafetyMode.ExecutionAndPublication);

        public static HttpClient Instane => _serviceProviderBuilder.Value;
        private static HttpClient getHttpClient()
        {
            var services = new CustomWebApplicationFactory();
            return services.CreateClient();
        }
    }
}