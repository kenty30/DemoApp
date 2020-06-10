using Demo.Users.Api.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Demo.Users.Api.Extensions
{
    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
