using System;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Security.Alexa
{
    /// <summary>
    /// Verifies that a request is actually intended for this agent
    /// </summary>
    public class TargetValidator : IRequestValidator<SkillRequest>
    {
        private string applicationId = "";


        /// <summary>
        /// Constructor
        /// </summary>
        public TargetValidator(string applicationId)
        {
            this.applicationId = applicationId;
        }


        /// <summary>
        /// Verify the request
        /// </summary>
        public Task VerifyAsync(HttpRequest http, SkillRequest payload, string input)
        {
            if (http == null)
                throw new ArgumentNullException(nameof(http));

            if ((payload?.Session?.Application ?? null) == null)
                throw new ArgumentNullException(nameof(payload));

            if(string.IsNullOrWhiteSpace(payload.Session.Application.ApplicationId))
                throw new SecurityException("Application ID cannot be null or empty");

            if (!payload.Session.Application.ApplicationId.Equals(applicationId, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityException("Request was not intended for this target");

            return Task.CompletedTask;
        }
    }
}
