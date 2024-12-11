namespace FacturaAPI.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "x-api-key";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.ContainsKey(ApiKeyHeaderName) ||
                httpContext.Request.Headers[ApiKeyHeaderName] != "prueba")
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("Unauthorized");
                return;
            }

            await _next(httpContext);
        }
    }
}
