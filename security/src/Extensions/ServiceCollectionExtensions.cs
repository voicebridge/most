using Microsoft.Extensions.DependencyInjection;
using System;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Security.Extensions
{
    /// <summary>
    /// Extension methods for IServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds service to validate Amazon Alexa requests
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
                o.Validators.Add(new Alexa.SignatureValidator(store));
                o.Validators.Add(new Alexa.TargetValidator(applicationId));
                o.Validators.Add(new Alexa.TimestampValidator());

                options?.Invoke(o);
            });

            return services;
        }


        /// <summary>
        /// Adds service to validate Google Home requests
        /// </summary>
        /// <param name="options">(Optional) Delegate to expose the collection of IRequestValidators for Google Home</param>
        public static IServiceCollection AddGoogleHomeValidation(this IServiceCollection services, string projectId, Action<RequestValidatorOptions<AppRequest>> options = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<GoogleHomeValidator>();

            services.Configure<RequestValidatorOptions<AppRequest>>(o =>
            {
                o.Validators.Add(new Google.TargetValidator(projectId));

                options?.Invoke(o);
            });

            return services;
        }
    }
}
