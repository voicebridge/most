namespace VoiceBridge.Most.Directives
{
    internal class PlayMediaDirective : IVirtualDirective
    {
        public PlayMediaDirective(Media media, Prompt prompt)
        {
            this.Prompt = prompt;
            this.Media = media;
        }

        public Prompt Prompt { get; }

        public Media Media { get; }
    }
}