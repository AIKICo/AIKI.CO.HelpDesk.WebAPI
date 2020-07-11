using System;

// ReSharper disable All

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class BaseObject
    {
        public Guid id { get; set; }
        public Guid? companyid { get; set; }
        public bool? allowdelete { get; set; }
    }
}