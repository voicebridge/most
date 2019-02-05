namespace VoiceBridge.Most
{
    /// <summary>
    /// Parameter (Slot) value
    /// </summary>
    public class ParameterValue
    {
        /// <summary>
        /// Raw value as provided by the platform
        /// </summary>
        public string ProvidedValue { get; set; }
        
        /// <summary>
        /// If available, this will be the resolved canonical id 
        /// </summary>
        public string ResolvedId { get; set; }
        
        /// <summary>
        /// If available this will be the resolved canonical display value
        /// </summary>
        public string ResolvedValue { get; set; }
    }
}