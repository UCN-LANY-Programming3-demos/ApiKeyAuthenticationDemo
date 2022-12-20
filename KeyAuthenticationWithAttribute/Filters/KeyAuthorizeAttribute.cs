using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KeyAuthenticationWithAttribute.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class KeyAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _apiKeyName;
        private readonly string[] _apiKeys;

        public KeyAuthorizeAttribute(string header = "Client-Authentication-Key")
        {
            _apiKeyName = header;
            _apiKeys = new[] { "F147A2F0-9E7B-455B-BDBF-1BE554D95E73", "F147A2F0-9E7B-455B-BDBF-1BE554D95E74" };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Checks if authorization is skipped 
            var skipAuthentication = context.ActionDescriptor.EndpointMetadata.Any(a => a is AllowAnonymousAttribute);

            if (skipAuthentication)
            {
                return;
            }

            // Checks if the header is provided
            if (!context.HttpContext.Request.Headers.TryGetValue(_apiKeyName, out var providedAuthenticationKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Content = $"Authentication header [{_apiKeyName}] not found"
                };
                return;
            }

            // Checks if the key is correct
            if (!_apiKeys.Any(k => k.Equals(providedAuthenticationKey, StringComparison.InvariantCultureIgnoreCase)))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Content = "Invalid authentication key (using ApiKeyAuthenticateAttribute)"
                };
                return;
            }            
        }
    }
}
