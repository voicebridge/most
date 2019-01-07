using System;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceBridge.Most.Test
{
    public static class DynamicHandlerHelper
    {
        public static async Task<TDirective> ExecuteHandle<TDirective>(Action<IntentConfiguration> action)
            where TDirective : IVirtualDirective
        {
            var context = new ConversationContext
            {
                RequestType = RequestType.Intent, RequestModel = {IntentName = "one"}
            };
            
            var intent = new IntentConfiguration(context.RequestModel.IntentName);
            action(intent);
            await intent.Handle(context);
            return (TDirective)context.OutputDirectives.Single(x => x is TDirective);
        }
    }
}