using System.Threading.Tasks;
using CloudFlare.Client.Api.Result;

namespace AIKI.CO.HelpDesk.WebAPI.Services.Interface
{
    public interface ICloudFlareService
    {
        Task<ResultInfo> AddDnsRecord(string subDoamin, string IpAddress);
    }
}