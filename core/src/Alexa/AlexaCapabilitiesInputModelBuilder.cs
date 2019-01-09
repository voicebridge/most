using System.Collections.Generic;
using VoiceBridge.Most.VoiceModel.Alexa;

namespace VoiceBridge.Most.Alexa
{
    public class AlexaCapabilitiesInputModelBuilder : IInputModelBuilder<SkillRequest>
    {
        public void Build(ConversationContext context, SkillRequest request)
        {
            var list = new List<DeviceCapability>
            {
                DeviceCapability.Audio
            };
            context.Capabilities = list;
            
            if (request.Context?.System?.Device?.SupportedInterfaces == null)
            {
                return;
            }

            if (request.Context.System.Device.SupportedInterfaces.ContainsKey(AlexaConstants.DeviceInterfaceNames
                .Display))
            {
                list.Add(DeviceCapability.Display);
            }

            if (request.Context.System.Device.SupportedInterfaces.ContainsKey(AlexaConstants.DeviceInterfaceNames
                .AlexaPresentationLanguage))
            {
                list.Add(DeviceCapability.AlexaPresentationLanguage);
            }

            if (request.Context.System.Device.SupportedInterfaces.ContainsKey(AlexaConstants.DeviceInterfaceNames
                .AudioPlayer))
            {
                list.Add(DeviceCapability.StreamMedia);
            }
        }
    }
}