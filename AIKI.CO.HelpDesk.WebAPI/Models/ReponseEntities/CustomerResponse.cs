﻿namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public sealed class CustomerResponse : BaseResponse
    {
        public string title { get; set; }
        public string description { get; set; }
        public string domains { get; set; }
        public byte?[] schema { get; set; }
        public bool? disabled { get; set; }
    }
}
