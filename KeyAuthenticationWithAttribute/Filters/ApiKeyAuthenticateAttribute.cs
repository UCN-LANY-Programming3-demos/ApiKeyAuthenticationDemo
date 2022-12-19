using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KeyAuthenticationWithAttribute.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthenticateAttribute : ActionFilterAttribute
    {
        private const string API_KEY_NAME = "Client-Authentication-Key";
        private const string API_KEY_VALUE = "F147A2F0-9E7B-455B-BDBF-1BE554D95E73"; // In this example the key is hardcoded, but it should be stored in a file or a database. Also, each client should have its own unique key.

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // if NoApiKeyAuthentication then skip
            var skipAuthentication = context.ActionDescriptor.FilterDescriptors.Any(d => d.Filter is NoApiKeyAuthenticateAttribute);

            if (!skipAuthentication)
            {
                // First, check if the header is provided
                if (!context.HttpContext.Request.Headers.TryGetValue(API_KEY_NAME, out var providedAuthenticationKey))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Content = "No authentication header (using ApiKeyAuthenticateAttribute)"
                    };
                    return;
                }

                // Next, check if the key is correct
                if (!API_KEY_VALUE.Equals(providedAuthenticationKey, StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Content = "Invalid authentication key (using ApiKeyAuthenticateAttribute)"
                    };
                    return;
                }
            }
            await next();
        }
    }
}
