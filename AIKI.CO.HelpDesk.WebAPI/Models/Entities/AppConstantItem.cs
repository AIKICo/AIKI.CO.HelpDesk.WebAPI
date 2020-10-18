using AIKI.CO.HelpDesk.WebAPI.Models.DTO;
using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class AppConstantItem : BaseObject
    {
        public Guid appconstantid { get; set; }
        public string value1 { get; set; }
        public string value2 { get; set; }
        public AdditionalInfo[] additionalinfo { get; set; }

        public Company Company { get; set; }
        public AppConstant AppConstant { get; set; }
    }
}