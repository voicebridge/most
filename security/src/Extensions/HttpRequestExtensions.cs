using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Security.Extensions
{
    /// <summary>
    /// Extensions to the HttpRequest object
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Extract an Alexa SkillRequest payload from the given HttpRequest
        /// </summary>
        public static SkillRequest ToSkillRequest(this HttpRequest item)
        {
            if (item.HttpContext.Items.TryGetValue(SecurityConstants.Platform.Alexa, out var result))
                return result as SkillRequest;

            return null;
        }
    }
}
