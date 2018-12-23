using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public class ResponseContent
    {
        [JsonProperty("outputSpeech", NullValueHandling = NullValueHandling.Ignore)]
        public IOutputSpeech OutputSpeech {get; set;}

        [JsonProperty("card", NullValueHandling = NullValueHandling.Ignore)]
        public IAlexaCard Card {get; set;}

        [JsonProperty("reprompt", NullValueHandling = NullValueHandling.Ignore)]
        public Reprompt Reprompt {get; set;}

        [JsonProperty("directives", NullValueHandling = NullValueHandling.Ignore)]
        public List<IAlexaDirective> Directives {get; set;} = new List<IAlexaDirective>();
        
        [JsonProperty("shouldEndSession")]
        public bool ShouldEndSession {get; set;}
    }
}