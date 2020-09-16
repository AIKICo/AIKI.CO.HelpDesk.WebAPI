using System.Collections.Generic;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class AppConstant : BaseObject
    {
        public AppConstant()
        {
            AppConstantItems = new HashSet<AppConstantItem>();
        }

        public string title { get; set; }
        public string name { get; set; }

        public Company Company { get; set; }
        public ICollection<AppConstantItem> AppConstantItems { get; set; }
    }
}