using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VoiceBridge.Most.Logging;

namespace VoiceBridge.Most.Security.Middleware
{
    /// <summary>
    /// Middleware that logs Http Requests to an ILogger
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;


        /// <summary>
        /// Constructor
        /// </summary>
        public RequestLoggingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }


        /// <summary>
        /// Invokes this middleware
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            var memory = new MemoryStream();

            await context.Request.Body.CopyToAsync(memory);
            memory.Seek(0, SeekOrigin.Begin);
            context.Request.Body = memory;

            var body = new StreamReader(memory).ReadToEnd();
            _logger.Log(LogLevel.Information, $"[Request] {context.Request.Method} {context.Request.Path} {body}");

            memory.Seek(0, SeekOrigin.Begin);

            await _next(context);
        }
    }
}
