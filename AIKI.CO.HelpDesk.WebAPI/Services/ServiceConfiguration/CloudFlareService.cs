using System;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using CloudFlare.Client;
using CloudFlare.Client.Api.Result;
using CloudFlare.Client.Enumerators;
using Microsoft.Extensions.Hosting;

namespace AIKI.CO.HelpDesk.WebAPI.Services.ServiceConfiguration
{
    public class CloudFlareService : ICloudFlareService
    {
        private readonly CloudFlareConfiguration _cloudFlareConfiguration;

        public CloudFlareService(CloudFlareConfiguration cloudFlareConfiguration)
        {
            _cloudFlareConfiguration = cloudFlareConfiguration;
        }

        public async Task<ResultInfo> AddDnsRecord(string subDoamin, string IpAddress)
        {
            using CloudFlareClient client =
                new CloudFlareClient(_cloudFlareConfiguration.EmailAddress,
                    Environment.GetEnvironmentVariable("CloudFlareApiKey"));
            var result = await client.CreateDnsRecordAsync(Environment.GetEnvironmentVariable("CloudFlareZoneId"),
                DnsRecordType.A,
                subDoamin, IpAddress);

            if (result.Success) return result.ResultInfo;
            return null;
        }
    }
}