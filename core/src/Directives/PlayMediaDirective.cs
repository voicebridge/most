namespace VoiceBridge.Most.Directives
{
    public class PlayMediaDirective : IVirtualDirective
    {
        private readonly Media media;

        public PlayMediaDirective(Media media)
        {
            this.media = media;
        }
    }
}