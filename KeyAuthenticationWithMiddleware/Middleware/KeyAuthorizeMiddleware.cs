using Microsoft.AspNetCore.Mvc;

namespace KeyAuthenticationWithMiddleware.Middleware
{
    public class KeyAuthorizeMiddleware
    {
        private const string API_KEY_NAME = "Client-Authentication-Key";
        private readonly RequestDelegate _next;

        public KeyAuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // First, check if the header is provided
            if (!context.Request.Headers.TryGetValue(API_KEY_NAME, out var providedAuthenticationKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("No authentication header (using KeyAuthorizeMiddleware)");
                return;
            }

            // Next, check if keys are defined in configuration (appsettings.json)
            var configuration = context.RequestServices.GetService<IConfiguration>();
            string[]? authorizedKeys = configuration?.GetSection("ClientAuthenticationKeys").Get<string[]>();

            if (configuration == null || authorizedKeys == null)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync($"No authentication keys found (using KeyAuthorizeAttribute)");
                return;
            }

            // Last, check if the provided key is allowed
            if (!authorizedKeys.Any(k => k.Equals(providedAuthenticationKey, StringComparison.InvariantCultureIgnoreCase)))
            {

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid authentication key (using KeyAuthorizeMiddleware)");
                return;
            }

            await _next(context);
        }
    }
}
