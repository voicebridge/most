using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Sample;
using VoiceBridge.Common;
using VoiceBridge.Most;
using VoiceBridge.Most.Directives;
using VoiceBridge.Most.LambdaHelper;
using VoiceBridge.Most.VoiceModel;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(MostSerializer))]

namespace Sample
{  
    public class Function
    {
        private ICompositeEngine compositeEngine;
        
        private static class IntentNames
        {
            public const string Score = "GetScoreIntent";
            public const string TeamName = "TeamNameIntent";
            public const string AudioSampleIntent = "SoundIntent";
        }
        
        private static class ParamNames
        {
            public const string Team = "team_name";
            public const string City = "city_name";
        }

        public Function()
        {
            var logger = new Logger();
            this.compositeEngine = GetAssistant().CompositeEngineBuilder().SetLogger(logger).Build();

        }

        public async Task<IResponse> HandleRequest(IRequest request, ILambdaContext context)
        {
            Log(request);

            try
            {
                var response = await this.compositeEngine.Evaluate(request);
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
            ConfigureScoreIntent(assistant);
            ConfigureSoundIntent(assistant);
            ConfigureWelcomeMessage(assistant);
            ConfigureTeamNameIntent(assistant);
            return assistant;
        }

        private static Assistant ConfigureSoundIntent(Assistant assistant)
        {
            var media = new Media
            {
                Author = "San Jose Sharks",
                LargeImageUrl = new SecureUrl("https://s3.amazonaws.com/voicebridge-assets/sharks_logo.png"),
                SmallImageUrl = new SecureUrl("https://s3.amazonaws.com/voicebridge-assets/sharks_logo.png"),
                StreamUrl = new SecureUrl("https://s3.amazonaws.com/voicebridge-assets/sample_file.mp3"),
                Title = "San Jose Goal Score Horn",
                Subtitle = "This is a sample audio"
            };

            assistant
                .OnIntent(IntentNames.AudioSampleIntent)
                .PlayAudio(media, "Here is the sound!".AsPrompt());
            
            return assistant;
        }

        private static Assistant ConfigureWelcomeMessage(Assistant assistant)
        {
            var text = "Hello! My name is Fakey, and I will give you fake scores for the NHL's pacific division";

            assistant
                .OnLaunch()
                .Say(text.AsPrompt())
                .KeepSessionOpen();

            return assistant;      
        }
        
        private static Assistant ConfigureTeamNameIntent(Assistant assistant)
        {
            assistant
                .OnIntent(IntentNames.TeamName)
                .WhenParameterIsSupplied(ParamNames.City)
                .Do(context =>
                {
                    var cityId = context.RequestModel.GetParameterValue(ParamNames.City);
                    var team = Teams.GetTeamByCity(cityId);
                    if (team != null)
                    {
                        return Result.Say($"The name of the team is {team.Name}".AsPrompt());
                    }

                    return Result.Say("I should know this but sadly I don't!".AsPrompt());
                });

            assistant
                .OnIntent(IntentNames.TeamName)
                .WhenParameterIsMissing(ParamNames.City)
                .AskFor(ParamNames.City, "What city would you like?".AsPrompt());

            return assistant;
        }
        
        private static Assistant ConfigureScoreIntent(Assistant assistant)
        {
            assistant
                .OnIntent(IntentNames.Score)
                .WhenParameterIsSupplied(ParamNames.Team)
                .Do(context =>
                {
                    var targetTeamId = context.RequestModel.GetParameterValue(ParamNames.Team);
                    var team = Teams.GetTeam(targetTeamId);
                    var fakeScore = Teams.GenerateAFakeScore(team);
                    return Result.Say(fakeScore.AsPrompt());
                });

            assistant
                .OnIntent(IntentNames.Score)
                .WhenParameterIsMissing(ParamNames.Team)
                .Do(context => Result.AskFor(ParamNames.Team, "What team would you like?".AsPrompt()));

            return assistant;
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