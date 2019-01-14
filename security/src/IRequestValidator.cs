using System;
using System.Threading.Tasks;
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
        Task VerifyAsync(HttpRequest http, TRequest payload, string input);
    }
}
