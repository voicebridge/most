﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;

using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Security
{
    /// <summary>
    /// ActionFilter attribute that enforces SkillRequest security validation (required by Amazon)
    /// </summary>
    public sealed class AlexaValidator : ActionFilterAttribute
    {
        private ICertificateCache cache;
        private RequestValidatorOptions<SkillRequest> options;


        /// <summary>
        /// Constructor
        /// </summary>
        public AlexaValidator(ICertificateCache cache, IOptionsMonitor<RequestValidatorOptions<SkillRequest>> options)
        {
            this.cache = cache;
            this.options = options.CurrentValue;
        }


        /// <summary>
        /// Trigger request verification on action execution
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var payload = new StreamReader(context.HttpContext.Request.Body).ReadToEnd();
                var skill = JsonConvert.DeserializeObject<SkillRequest>(payload);
            
                foreach (var validator in options.Validators)
                {
                    var task = validator.VerifyAsync(context.HttpContext.Request, skill, payload);
                    task.Wait();
                }

                context.HttpContext.Items.Add(SecurityConstants.Platform.Alexa, skill);

                base.OnActionExecuting(context);
            }
            catch (Exception)
            {
                // TODO: Log errors
                context.Result = new UnauthorizedResult();
            }
        }
    }
}