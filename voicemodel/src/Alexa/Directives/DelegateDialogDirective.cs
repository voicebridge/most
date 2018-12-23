namespace VoiceBridge.Most.VoiceModel.Alexa.Directives
{

    public class DelegateDialogDirective : DialogDirectiveBase
    {
        public DelegateDialogDirective()
        {
        }

        public override string Type => AlexaConstants.Dialog.Delegate;
    }

}