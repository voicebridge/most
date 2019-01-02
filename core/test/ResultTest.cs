using System.Threading.Tasks;
using VoiceBridge.Most.Directives;
using Xunit;

namespace VoiceBridge.Most.Test
{
    public class ResultTest
    {
        [Fact]
        public void Say()
        {
            var prompt = new Prompt();
            var directive = Result.Say(prompt, true) as SayDirective;
            Assert.NotNull(directive);
            Assert.Same(prompt, directive.Prompt);
            Assert.True(directive.KeepSessionOpen);
        }

        [Fact]
        public async Task SayOnIntent()
        {
            var prompt = new Prompt();
            var directive = await DynamicHandlerHelper.ExecuteHandle<SayDirective>(intent => intent.Say(prompt, true));
            Assert.Same(prompt, directive.Prompt);
            Assert.True(directive.KeepSessionOpen);
        }

        [Fact]
        public void Ask()
        {
            var prompt = new Prompt();
            const string paramName = "p";
            const string expectedIntent = "t";
            var directive = (AskForValueDirective)Result.AskFor(paramName, prompt, expectedIntent);
            Assert.Same(prompt, directive.Prompt);
            Assert.Equal(paramName, directive.ParameterName);
            Assert.Equal(expectedIntent, directive.ExpectedIntentName);
        }

        [Fact]
        public async Task AskOnIntent()
        {
            var prompt = new Prompt();
            const string paramName = "p";
            const string expectedIntent = "t";
            var directive =
                await DynamicHandlerHelper.ExecuteHandle<AskForValueDirective>(intent =>
                    intent.AskFor(paramName, prompt, expectedIntent));
            Assert.Same(prompt, directive.Prompt);
            Assert.Equal(paramName, directive.ParameterName);
            Assert.Equal(expectedIntent, directive.ExpectedIntentName);
        }
        
        [Fact]
        public void PlayAudio()
        {
            var prompt = new Prompt();
            var media = new Media();
            var directive = (PlayMediaDirective) Result.PlayAudio(media, prompt, true);
            Assert.Same(prompt, directive.Prompt);
            Assert.Same(media, media);
            Assert.True(directive.KeepSessionOpen);
        }

        [Fact]
        public async Task PlayAudioOnIntent()
        {
            var media = new Media();
            var prompt = new Prompt();
            var directive = await DynamicHandlerHelper.ExecuteHandle<PlayMediaDirective>(intent => intent.PlayAudio(media, prompt));
            Assert.Same(media, directive.Media);
            Assert.Same(prompt, directive.Prompt);
        }
    }
}