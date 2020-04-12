using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class OrganizeChartResponse:BaseResponse
    {
        public Guid? parent_id { get; set; }
        public string title { get; set; }
    }
}