namespace VoiceBridge.Most.Directives
{
    internal class AskForValueDirective : IVirtualDirective
    {
        public string ParameterName { get; set; }
        
        public Prompt Prompt { get; set; }
        
        
        public string ExpectedIntentName { get; set; }
    }
}