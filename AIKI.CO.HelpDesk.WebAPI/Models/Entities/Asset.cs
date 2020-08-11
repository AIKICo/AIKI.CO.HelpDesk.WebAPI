using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class Asset : BaseObject
    {
        public Guid? employeeid { get; set; }
        public Guid? assetlocationid { get; set; }
        public Guid? assettypeid { get; set; }
        public string assetnumber { get; set; }
        public Guid? customerid { get; set; }
        public DateTime? deliverydate { get; set; }
        public AssetAdditionalInfo[] assetadditionalinfo { get; set; }
        public Company Company { get; set; }
        public Customer Customer { get; set; }
    }
}