using System;
using System.ComponentModel.DataAnnotations;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class AppConstantItemResponse : BaseResponse
    {
        public Guid appconstantid { get; set; }

        [Required] public string value1 { get; set; }

        public string value2 { get; set; }
        public AdditionalInfo[] additionalinfo { get; set; }
    }
}