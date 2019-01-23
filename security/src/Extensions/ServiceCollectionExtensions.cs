using Microsoft.Extensions.DependencyInjection;
using System;
using VoiceBridge.Most.Security.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Security.Extensions
{
    /// <summary>
    /// Extension methods for IServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds service to validate 
        /// </summary>
        /// <param name="cache">(Optional) Singleton instance of an ICertificateCache</param>
        /// <param name="options">(Optional) Delegate to expose the collection of IRequestValidators for Alexa</param>
        /// <remarks>
        /// If ICertificateCache is null, an in-memory store will be used
        /// </remarks>
        public static IServiceCollection AddAlexaValidation(this IServiceCollection services, string applicationId, ICertificateCache cache = null, Action<RequestValidatorOptions<SkillRequest>> options = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            ICertificateCache store = (cache == null) ? new TransientCertificateCache() : cache;
            services.AddSingleton<ICertificateCache>(store);

            services.AddScoped<AlexaValidator>();

            services.Configure<RequestValidatorOptions<SkillRequest>>(o =>
            {
                o.Validators.Add(new SignatureValidator(store));
                o.Validators.Add(new TargetValidator(applicationId));
                o.Validators.Add(new TimestampValidator());

                options?.Invoke(o);
            });

            return services;
        }
    }
}
