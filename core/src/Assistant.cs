using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.Directives;
using VoiceBridge.Most.Directives.Processors;
using VoiceBridge.Most.Google;
using VoiceBridge.Most.Logging;
using VoiceBridge.Most.VoiceModel;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most
{
    public class Assistant
    {
        private readonly List<IntentConfiguration> intents = new List<IntentConfiguration>();
        
        public IntentConfiguration OnIntent(string intentName)
        {
            var intent = new IntentConfiguration(intentName);
            intents.Add(intent);
            return intent;
        }
        
        public IntentConfiguration OnIntents(params string[] intentNames)
        {
            var intent = new IntentConfiguration(intentNames);
            intents.Add(intent);
            return intent;
        }
        
        public IntentConfiguration OnAnyIntent()
        {
            var intent = new IntentConfiguration();
            intents.Add(intent);
            return intent;
        }
        
        public EngineBuilder<SkillRequest, SkillResponse> AlexaEngineBuilder()
        {
            return CreateBuilder(new AlexaResponseFactory(), new AlexaInputModelBuilder());
        }

        public EngineBuilder<AppRequest, AppResponse> GoogleEngineBuilder()
        {
            return CreateBuilder(new ActionResponseFactory(), new ActionInputModelBuilder());
        }

        private EngineBuilder<TRequest, TResponse> CreateBuilder<TRequest, TResponse>(
            IResponseFactory<TResponse> responseFactory,
            IInputModelBuilder<TRequest> inputModelBuilder) 
            where TRequest : IRequest
            where TResponse : IResponse
        {
            var builder = new EngineBuilder<TRequest, TResponse>();
            builder
                .SetResponseFactory(responseFactory)
                .AddInputModelBuilder(inputModelBuilder);
            RegisterIntents(builder);
            RegisterProcessors(builder);
            return builder;
        }

        private void RegisterProcessors<TRequest, TResponse>(EngineBuilder<TRequest, TResponse> builder)
            where TRequest : IRequest
            where TResponse : IResponse
        {
            builder.AddDirectiveProcessor((IDirectiveProcessor<TRequest, TResponse>)new AskForValueProcessor());
            builder.AddDirectiveProcessor((IDirectiveProcessor<TRequest, TResponse>)new SayProcessor());
        }

        private void RegisterIntents<TRequest, TResponse>(EngineBuilder<TRequest, TResponse> engine) 
            where TRequest : IRequest 
            where TResponse : IResponse
        {
            foreach (var intentCfg in this.intents)
            {
                engine.AddRequestHandler(intentCfg);
            }
        }
    }
}