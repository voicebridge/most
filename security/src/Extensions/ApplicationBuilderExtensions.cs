using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using VoiceBridge.Most.Security.Middleware;

namespace VoiceBridge.Most.Security.Extensions
{
    /// <summary>
    /// Extension methods for IApplicationBuilder
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Logs Http Requests to an ILogger
        /// </summary>
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }


        /// <summary>
        /// Logs Http Responses to an ILogger
        /// </summary>
        public static IApplicationBuilder UseResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseLoggingMiddleware>();
        }
    }
}
