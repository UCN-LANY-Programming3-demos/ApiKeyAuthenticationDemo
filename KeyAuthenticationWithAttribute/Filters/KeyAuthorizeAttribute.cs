using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KeyAuthenticationWithAttribute.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class KeyAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _httpHeaderName;
    
        public KeyAuthorizeAttribute(string header = "Client-Authentication-Key")
        {
            _httpHeaderName = header;
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
            if (!context.HttpContext.Request.Headers.TryGetValue(_httpHeaderName, out var providedAuthenticationKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Content = $"Authentication header [{_httpHeaderName}] not found"
                };
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();

            if (configuration == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Content = $"No authentication keys found"
                };
                return;
            }

            string[] _authorizedKeys = configuration.GetSection("ClientAuthenticationKeys").Get<string[]>();

            // Checks if the provided key is allowed
            if (!_authorizedKeys.Any(k => k.Equals(providedAuthenticationKey, StringComparison.InvariantCultureIgnoreCase)))
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
