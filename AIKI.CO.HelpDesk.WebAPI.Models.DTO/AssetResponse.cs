using System;
using System.ComponentModel.DataAnnotations;

namespace AIKI.CO.HelpDesk.WebAPI.Models.DTO
{
    public class AssetResponse : BaseResponse
    {
        public Guid? employeeid { get; set; }
        public Guid? assetlocationid { get; set; }
        public Guid? assettypeid { get; set; }

        [Required] public string assetnumber { get; set; }

        public DateTime? deliverydate { get; set; }
        public Guid? customerid { get; set; }
        public string employees { get; set; }
        public AssetAdditionalInfo[] assetadditionalinfo { get; set; }
    }
}