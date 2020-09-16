using System;
using System.Linq;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using CloudFlare.Client;
using CloudFlare.Client.Api.Result;
using CloudFlare.Client.Enumerators;

namespace AIKI.CO.HelpDesk.WebAPI.Services.ServiceConfiguration
{
    public class CloudFlareService : ICloudFlareService
    {
        private readonly ICloudFlareConfiguration _cloudFlareConfiguration;

        public CloudFlareService(ICloudFlareConfiguration cloudFlareConfiguration)
        {
            _cloudFlareConfiguration = cloudFlareConfiguration;
        }

        public async Task<bool> AddDnsRecord(string subDoamin, string IpAddress)
        {
            using var client =
                new CloudFlareClient(_cloudFlareConfiguration.EmailAddress,
                    Environment.GetEnvironmentVariable("CloudFlareApiKey"));
            var result = await client.CreateDnsRecordAsync(Environment.GetEnvironmentVariable("CloudFlareZoneId"),
                DnsRecordType.A,
                subDoamin, IpAddress, null, null, true);

            return result.Success;
        }
    }
}