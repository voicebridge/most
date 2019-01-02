namespace VoiceBridge.Most.Directives
{
    public class PlayMediaDirective : IVirtualDirective
    {
        public PlayMediaDirective(Media media, Prompt prompt)
        {
            this.Prompt = prompt;
            this.Media = media;
        }

        public bool KeepSessionOpen { get; set; }
        
        public Prompt Prompt { get; }

        public Media Media { get; }
    }
}