namespace EmployeePerformanceReviewSystem.Web.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogInformation("Incoming Request: {Method} {Path}", context.Request.Method, context.Request.Path);

                await _next(context); // Call the next middleware

                _logger.LogInformation("Outgoing Response: {StatusCode}", context.Response.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred while processing the request.");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    StatusCode = 500,
                    Message = "Internal Server Error. Please try again later."
                };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
