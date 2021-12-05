using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace PaymentSystemApi.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        
        public CultureMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        
        public async Task Invoke(HttpContext context)
        {
            var lang = context.Request.Headers["Content-Language"];
            if (string.IsNullOrEmpty(lang))
                CultureInfo.CurrentCulture = new CultureInfo("ru");
            else
            {
                try
                {
                    CultureInfo.CurrentCulture = new CultureInfo(lang);
                    CultureInfo.CurrentUICulture = new CultureInfo(lang);
                }
                catch (CultureNotFoundException) { }
            }
            
            await _next.Invoke(context);
        }
    }
 
    public static class CultureExtensions
    {
        public static IApplicationBuilder UseCulture(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CultureMiddleware>();
        }
    }
}