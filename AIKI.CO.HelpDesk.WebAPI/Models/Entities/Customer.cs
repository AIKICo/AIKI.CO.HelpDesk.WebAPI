using System;
using System.Collections.Generic;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public partial class Customer:BaseObject
    {
        public string title { get; set; }
        public string description { get; set; }
        public string domains { get; set; }
        public byte[] schema { get; set; }
        public bool? disabled { get; set; }

        public virtual Company Company { get; set; }
    }
}
