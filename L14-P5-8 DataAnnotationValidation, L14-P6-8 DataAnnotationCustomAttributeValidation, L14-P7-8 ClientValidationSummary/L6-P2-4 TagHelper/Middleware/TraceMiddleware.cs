using System;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using L6_P2_4_TagHelper.Infrastructure;
using Microsoft.AspNetCore.Http.Internal;

namespace L6_P2_4_TagHelper.Middleware
{
    public class TraceMiddleware
    {
        private readonly RequestDelegate _next;
        public TraceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger logger)
        {
            var originalBody = context.Response.Body;
            try
            {
                logger.Info($"REQUEST: {context.Request.Path} with {context.Request.QueryString}");

                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;

                    await _next(context);

                    memStream.Position = 0;
                    var responseBody = new StreamReader(memStream).ReadToEnd();
                    logger.Info($"RESPONSE: {responseBody}");

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }
            }
            catch (Exception ex)
            {
                logger.Error($"ERROR: {ex.Message}");
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
    }

    public static class TraceMiddlewareExtension
    {
        public static IApplicationBuilder UseTraceMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<TraceMiddleware>();
        }
    }
}
