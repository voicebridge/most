using System;
using System.Threading.Tasks;
using VoiceBridge.Common;
using VoiceBridge.Most.Directives;
using Xunit;

namespace VoiceBridge.Most.Test
{
    public class ResultTest
    {
        private const string IMAGE_URL_1 = "https://images-na.ssl-images-amazon.com/images/I/61GcN5yX6XL._SL1000_.jpg";
        private const string IMAGE_URL_2 = "https://images-na.ssl-images-amazon.com/images/I/6182S7MYC2L._SL1000_.jpg";
        private const string IMAGE_URL_3 = "https://images-na.ssl-images-amazon.com/images/I/61yI7vWa83L._SL1000_.jpg";
        private const string IMAGE_URL_4 = "https://images-na.ssl-images-amazon.com/images/I/51FQBzDZ3VL.jpg";


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
        public void ShowImage()
        {
            var image = IMAGE_URL_1.AsImage();
            var directive = Result.ShowImage(image) as ImageDirective;

            Assert.NotNull(directive);
            Assert.True(image is Image);
            Assert.Same(image, directive.Image);

            image = new Image() { ImageUri = new SecureUri(IMAGE_URL_1) };
            directive = Result.ShowImage(image) as ImageDirective;

            Assert.NotNull(directive);
            Assert.True(directive.Image is Image);
            Assert.Same(image, directive.Image);
        }

        [Fact]
        public async Task ShowImageOnIntent()
        {
            var image = IMAGE_URL_1.AsImage();
            var directive = await DynamicHandlerHelper.ExecuteHandle<ImageDirective>(intent => intent.ShowImage(image));
            
            Assert.NotNull(directive);
            Assert.True(image is Image);
            Assert.Same(image, directive.Image);

            image = new Image() { ImageUri = new SecureUri(IMAGE_URL_1) };
            directive = await DynamicHandlerHelper.ExecuteHandle<ImageDirective>(intent => intent.ShowImage(image));

            Assert.NotNull(directive);
            Assert.True(directive.Image is Image);
            Assert.Same(image, directive.Image);
        }

        [Fact]
        public void ShowImageResponsive()
        {
            var image = new ResponsiveImage()
            {
                SmallImageUri = new SecureUri(IMAGE_URL_1),
                MediumImageUri = new SecureUri(IMAGE_URL_2),
                LargeImageUri = new SecureUri(IMAGE_URL_3),
                ExtraLargeImageUri = new SecureUri(IMAGE_URL_4)
            };

            var directive = Result.ShowImage(image) as ImageDirective;

            Assert.NotNull(directive);
            Assert.True(directive.Image is ResponsiveImage);
            Assert.Same(image, directive.Image);
        }

        [Fact]
        public async Task ShowImageResponsiveOnIntent()
        {
            var image = new ResponsiveImage()
            {
                SmallImageUri = new SecureUri(IMAGE_URL_1),
                MediumImageUri = new SecureUri(IMAGE_URL_2),
                LargeImageUri = new SecureUri(IMAGE_URL_3),
                ExtraLargeImageUri = new SecureUri(IMAGE_URL_4)
            };

            var directive = await DynamicHandlerHelper.ExecuteHandle<ImageDirective>(intent => intent.ShowImage(image));

            Assert.NotNull(directive);
            Assert.True(directive.Image is ResponsiveImage);
            Assert.Same(image, directive.Image as ResponsiveImage);
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