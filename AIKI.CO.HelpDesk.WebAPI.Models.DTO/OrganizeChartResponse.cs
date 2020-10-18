using System;
using System.ComponentModel.DataAnnotations;

namespace AIKI.CO.HelpDesk.WebAPI.Models.DTO
{
    public class OrganizeChartResponse : BaseResponse
    {
        public Guid? parent_id { get; set; }

        [Required] public string title { get; set; }

        [Required] public Guid? titletype { get; set; }

        public OrganizeChartAdditionalInfo[] additionalinfo { get; set; }
        public string email { get; set; }
        public Guid customerid { get; set; }
    }
}