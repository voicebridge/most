namespace VoiceBridge.Most
{
    /// <summary>
    /// Describes device capabilities
    /// </summary>
    public enum DeviceCapability
    {
        /// <summary>
        /// Device capable of receiving and sending audio
        /// </summary>
        Audio,
        
        /// <summary>
        /// Device has a display
        /// </summary>
        Display,
        
        /// <summary>
        /// Device supports Alexa Presentation Language (Alexa only)
        /// </summary>
        AlexaPresentationLanguage,
        
        /// <summary>
        /// Device supports streaming media
        /// </summary>
        StreamMedia
    }
}