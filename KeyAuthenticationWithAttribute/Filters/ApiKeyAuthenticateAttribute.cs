using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace KeyAuthenticationWithAttribute.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class KeyAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string _apiKeyName;
        private readonly string _apiKeyValue;

        public KeyAuthorizeAttribute(string header = "Client-Authentication-Key")
        {
            _apiKeyName = header;
            _apiKeyValue = "F147A2F0-9E7B-455B-BDBF-1BE554D95E73";
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // if NoApiKeyAuthentication then skip
            var skipAuthentication = context.ActionDescriptor.EndpointMetadata.Any(a => a is AllowAnonymousAttribute);

            if (!skipAuthentication)
            {
                // First, check if the header is provided
                if (!context.HttpContext.Request.Headers.TryGetValue(_apiKeyName, out var providedAuthenticationKey))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Content = "No authentication header (using ApiKeyAuthenticateAttribute)"
                    };
                    return;
                }

                // Next, check if the key is correct
                if (!_apiKeyValue.Equals(providedAuthenticationKey, StringComparison.InvariantCultureIgnoreCase))
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
