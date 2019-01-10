namespace VoiceBridge.Most
{
    /// <summary>
    /// Describes current state of Audio playback
    /// </summary>
    public class AudioPlayerInfo
    {
        /// <summary>
        /// Current Audio Player state. On Google Assistant, only Active, Finished and Failed
        /// available
        /// </summary>
        public AudioPlayerState State { get; set; }
        
        /// <summary>
        /// Gets the current playback offset (at the time when this request was received).
        /// This property is only available on Alexa
        /// </summary>
        public int? CurrentOffsetInMilliseconds { get; set; }
    }
}