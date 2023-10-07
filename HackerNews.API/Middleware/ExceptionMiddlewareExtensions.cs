using HackerNews.Service.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;

namespace HackerNews.API.Middleware
{
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Configure Extension method to handel exceptions at global level.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="logger"></param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appErrorr => {
                appErrorr.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        { 
                        StatusCode= context.Response.StatusCode,
                        Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        
        }
    }
}
