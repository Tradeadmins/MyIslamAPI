using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MyIslamWebApp.Filter
{
    /// <summary>
    /// This is a custom action filter created to validate the inputs coming from users.
    /// Write Required attributes on properties of Models that are parameters of Controller methods and 
    /// this filter will check whether the models are valid or not.
    /// </summary>
    public class ModelValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            base.OnActionExecuting(actionContext);
        }
    }
}