using System;
using System.ComponentModel.DataAnnotations;

namespace AIKI.CO.HelpDesk.WebAPI.Models.DTO
{
    public class AppConstantItemResponse : BaseResponse
    {
        public Guid appconstantid { get; set; }

        [Required] public string value1 { get; set; }

        public string value2 { get; set; }
        public AdditionalInfo[] additionalinfo { get; set; }
    }
}