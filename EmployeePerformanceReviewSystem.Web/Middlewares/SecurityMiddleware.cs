using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EmployeePerformanceReviewSystem.Web.Middlewares
{
    public class SecurityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SecurityMiddleware> _logger;

        public SecurityMiddleware(RequestDelegate next, ILogger<SecurityMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            if (!IsIntranetIP(ipAddress))
            {
                _logger.LogWarning("Blocked external IP access attempt.");
                if (context.Request.Headers["Accept"].ToString().Contains("text/html"))
                {
                    context.Response.Redirect("/Account/AccessDenied");
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Access Denied");
                }
                return;
            }

            var path = context.Request.Path.Value;

            if (path.StartsWith("/Account", StringComparison.OrdinalIgnoreCase))
            {
                // Skip auth checks for login/logout/access-denied pages
                await _next(context);
                return;
            }

            // Authentication check
            if (!context.User.Identity.IsAuthenticated)
            {
                _logger.LogWarning("Unauthenticated access attempt.");
                await context.ChallengeAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return;
            }

            // Authorization check
            if (!context.User.IsInRole("Admin") && !context.User.IsInRole("Manager"))
            {
                _logger.LogWarning("Unauthorized role access.");
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

            await _next(context); // Let the request continue if all is good
        }

        private bool IsIntranetIP(string ip)
        {
            if (string.IsNullOrEmpty(ip)) return false;
            return ip.StartsWith("192.168.") || ip.StartsWith("10.") || ip == "::1" || ip == "127.0.0.1";
        }
    }
}
