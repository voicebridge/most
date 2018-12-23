using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using SampleSkill;
using VoiceBridge.Most;
using VoiceBridge.Most.Directives;
using VoiceBridge.Most.Logging;
using VoiceBridge.Most.VoiceModel;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SampleSkill
{  
    public class Function
    {
        private static class ParamNames
        {
            public const string Team = "team_name";
        }
        
        public async Task<SkillResponse> FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            var engine = GetAssistant()
                .AlexaEngineBuilder()
                .SetLogger(new Logger())
                .Build();
            
            return await Process(engine, input, context);
        }
        
        public async Task<AppResponse> ActionHandler(AppRequest input, ILambdaContext context)
        {
            var engine = GetAssistant()
                .GoogleEngineBuilder()
                .SetLogger(new Logger())
                .Build();
            
            return await Process(engine, input, context);
        }

        private async Task<TResponse> Process<TRequest, TResponse>(
            IConversationEngine<TRequest, TResponse> engine,
            TRequest request,
            ILambdaContext context)
            where TRequest : IRequest
            where TResponse : IResponse
        {
            Log(request);

            try
            {
                var response = await engine.Evaluate(request);
                Log(response);
                return response;
            }
            catch (Exception e)
            {
                LambdaLogger.Log($"Exception: {e}");
                throw;
            }
        }

        private Assistant GetAssistant()
        {
            var assistant = new Assistant();
            assistant
                .OnIntents("ScoreIntent", "HockeyIntent")
                .When(x => x.RequestModel.GetParameterValue(ParamNames.Team) == "sj")
                .Say("Sharks beat the wild! Today they will play the jets");

            assistant
                .OnIntents("ScoreIntent", "HockeyIntent", "actions.intent.Text")
                .When(x => x.RequestModel.GetParameterValue(ParamNames.Team) == "vn")
                .Say("Not sure about vancouver");

            assistant
                .OnIntent("LatestHockeyScoresIntent")
                .Do(CreatePlayMediaDirective);

            assistant
                .OnAnyIntent()
                .When(x => !x.RequestModel.ParameterHasValue(ParamNames.Team))
                .AskFor(ParamNames.Team, "What team would you like?");

            return assistant;
        }

        private IVirtualDirective CreatePlayMediaDirective()
        {
            return null;
        }

        private static void Log<TModel>(TModel input)
        {
            if (input == null)
            {
                LambdaLogger.Log($"Model of type {typeof(TModel).FullName} is null");
                return;
            }
            
            var json = JsonConvert.SerializeObject(input);
            LambdaLogger.Log(json);
        }
    }
}