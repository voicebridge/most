using System.Collections.Generic;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Describes an utterance that the device will speak
    /// </summary>
    public class Prompt
    {
        /// <summary>
        /// (Reserved for future use)
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Gets or sets content of the prompt (Can be either SSML or plain text)
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// Gets or sets whether the content is SSML
        /// </summary>
        public bool IsSSML { get; set; }
    }
}