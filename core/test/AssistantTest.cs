using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using VoiceBridge.Most.Test.TestData;
using VoiceBridge.Most.VoiceModel.Alexa;
using Xunit;
using Xunit.Abstractions;

namespace VoiceBridge.Most.Test
{
    public class AssistantTest : TestBase
    {
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
        
        [Fact]
        public async Task HappyPath()
        {
            const string promptText = "56 degrees";
            var request = TestData.AlexaRequests.Weather("Menlo Park");
            
            var assistant = new Assistant();
            assistant
                .OnIntent(TestIntents.Weather)
                .When(x => x.RequestModel.ParameterHasValue("city"))
                .Say(promptText.AsPrompt());
            
            var engine = assistant.AlexaEngineBuilder().Build();
            
            var response = await engine.Evaluate(request);
            Assert.True(response.Content.ShouldEndSession);
            Assert.Equal(promptText, ((PlainTextOutputSpeech)response.Content.OutputSpeech).Text);
        }

        [Fact]
        public async Task ValidateRealRequest()
        {
            var timer = new Stopwatch();
            timer.Start();
            var response = await RunSimulation(Files.HockeyScoreIntentRequest);
            timer.Stop();
            this.LogMessage($"Time taken is: {timer.Elapsed.TotalMilliseconds} ms");
        }

        private async Task<SkillResponse> RunSimulation(string requestJson)
        {
            var request = JsonConvert.DeserializeObject<SkillRequest>(requestJson);
            var assistant = SetupHockeyAssistant();
            return await assistant
                            .AlexaEngineBuilder()
                            .SetLogger(this)
                            .Build()
                            .Evaluate(request);
        }

        private static Assistant SetupHockeyAssistant()
        {
            var assistant = new Assistant();
            ConfigureScoreIntent(assistant);
            ConfigureSoundIntent(assistant);
            ConfigureWelcomeMessage(assistant);
            ConfigureTeamNameIntent(assistant);
            return assistant;
        }

        private static void ConfigureSoundIntent(Assistant assistant)
        {
            var media = new Media
            {
                Author = "San Jose Sharks",
                LargeImageUrl = new Uri("https://s3.amazonaws.com/voicebridge-assets/sharks_logo.png"),
                SmallImageUrl = new Uri("https://s3.amazonaws.com/voicebridge-assets/sharks_logo.png"),
                StreamUrl = new Uri("https://s3.amazonaws.com/voicebridge-assets/sample_file.mp3"),
                Title = "San Jose Goal Score Horn",
                Subtitle = "This is a sample audio"
            };

            assistant
                .OnIntent(IntentNames.AudioSampleIntent)
                .PlayAudio(media, null);
        }

        private static void ConfigureWelcomeMessage(Assistant assistant)
        {
            assistant
                .OnLaunch()
                .Say(
                    "Hello! My name is Fakey, and I will give you fake scores for the NHL's pacific division"
                        .AsPrompt(), keepSessionOpen: true);
        }
        
        private static Assistant ConfigureTeamNameIntent(Assistant assistant)
        {
            assistant
                .OnIntent(IntentNames.TeamName)
                .WhenParameterIsSupplied(ParamNames.City)
                .Do(context => Result.Say("I should know this but sadly I don't!".AsPrompt()));

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
                .Do(context => Result.Say("2 and 2!".AsPrompt()));

            assistant
                .OnIntent(IntentNames.Score)
                .WhenParameterIsMissing(ParamNames.Team)
                .Do(context => Result.AskFor(ParamNames.Team, "What team would you like?".AsPrompt()));

            return assistant;
        }

        public AssistantTest(ITestOutputHelper output) : base(output)
        {
        }
    }
}