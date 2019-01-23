using System;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Security.Google
{
    /// <summary>
    /// Verifies that a request is actually intended for this agent
    /// </summary>
    public class TargetValidator : IRequestValidator<AppRequest>
    {
        private Regex regex = new Regex("/([^/]*)/", RegexOptions.Compiled);
        private string projectId = "";


        /// <summary>
        /// Constructor
        /// </summary>
        public TargetValidator(string projectId)
        {
            this.projectId = "/" + projectId + "/";
        }


        /// <summary>
        /// Verify the request
        /// </summary>
        public Task VerifyAsync(HttpRequest http, AppRequest payload, string input)
        {
            if (http == null)
                throw new ArgumentNullException(nameof(http));

            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            if(string.IsNullOrWhiteSpace(payload.SessionId))
                throw new SecurityException("Session ID cannot be null or empty");

            var matches = regex.Matches(payload.SessionId);

            if(matches.Count == 0 || !matches[0].Value.Equals(projectId, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityException("Request was not intended for this target");

            return Task.CompletedTask;
        }
    }
}
