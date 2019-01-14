using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using VoiceBridge.Most.Security;
using VoiceBridge.Most.Security.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Security
{
    public class AlexaAuth : ActionFilterAttribute
    {
        ICertificateCache cache;


        public AlexaAuth(ICertificateCache cache)
        {
            this.cache = cache;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var validator = new SignatureValidator(new TransientCertificateCache());
            var payload = new StreamReader(context.HttpContext.Request.Body).ReadToEnd();
            var skill = JsonConvert.DeserializeObject<SkillRequest>(payload);

            try
            {
                var task = validator.VerifyAsync(context.HttpContext.Request, skill, payload);
                task.Wait();

                context.HttpContext.Items.Add(SecurityConstants.Platform.Alexa, skill);

                base.OnActionExecuting(context);
            }
            catch (Exception)
            {
                // TODO: Log error
                context.Result = new UnauthorizedResult();
            }
        }
    }
}