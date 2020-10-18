using System.ComponentModel.DataAnnotations;
namespace AIKI.CO.HelpDesk.WebAPI.Models.DTO
{
    public class OperatingHoureResponse : BaseResponse
    {
        [Required] public string title { get; set; }

        public string timezone { get; set; }
        public Workday[] workdays { get; set; }
        public Holiday[] holidays { get; set; }
        public bool? isdefault { get; set; }
    }
}