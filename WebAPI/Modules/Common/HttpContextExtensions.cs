using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace WebAPI.Modules.Common
{
    public static class HttpContextExtensions
    {
        public static HttpContext AddCorsHeaders(this HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("Access-Control-Allow-Headers", new StringValues("*"));
            httpContext.Response.Headers.Add("Access-Control-Allow-Methods", new StringValues("*"));
            httpContext.Response.Headers.Add("Access-Control-Allow-Origin", new StringValues("*"));
            httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", new StringValues("true"));
            return httpContext;
        }

        public static string GetToken(this IHttpContextAccessor httpContextAccessor)
        {
            httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("X-Access-Token", out var token);
            return token;
        }
        public static string GetCookie(this IHttpContextAccessor httpContextAccessor)
        {
            httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Cookie", out var cookie);
            return cookie;
       
        }
    }
}