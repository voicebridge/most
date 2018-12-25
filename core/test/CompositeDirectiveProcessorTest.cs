using VoiceBridge.Most.VoiceModel.Alexa;
using Xunit;
using Xunit.Abstractions;

namespace VoiceBridge.Most.Test
{
    public class CompositeDirectiveProcessorTest : TestBase
    {
        private readonly SkillResponse response = new SkillResponse();
        
        [Fact]
        public void CanHandlePositive()
        {
            var vDirective1 = Util.QuickStub<IVirtualDirective>();
            var vDirective2 = Util.QuickStub<IVirtualDirective>();
            var processor1 = Util.CreateDirectiveProcessor(response, vDirective1);
            var processor2 = Util.CreateDirectiveProcessor(response, vDirective2);
            var processor = new CompositeDirectiveProcessor<SkillRequest, SkillResponse>(Util.ToEnumerable(processor2, processor1), this);
            Assert.True(processor.CanHandle(vDirective1));
        }
        
        [Fact]
        public void CanHandleNegative()
        {
            var vDirective1 = Util.QuickStub<IVirtualDirective>();
            var vDirective2 = Util.QuickStub<IVirtualDirective>();
            var vDirective3 = Util.QuickStub<IVirtualDirective>();
            var processor1 = Util.CreateDirectiveProcessor(response, vDirective1);
            var processor2 = Util.CreateDirectiveProcessor(response, vDirective2);
            var processor = new CompositeDirectiveProcessor<SkillRequest, SkillResponse>(Util.ToEnumerable(processor2, processor1), this);
            Assert.False(processor.CanHandle(vDirective3));
        }
        
        [Fact]
        public void SuitableProcessorsAreInvoked()
        {
            var vDirective1 = Util.QuickStub<IVirtualDirective>();
            var vDirective2 = Util.QuickStub<IVirtualDirective>();
            var processor1 = Util.CreateDirectiveProcessor(response, vDirective2);
            var processor2 = Util.CreateDirectiveProcessor(response, vDirective1);
            var processor3 = Util.CreateDirectiveProcessor(response, vDirective2);
            var processor =
                new CompositeDirectiveProcessor<SkillRequest, SkillResponse>(Util.ToEnumerable(processor2, processor1, processor3), this);
            processor.Process(vDirective2, null, response);
            processor1.Verify(x => x.Process(vDirective2, null, response));
            processor3.Verify(x => x.Process(vDirective2, null, response));
            processor2.Verify(x => x.Process(vDirective2, null, response), Moq.Times.Never);
        }

        public CompositeDirectiveProcessorTest(ITestOutputHelper output) : base(output)
        {
        }
    }
}