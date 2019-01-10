using System;
using System.Collections.Generic;
using System.Text;
using VoiceBridge.Most.Directives;
using VoiceBridge.Most.Directives.Processors;
using VoiceBridge.Most.VoiceModel.Alexa;
using VoiceBridge.Most.VoiceModel.Alexa.APL;
using VoiceBridge.Most.VoiceModel.Alexa.Directives;
using Xunit;

namespace VoiceBridge.Most.Test.Directives.Processors
{
    public abstract class ImageTestBase
    {
        protected abstract IImage GetImage();

        protected IImage ExecuteProcessor(Action<IVirtualDirective, ImageProcessor> action, IImage image = null)
        {
            var item = (image == null) ? GetImage() : image;
            var virtualDirective = new ImageDirective()
            {
                Image = item
            };

            var processor = new ImageProcessor();
            action(virtualDirective, processor);
            return item;
        }

        protected void TestAplDocument(ShowImageDirective directive)
        {
            Assert.NotNull(directive);
            Assert.Null(directive.DataSources);
            Assert.Equal(AlexaConstants.Presentation.Directives.RenderDocument, directive.Type);
            Assert.Equal("1.0", directive.Document.Version);
            Assert.Equal("APL", directive.Document.Type);
            Assert.Equal("auto", directive.Document.Theme);
            Assert.Empty(directive.Document.Resources);
            Assert.Single(directive.Document.Import);
            Assert.Equal("1.0.0", directive.Document.Import[0].Version);
            Assert.Equal("alexa-viewport-profiles", directive.Document.Import[0].Name);
            Assert.Empty(directive.Document.MainTemplate.Parameters);
            Assert.Single(directive.Document.MainTemplate.Items);
        }

        protected void TestParentContainer(TemplateContainer container, int expectedChildren)
        {
            Assert.Equal(expectedChildren, container.Items.Count);
            Assert.True(string.IsNullOrWhiteSpace(container.AlignItems));
            Assert.Equal("column", container.Direction);
            Assert.Equal(0, container.Grow);
            Assert.True(string.IsNullOrWhiteSpace(container.JustifyContent));
            Assert.Equal("Container", container.Type);
            Assert.Null(container.When);
        }

        protected void TestContainerWithImage(TemplateContainer container, string url, string when = null)
        {
            Assert.Single(container.Items);
            Assert.Equal("center", container.AlignItems);
            Assert.True(string.IsNullOrWhiteSpace(container.Direction));
            Assert.Equal(1, container.Grow);
            Assert.Equal("center", container.JustifyContent);
            Assert.Equal("Container", container.Type);

            if (when == null)
            {
                Assert.Null(container.When);
            }
            else
            {
                Assert.Equal(when, container.When);
            }

            var tImage = container.Items[0] as TemplateImage;
            Assert.Equal("center", tImage.Align);
            Assert.Equal("100vh", tImage.Height);
            Assert.Equal("best-fill", tImage.Scale);
            Assert.Equal(url, tImage.Source);
            Assert.Equal("Image", tImage.Type);
            Assert.Equal("100vw", tImage.Width);
        }
    }
}
