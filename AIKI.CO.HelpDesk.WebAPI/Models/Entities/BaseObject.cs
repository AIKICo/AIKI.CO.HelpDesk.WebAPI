﻿using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class BaseObject
    {
        public Guid id { get; set; }
        public Guid? companyid { get; set; }
    }
}
