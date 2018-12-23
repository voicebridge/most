using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Moq;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Test
{
    public static class Util
    {
        public static T QuickMock<T>() where T : class
        {
            return new Mock<T>().Object;
        }

        public static Mock<IRequestHandler> CreateHandler(ConversationContext target)
        {    
            var m = new Mock<IRequestHandler>();
            m.Setup(x => x.CanHandle(target)).Returns(true);      
            m.Setup(y => y.Handle(target)).Returns(Task.Run(() => NoOpAction()));
            return m;
        }

        public static IEnumerable<T> ToEnumerable<T>(params Mock<T>[] mocks) where T : class
        {
            foreach (var m in mocks)
            {
                yield return m.Object;
            }
        }
        
        public static Mock<IResponseBuilder<SkillResponse>>CreateResponseBuilder(ConversationContext target)
        {
            return CreateResponseBuilder(target, new SkillResponse());
        }

        public static Mock<IResponseBuilder<SkillResponse>> CreateResponseBuilder(ConversationContext target, SkillResponse response)
        {    
            var m = new Mock<IResponseBuilder<SkillResponse>>();
            m.Setup(x => x.CanHandle(target)).Returns(true);      
            m.Setup(y => y.Build(target)).Returns(response);
            return m;
        }

        public static Mock<IDirectiveProcessor<SkillRequest, SkillResponse>> CreateDirectiveProcessor(
            SkillResponse response,
            IVirtualDirective directive,
            Action<IVirtualDirective, SkillResponse> action = null)
        {
            var m = new Mock<IDirectiveProcessor<SkillRequest, SkillResponse>>();
            m.Setup(x => x.CanHandle(directive)).Returns(true);
            var setup = m.Setup(x => x.Process(directive, It.IsAny<SkillRequest>(), response));
            if (action != null)
            {
                setup.Callback(action);
            }
            return m;
        }

        public static Mock<IInputModelBuilder<SkillRequest>> CreateInputModelBuilder(ConversationContext context, SkillRequest target)
        {    
            var m = new Mock<IInputModelBuilder<SkillRequest>>();
            m.Setup(y => y.Build(context, target));
            return m;
        }

        public static ParameterValue ToParameterValue(this string s)
        {
            return new ParameterValue
            {
                ProvidedValue = s,
                ResolvedId = s,
                ResolvedValue = s
            };
        }
        
        private static void NoOpAction(){}
    }
}