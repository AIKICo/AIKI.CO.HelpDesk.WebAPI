namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public sealed class Customer : BaseObject
    {
        public string title { get; set; }
        public string description { get; set; }
        public string domains { get; set; }
        public byte?[] schema { get; set; }
        public bool? disabled { get; set; }

        public Company Company { get; set; }
    }
}
