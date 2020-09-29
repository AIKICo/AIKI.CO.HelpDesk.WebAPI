using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AIKI.CO.HelpDesk.WebAPI.CustomActionFilters
{
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new
                    {model = context.ModelState, message = "خطا در تغییر اطلاعات"});
            }
        }
    }
}