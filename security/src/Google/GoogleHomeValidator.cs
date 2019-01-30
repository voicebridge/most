using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using VoiceBridge.Most.Logging;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Security
{
    /// <summary>
    /// ActionFilter attribute that enforces basic AppRequest validation for Google Home
    /// </summary>
    public sealed class GoogleHomeValidator : ActionFilterAttribute
    {
        private RequestValidatorOptions<AppRequest> options;
        private ILogger logger;


        /// <summary>
        /// Constructor
        /// </summary>
        public GoogleHomeValidator(IOptionsMonitor<RequestValidatorOptions<AppRequest>> options, ILogger logger = null)
        {
            this.options = options.CurrentValue;
            this.logger = logger;
        }


        /// <summary>
        /// Trigger request verification on action execution
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var payload = new StreamReader(context.HttpContext.Request.Body).ReadToEnd();
                var skill = JsonConvert.DeserializeObject<AppRequest>(payload);
            
                foreach (var validator in options.Validators)
                {
                    var task = validator.VerifyAsync(context.HttpContext.Request, skill, payload);
                    task.Wait();
                }

                context.HttpContext.Items.Add(SecurityConstants.Platform.GoogleHome, skill);

                base.OnActionExecuting(context);
            }
            catch (Exception error)
            {
                logger?.Log(LogLevel.Error, $"[{error.GetType().Name}] {error.Message}");
                context.Result = new UnauthorizedResult();
            }
        }
    }
}