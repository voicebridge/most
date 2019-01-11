using VoiceBridge.Common;

namespace VoiceBridge.Most
{
    public static class PublicExtensions
    {
        public static Prompt AsPrompt(this string s)
        {
            return new Prompt
            {
                Id = "transient-prompt",
                Content = s,
                IsSSML = false
            };
        }

        public static IImage AsImage(this string s, string caption = "")
        {
            return new Image
            {
                ImageUri = new SecureUrl(s)
                // Caption = caption
            };
        }
    }
}