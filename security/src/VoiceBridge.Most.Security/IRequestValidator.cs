using System;
using Microsoft.AspNetCore.Http;

namespace VoiceBridge.Most.Security
{
    /// <summary>
    /// Interface for the verification of requests/payloads
    /// </summary>
    public interface IRequestValidator<TRequest>
    {
        /// <summary>
        /// Verify the request
        /// </summary>
        bool Verify(HttpRequest http, TRequest payload, out Exception exception);
    }
}
