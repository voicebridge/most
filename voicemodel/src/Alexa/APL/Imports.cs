using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceBridge.Most.VoiceModel.Alexa.APL
{
    public static class Imports
    {
        public static Import ViewportProfiles
        {
            get
            {
                return new Import()
                {
                    Name = "alexa-viewport-profiles",
                    Version = "1.0.0"
                };
            }
        }
    }
}
