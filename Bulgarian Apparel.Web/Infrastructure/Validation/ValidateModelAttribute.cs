using Bulgarian_Apparel.Common;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Bulgarian_Apparel.Web.Infrastructure.Validation
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.Any(p => p.Value == null))
            {
                actionContext.ModelState.AddModelError(string.Empty, GlobalConstants.RequestCannotBeEmpty);
            }

            if (!actionContext.ModelState.IsValid)
            {
                var error = actionContext
                    .ModelState
                    .Values
                    .SelectMany(v => v.Errors.Select(er => er.ErrorMessage))
                    .First();

                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, "error");
            }
        }
    }
}
