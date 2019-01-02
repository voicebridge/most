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
    }
}