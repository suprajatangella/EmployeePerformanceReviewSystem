namespace EmployeePerformanceReviewSystem.Web.Middlewares
{
    public class RoleAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RoleAuthorizationMiddleware> _logger;

        public RoleAuthorizationMiddleware(RequestDelegate next, ILogger<RoleAuthorizationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            if (context.User.Identity?.IsAuthenticated == true)
            {
                // EMPLOYEE ROLE RESTRICTIONS
                if (context.User.IsInRole("Employee"))
                {
                    // If trying to access edit/delete/create, block them
                    if (path.Contains("/edit") || path.Contains("/delete") || path.Contains("/create") || path.Contains("/admin"))
                    {
                        _logger.LogWarning("Employee tried to access restricted path: {Path}", path);
                        if (context.Request.Headers["Accept"].ToString().Contains("text/html"))
                        {
                            context.Response.Redirect("/Account/AccessDenied");
                        }
                        else
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            await context.Response.WriteAsync("Access Denied. You are not authorized.");
                        }
                        return;
                    }
                }

                // ADMIN ROLE — Full access allowed — no action needed
            }
            else
            {
                // Not logged in
                _logger.LogWarning("Unauthenticated access attempt to: {Path}", path);
                context.Response.Redirect("/Account/Login");
                return;
            }

            await _next(context); // Call next middleware
        }
    }
}
