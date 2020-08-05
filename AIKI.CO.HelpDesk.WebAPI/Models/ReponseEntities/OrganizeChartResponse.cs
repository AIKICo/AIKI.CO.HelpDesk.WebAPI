using System;
using System.ComponentModel.DataAnnotations;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class OrganizeChartResponse : BaseResponse
    {
        public Guid? parent_id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string titletype { get; set; }
        public OrganizeChartAdditionalInfo[] additionalinfo { get; set; }
        public string email { get; set; }
        public Guid customerid { get; set; }
    }
}