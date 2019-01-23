using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Most.Security
{
    /// <summary>
    /// Options for IRequestValidators
    /// </summary>
    public class RequestValidatorOptions<TRequest>
    {
        /// <summary>
        /// Collection of validators to apply to a request of type TRequest
        /// </summary>
        public IList<IRequestValidator<TRequest>> Validators;


        /// <summary>
        /// Constructor
        /// </summary>
        public RequestValidatorOptions()
        {
            Validators = new List<IRequestValidator<TRequest>>();
        }
    }
}
