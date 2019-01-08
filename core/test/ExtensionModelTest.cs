using Xunit;

namespace VoiceBridge.Most.Test
{
    public class ExtensionModelTest
    {
        [Fact]
        public void AddGetNonNamedModel()
        {
            var model = "m";
            var extension = new ExtensionModel();
            extension.Add(model);
            Assert.Same(model, extension.Get<string>());
        }
        
        [Fact]
        public void AddGetNamedModel()
        {
            var model = "m";
            var model2 = "m2";
            var extension = new ExtensionModel();
            extension.Add(model);
            extension.Add(model2, "model2");
            Assert.Same(model, extension.Get<string>());
            Assert.Same(model2, extension.Get<string>("model2"));
        }

        [Fact]
        public void IsAvailable()
        {
            var extension = new ExtensionModel();
            extension.Add("m");
            Assert.True(extension.IsAvailable<string>());
            Assert.False(extension.IsAvailable<int>());
        }
        
        [Fact]
        public void IsAvailableByName()
        {
            var extension = new ExtensionModel();
            extension.Add("m", "primary");
            Assert.True(extension.IsAvailable<string>("primary"));
            Assert.False(extension.IsAvailable<string>());
        }
    }
}