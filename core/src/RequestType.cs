namespace VoiceBridge.Most
{
    public enum RequestType
    {
        /// <summary>
        /// Generic launch request (MAIN intent in Google Actions)
        /// </summary>
        Launch,
        
        /// <summary>
        /// Specific intent has been triggered
        /// </summary>
        Intent,
        
        /// <summary>
        /// Error has occured
        /// </summary>
        Error,
        
        /// <summary>
        /// User has terminated the flow (example: Alexa stop)
        /// </summary>
        UserInitiatedTermination,
        
        /// <summary>
        /// Can Fulfill Query. This is only used by Alexa
        /// </summary>
        FulfillmentQuery
    }
}