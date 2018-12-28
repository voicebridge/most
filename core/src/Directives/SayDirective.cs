namespace VoiceBridge.Most.Directives
{
    public class SayDirective : IVirtualDirective
    {
        public Prompt Prompt { get; set; }

        public bool IsQuestion { get; set; }
    }
}