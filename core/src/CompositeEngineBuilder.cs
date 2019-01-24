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

        public CompositeEngineBuilder SetLogger(ILogger logger)
        {
            this.logger = logger;
            return this;
        }

        public CompositeEngineBuilder SetMetricsReporter(IMetricsReporter metricsReporter)
        {
            this.metricsReporter = metricsReporter;
            return this;
        }

        public CompositeEngineBuilder SetSessionStore(ISessionStateStore sessionStateStore)
        {
            this.sessionStateStore = sessionStateStore;
            return this;
        }

        public CompositeEngineBuilder ConfigureAlexaEngine(
            Action<EngineBuilder<SkillRequest, SkillResponse>> configureAction)
        {
            this.alexaConfigureAction = configureAction;
            return this;
        }
        
        public CompositeEngineBuilder ConfigureGoogleEngine(
            Action<EngineBuilder<AppRequest, AppResponse>> configureAction)
        {
            this.googleConfigureAction = configureAction;
            return this;
        }

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