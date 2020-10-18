using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.DTO
{
    public class OrganizeChartViewResponse : BaseResponse
    {
        public Guid customerid { get; set; }
        public Guid? parent_id { get; set; }
        public string title { get; set; }
        public Guid? titletype { get; set; }
        public string children { get; set; }
    }
}