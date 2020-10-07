using System;

namespace AIKI.CO.HelpDesk.WebAPI.Settings
{
    public sealed class AppSettings
    {
        public string Secret { get; set; }
        public Guid CompanyID { get; set; } 
    }
}