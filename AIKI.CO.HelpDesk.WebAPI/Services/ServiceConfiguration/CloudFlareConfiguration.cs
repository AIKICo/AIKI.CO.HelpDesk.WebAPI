using AIKI.CO.HelpDesk.WebAPI.Services.Interface;

namespace AIKI.CO.HelpDesk.WebAPI.Services.ServiceConfiguration
{
    public class CloudFlareConfiguration : ICloudFlareConfiguration
    {
        public string EmailAddress { get; set; }
        public string ApiKey { get; set; }
        public string CloudFlareZoneId { get; set; }
        public string Domain { get; set; }
        public string IPAddress1 { get; set; }
        public string IPAddress2 { get; set; }
    }
}