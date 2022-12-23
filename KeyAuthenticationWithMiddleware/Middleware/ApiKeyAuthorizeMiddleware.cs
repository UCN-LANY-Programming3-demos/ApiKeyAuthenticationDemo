namespace KeyAuthenticationWithMiddleware.Middleware
{
    public class ApiKeyAuthorizeMiddleware
    {
        private const string API_KEY_NAME = "Client-Authentication-Key";
        private const string API_KEY_VALUE = "F147A2F0-9E7B-455B-BDBF-1BE554D95E73"; // In this example the key is hardcoded, but it should be stored in a file or a database. Also, each client should have its own unique key.
        private readonly RequestDelegate _next;

        public ApiKeyAuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // First, check if the header is provided
            if (!context.Request.Headers.TryGetValue(API_KEY_NAME, out var providedAuthenticationKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("No authentication header (using ApiKeyAuthenticateMiddleware)");
                return;
            }

            // Next, check if the key is correct
            if (!API_KEY_VALUE.Equals(providedAuthenticationKey, StringComparison.InvariantCultureIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid authentication key (using ApiKeyAuthenticateMiddleware)");
                return;
            }

            await _next(context);
        }
    }
}
