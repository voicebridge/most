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
            var directive = Result.Say(prompt) as SayDirective;
            Assert.NotNull(directive);
            Assert.Same(prompt, directive.Prompt);
        }

        [Fact]
        public async Task SayOnIntent()
        {
            var prompt = new Prompt();
            var directive = await DynamicHandlerHelper.ExecuteHandle<SayDirective>(intent => intent.Say(prompt));
            Assert.Same(prompt, directive.Prompt);
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
            var directive = (PlayMediaDirective) Result.PlayAudio(media, prompt);
            Assert.Same(prompt, directive.Prompt);
            Assert.Same(media, media);
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


        [Fact]
        public void KeepSessionOpen()
        {
            var directive = (SessionDirective)Result.KeepSessionOpen();
        }

        [Fact]
        public async Task KeepSessionOpenOnIntent()
        {
            var directive = await DynamicHandlerHelper.ExecuteHandle<SessionDirective>(intent => intent.KeepSessionOpen());
        }
    }
}