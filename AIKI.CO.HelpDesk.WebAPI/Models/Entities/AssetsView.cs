using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class AssetsView : BaseObject
    {
        public string assetnumber { get; set; }
        public string title { get; set; }
        public string assetlocationid { get; set; }
        public string assettypeid { get; set; }
        public DateTime? deliverydate { get; set; }
        public Guid? customerid { get; set; }
    }
}