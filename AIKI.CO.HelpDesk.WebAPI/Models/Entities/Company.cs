using System;
using System.Collections.Generic;
// ReSharper disable All

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public sealed class Company
    {
        public Company()
        {
            Customers = new HashSet<Customer>();
            Members = new HashSet<Member>();
            OperatingsHour = new HashSet<OperatingHour>();
            SLASettings = new HashSet<SLASetting>();
            Groups = new HashSet<Group>();
            AppConstants = new HashSet<AppConstant>();
            AppConstantItems = new HashSet<AppConstantItem>();
            OrganizeCharts = new HashSet<OrganizeChart>();
            Assets = new HashSet<Asset>();
            Tickets = new HashSet<Ticket>();
            TicketHistory = new List<TicketHistory>();
            ProfilePictures = new HashSet<ProfilePicture>();
        }

        public Guid id { get; set; }
        public string title { get; set; }
        public string email { get; set; }
        public bool? allowdelete { get; set; }
        public string subdomain { get; set; }    

        public ICollection<Customer> Customers { get; set; }
        public ICollection<Member> Members { get; set; }
        public ICollection<OperatingHour> OperatingsHour { get; set; }
        public ICollection<SLASetting> SLASettings { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<AppConstant> AppConstants { get; set; }
        public ICollection<AppConstantItem> AppConstantItems { get; set; }
        public ICollection<OrganizeChart> OrganizeCharts { get; set; }
        public ICollection<Asset> Assets { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<TicketHistory> TicketHistory { get; set; }
        public ICollection<ProfilePicture> ProfilePictures { get; set; }
    }
}