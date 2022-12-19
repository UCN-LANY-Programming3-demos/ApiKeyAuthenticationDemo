using Microsoft.AspNetCore.Mvc.Filters;

namespace KeyAuthenticationWithAttribute.Filters
{
    public class NoApiKeyAuthenticateAttribute : ApiKeyAuthenticateAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
        }
    }
}
