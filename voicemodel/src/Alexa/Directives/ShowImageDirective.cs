using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using VoiceBridge.Most.VoiceModel.Alexa.APL;

namespace VoiceBridge.Most.VoiceModel.Alexa.Directives
{
    public class ShowImageDirective : AplDirectiveBase
    {
        public override string Type => AlexaConstants.Presentation.Directives.RenderDocument;

        public override List<DataSource> DataSources => null;

        public ShowImageDirective()
        {
            Document.Import.Add(Imports.ViewportProfiles);
        }
    }

}
