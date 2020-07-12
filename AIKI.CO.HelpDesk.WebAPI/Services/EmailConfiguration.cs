using AIKI.CO.HelpDesk.WebAPI.Services.Interface;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public class EmailConfiguration:IEmailConfiguration
    {
        public string SmtpServer { get; set; }
        public int SmtpPort  { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}