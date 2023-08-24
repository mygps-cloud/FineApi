using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;



namespace Fine.Api.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!IsUserAuthorized(context.HttpContext.User))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
        private bool IsUserAuthorized(ClaimsPrincipal user)
        {
            return user.IsInRole("Technical Department Manager");
        }
    }
}
