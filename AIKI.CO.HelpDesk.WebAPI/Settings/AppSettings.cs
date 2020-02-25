using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Settings
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public Guid CompanyID { get; set; } = new Guid("997afb89-9abf-4889-8e43-cc301a311a9f");
    }
}
