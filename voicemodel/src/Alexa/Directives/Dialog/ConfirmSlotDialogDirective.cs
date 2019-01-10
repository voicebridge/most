using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa.Directives.Dialog
{
    public class ConfirmSlotDialogDirective : DialogDirectiveBase
    {
        public ConfirmSlotDialogDirective()
        {
        }
        
        public override string Type => AlexaConstants.Dialog.Delegate;
        
        [JsonProperty("slotToConfirm")] 
        public string SlotToElicit { get; private set; }
    }
}