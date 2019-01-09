using System.Collections.Generic;
using System.Linq;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK;
using VoiceBridge.Most.VoiceModel.GoogleAssistant.DialogFlow;

namespace VoiceBridge.Most.Google
{
    public class GoogleCapabilitiesInputModelBuilder : IInputModelBuilder<AppRequest>
    {
        private Dictionary<string, DeviceCapability> mapping = new Dictionary<string, DeviceCapability>
        {
            {GoogleAssistantConstants.Capabilities.AudioOut, DeviceCapability.Audio},
            {GoogleAssistantConstants.Capabilities.ScreenOut, DeviceCapability.Display},
            {GoogleAssistantConstants.Capabilities.AudioMediaResponse, DeviceCapability.StreamMedia}
        };
        
        public void Build(ConversationContext context, AppRequest request)
        {
            if (request.OriginalDetectIntentRequest?.Content?.Surface?.Capabilities == null)
            {
                return;
            }
            var list = new List<DeviceCapability>();
            var deviceList = request.OriginalDetectIntentRequest.Content.Surface.Capabilities;
            foreach (var capability in deviceList)
            {
                if (mapping.ContainsKey(capability.Name))
                {
                    list.Add(mapping[capability.Name]);
                }
            }

            context.Capabilities = list;
        }
    }
}