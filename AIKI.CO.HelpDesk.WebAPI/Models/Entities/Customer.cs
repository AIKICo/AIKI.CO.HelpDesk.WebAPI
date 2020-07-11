﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public sealed class Customer : BaseObject
    {
        public Customer()
        {
            OrganizeCharts = new HashSet<OrganizeChart>();   
        }
        public string title { get; set; }
        public string description { get; set; }
        public string domains { get; set; }
        public byte?[] schema { get; set; }
        public bool? disabled { get; set; }
        public Guid? operatinghourid { get; set; }

        public Company Company { get; set; }
        public ICollection<OrganizeChart> OrganizeCharts { get; set; }
    }
}