using System;
using VoiceBridge.Most.Logging;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most
{
    /**
     * This class contain some repetition. It needs clean up. Consider creating a common interface with EngineBuilder
     */
    /// <summary>
    /// Builder class for Composite Engine (Engine which works with both Alexa and Google Home)
    /// </summary>
    public class CompositeEngineBuilder
    {
        private readonly EngineBuilder<SkillRequest, SkillResponse> alexaBuilder;
        private readonly EngineBuilder<AppRequest, AppResponse> googleBuilder;
        private Action<EngineBuilder<SkillRequest, SkillResponse>> alexaConfigureAction;
        private Action<EngineBuilder<AppRequest, AppResponse>> googleConfigureAction;
        private ILogger logger;
        private IMetricsReporter metricsReporter;
        private ISessionStateStore sessionStateStore;

        public CompositeEngineBuilder(EngineBuilder<SkillRequest, SkillResponse> alexaBuilder,
            EngineBuilder<AppRequest, AppResponse> googleBuilder)
        {
            this.alexaBuilder = alexaBuilder;
            this.googleBuilder = googleBuilder;
        }

        /// <summary>
        /// Inject a logger into all engines hosted by the composite engine
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <returns>Self</returns>
        public CompositeEngineBuilder SetLogger(ILogger logger)
        {
            this.logger = logger;
            return this;
        }

        /// <summary>
        /// Inject metrics reporter into all engines hosted by the composite engine
        /// </summary>
        /// <param name="metricsReporter">Metrics Reporter</param>
        /// <returns>Self</returns>
        public CompositeEngineBuilder SetMetricsReporter(IMetricsReporter metricsReporter)
        {
            this.metricsReporter = metricsReporter;
            return this;
        }

        /// <summary>
        /// Inject Session State Store into all engines hosted by the composite engine
        /// </summary>
        /// <param name="sessionStateStore"></param>
        /// <returns></returns>
        public CompositeEngineBuilder SetSessionStore(ISessionStateStore sessionStateStore)
        {
            this.sessionStateStore = sessionStateStore;
            return this;
        }

        /// <summary>
        /// Configure Alexa Engine using the Engine Builder
        /// </summary>
        /// <param name="configureAction">Configuration Action</param>
        /// <returns>Self</returns>
        public CompositeEngineBuilder ConfigureAlexaEngine(
            Action<EngineBuilder<SkillRequest, SkillResponse>> configureAction)
        {
            this.alexaConfigureAction = configureAction;
            return this;
        }
        
        /// <summary>
        /// Configure Google Engine using the Engine Builder
        /// </summary>
        /// <param name="configureAction">Configuration Action</param>
        /// <returns>Self</returns>
        public CompositeEngineBuilder ConfigureGoogleEngine(
            Action<EngineBuilder<AppRequest, AppResponse>> configureAction)
        {
            this.googleConfigureAction = configureAction;
            return this;
        }

        /// <summary>
        /// Create an instance of composite engine
        /// </summary>
        /// <returns>ICompositeEngine</returns>
        public ICompositeEngine Build()
        {
            this.RunExternalConfiguration();
            this.ConfigureLogging();
            this.ConfigureMetrics();
            this.ConfigureSessionStateStore();
            
            var alexaEngine = this.alexaBuilder.Build();
            var googleEngine = this.googleBuilder.Build();
            
            return new CompositeEngine(alexaEngine, googleEngine);
        }

        private void RunExternalConfiguration()
        {
            this.alexaConfigureAction?.Invoke(this.alexaBuilder);
            this.googleConfigureAction?.Invoke(this.googleBuilder);
        }

        private void ConfigureSessionStateStore()
        {
            if (this.sessionStateStore != null)
            {
                this.alexaBuilder.SetSessionStore(this.sessionStateStore);
                this.googleBuilder.SetSessionStore(this.sessionStateStore);
            }
        }

        private void ConfigureMetrics()
        {
            if (this.metricsReporter != null)
            {
                this.alexaBuilder.SetMetricsReporter(this.metricsReporter);
                this.googleBuilder.SetMetricsReporter(this.metricsReporter);
            }
        }

        private void ConfigureLogging()
        {
            if (this.logger != null)
            {
                this.alexaBuilder.SetLogger(this.logger);
                this.googleBuilder.SetLogger(this.logger);
            }
        }
    }
}