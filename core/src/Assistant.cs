using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using VoiceBridge.Most.Alexa;
using VoiceBridge.Most.Directives;
using VoiceBridge.Most.Directives.Processors;
using VoiceBridge.Most.Google;
using VoiceBridge.Most.Logging;
using VoiceBridge.Most.VoiceModel;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa.Directives;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Builds rules for a virtual assistant
    /// </summary>
    public class Assistant
    {
        private readonly List<IRequestHandlerBuilder> requestHandlerBuilders = new List<IRequestHandlerBuilder>();

        /// <summary>
        /// On first launch. This is the MAIN intent for Google Actions
        /// and Launch Request for Alexa
        /// </summary>
        /// <returns>Itself</returns>
        public IRequestHandlerBuilder OnLaunch()
        {
            return CreateHandlerBuilder(RequestType.Launch);
        }

        /// <summary>
        /// Triggered when the user quits (says quit, cancel, stop)
        /// </summary>
        /// <returns>Itself</returns>
        public IRequestHandlerBuilder OnExit()
        {
            return CreateHandlerBuilder(RequestType.UserInitiatedTermination);
        }
        
        /// <summary>
        /// Sets a handler to be triggered on a specific intent name
        /// </summary>
        /// <param name="intentName">Name of the intent to match (case-insensitive)</param>
        /// <returns>IRequestHandlerBuilder</returns>
        public IRequestHandlerBuilder OnIntent(string intentName)
        {
            return CreateHandlerBuilder(RequestType.Intent)
                .When(ctx => Util.StringOrdinalEquals(ctx.RequestModel.IntentName, intentName));
        }

        /// <summary>
        /// Set a handler to be triggered when a request comes in notifying you of audio player status change
        /// </summary>
        /// <returns>IRequestHandlerBuilder</returns>
        public IRequestHandlerBuilder OnAudioPlayerStatusChange()
        {
            return CreateHandlerBuilder(RequestType.AudioPlayerStatusChange);
        }

        /// <summary>
        /// Set a handler to be triggered when a display element is triggered (List, Button, etc...)
        /// </summary>
        /// <returns>IRequestHandlerBuilder</returns>
        public IRequestHandlerBuilder OnDisplayElementEvent()
        {
            return CreateHandlerBuilder(RequestType.NonVoiceInputEvent);
        }
        
        /// <summary>
        /// Set a handler to be triggered when any intent in the list provided is detected.
        /// </summary>
        /// <param name="intentNames">Names of intents to match</param>
        /// <returns>IRequestHandlerBuilder</returns>
        public IRequestHandlerBuilder OnIntents(params string[] intentNames)
        {
            return CreateHandlerBuilder(RequestType.Intent)
                .When(ctx => intentNames.Any(name => Util.StringOrdinalEquals(name, ctx.RequestModel.IntentName)));
        }
        
        /// <summary>
        /// Set a handler to be triggered on any intent name
        /// </summary>
        /// <returns>IRequestHandlerBuilder</returns>
        public IRequestHandlerBuilder OnAnyIntent()
        {
            return CreateHandlerBuilder(RequestType.Intent);
        }
        
        /// <summary>
        /// Creates an Alexa Engine Builder
        /// </summary>
        /// <returns>Engine Builder</returns>
        public EngineBuilder<SkillRequest, SkillResponse> AlexaEngineBuilder()
        {
            var compositeBuilder = new CompositeInputModelBuilder<SkillRequest>(
                new IInputModelBuilder<SkillRequest>[]
            {
                new AlexaInputModelBuilder(), 
                new NonVoiceEventsInputModelBuilder(),
                new AlexaCapabilitiesInputModelBuilder()
            });
            return CreateBuilder(new AlexaResponseFactory(), compositeBuilder);
        }

        /// <summary>
        /// Creates a Google Assistant Engine builder
        /// </summary>
        /// <returns>Engine Builder</returns>
        public EngineBuilder<AppRequest, AppResponse> GoogleEngineBuilder()
        {
            var compositeBuilder = new CompositeInputModelBuilder<AppRequest>(
                new IInputModelBuilder<AppRequest>[]
                {
                    new ActionInputModelBuilder(), 
                    new NonVoiceInputModelBuilder(),
                    new GoogleCapabilitiesInputModelBuilder(),
                });
            return CreateBuilder(new ActionResponseFactory(), compositeBuilder);
        }

        private IRequestHandlerBuilder CreateHandlerBuilder(RequestType requestType)
        {
            var builder = new RequestHandlerBuilder(requestType);
            this.requestHandlerBuilders.Add(builder);
            return builder;
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
            builder.AddDirectiveProcessor((IDirectiveProcessor<TRequest, TResponse>)new PlayMediaProcessor());
            builder.AddDirectiveProcessor((IDirectiveProcessor<TRequest, TResponse>)new ImageProcessor());
            builder.AddDirectiveProcessor((IDirectiveProcessor<TRequest, TResponse>)new SessionProcessor());
        }

        private void RegisterIntents<TRequest, TResponse>(EngineBuilder<TRequest, TResponse> engine) 
            where TRequest : IRequest 
            where TResponse : IResponse
        {
            foreach (var intentCfg in this.requestHandlerBuilders)
            {
                engine.AddRequestHandler(intentCfg);
            }
        }
    }
}