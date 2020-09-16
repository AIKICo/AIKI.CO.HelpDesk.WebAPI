using System.Threading.Tasks;
using CloudFlare.Client.Api.Result;

namespace AIKI.CO.HelpDesk.WebAPI.Services.Interface
{
    public interface ICloudFlareService
    {
        Task<bool> AddDnsRecord(string subDoamin, string IpAddress);
    }
}