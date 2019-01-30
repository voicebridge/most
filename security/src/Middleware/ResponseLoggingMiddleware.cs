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
    /// Middleware that logs Http Response to an ILogger
    /// </summary>
    public class ResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;


        /// <summary>
        /// Constructor
        /// </summary>
        public ResponseLoggingMiddleware(RequestDelegate next, ILogger logger)
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
            var original = context.Response.Body;
            context.Response.Body = memory;

            await _next(context);

            memory.Seek(0, SeekOrigin.Begin);

            var body = new StreamReader(memory).ReadToEnd();
            _logger.Log(LogLevel.Information, $"[Response] {context.Request.Method} {context.Request.Path} {body}");

            memory.Seek(0, SeekOrigin.Begin);
            await memory.CopyToAsync(original);
        }
    }
}
