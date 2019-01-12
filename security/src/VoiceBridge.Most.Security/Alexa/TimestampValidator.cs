using System;
using System.Security;

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
        internal TimestampValidator()
        {
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="skewInMilliseconds">The skew to tolerate in request timings (in milliseconds)</param>
        internal TimestampValidator(int skewInSeconds)
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
        public bool Verify(HttpRequest http, SkillRequest payload, out Exception exception)
        {
            if (http == null)
            {
                exception = new ArgumentNullException(nameof(http));
                return false;
            }

            if ((payload?.Content ?? null) == null)
            {
                exception = new ArgumentNullException(nameof(payload));
                return false;
            }

            var offset = DateTime.Now.Subtract(payload.Content.Timestamp);

            // No time-travel for us!
            if (offset.TotalSeconds < 0)
            {
                exception = new SecurityException("Request was invalid");
                return false;
            }

            var isValid = offset.TotalSeconds <= tolerance;
            exception = isValid ? null : new SecurityException("Request failed timestamp validation");
            return isValid;
        }
    }
}
