using Newtonsoft.Json;
using VoiceBridge.Most.VoiceModel.Alexa.Directives.Dialog;

namespace VoiceBridge.Most.VoiceModel.Alexa.Directives
{
    public class ElicitSlotDialogDirective : DialogDirectiveBase
    {
        public ElicitSlotDialogDirective(string slotToElicit)
        {
            this.SlotToElicit = slotToElicit;
        }

        public override string Type => AlexaConstants.Dialog.ElicitSlot;
        
        [JsonProperty("slotToElicit")] 
        public string SlotToElicit { get; private set; }
    }
}