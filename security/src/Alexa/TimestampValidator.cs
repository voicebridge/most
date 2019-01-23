using System;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Security.Alexa
{
    /// <summary>
    /// Verifies request timestamps to ensure an Alexa payload is valid
    /// </summary>
    public class TimestampValidator : IRequestValidator<SkillRequest>
    {
        /// <summary>
        /// Maximum allowed timestamp offset tolerance value in seconds
        /// </summary>
        public const int MAXIMUM_TOLERANCE_SEC = 3600;


        /// <summary>
        /// Default timestamp offset tolerance value in seconds (unless overidden in AlexaTimestampValidator constructor)
        /// </summary>
        public const int DEFAULT_TOLERANCE_SEC = 30;


        // Private members
        private int tolerance = DEFAULT_TOLERANCE_SEC;


        /// <summary>
        /// Constructor
        /// </summary>
        public TimestampValidator()
        {
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="skewInMilliseconds">The skew to tolerate in request timings (in milliseconds)</param>
        public TimestampValidator(int skewInSeconds)
        {
            if (skewInSeconds < 0)
                throw new ArgumentException($"Skew tolerance must be a positive number");

            if (skewInSeconds > MAXIMUM_TOLERANCE_SEC)
                throw new ArgumentException($"Skew tolerance cannot be larger than {MAXIMUM_TOLERANCE_SEC} seconds");

            tolerance = skewInSeconds;
        }


        /// <summary>
        /// Verify the request
        /// </summary>
        public Task VerifyAsync(HttpRequest http, SkillRequest payload, string input)
        {
            if (http == null)
                throw new ArgumentNullException(nameof(http));

            if ((payload?.Content ?? null) == null)
                throw new ArgumentNullException(nameof(payload));

            var offset = Math.Abs(DateTime.Now.Subtract(payload.Content.Timestamp).TotalSeconds);

            if (offset > tolerance)
                throw new SecurityException("Request failed timestamp validation");

            return Task.CompletedTask;
        }
    }
}
