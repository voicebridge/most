using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using VoiceBridge.Most.VoiceModel.Alexa.APL;

namespace VoiceBridge.Most.VoiceModel.Alexa
{
    public static class AlexaConstants
    {
        public static class SessionTerminationReasons
        {
            public const string UserInitiated = "USER_INITIATED";
            public const string Error = "Error";
            public const string MaxPrepromptsExceeded = "EXCEEDED_MAX_REPROMPTS";
        }

        public static class ErrorType
        {
            public const string InvalidResponse = "INVALID_RESPONSE";
            public const string DeviceCommunicationError = "DEVICE_COMMUNICATION_ERROR";
            public const string InternalError = "INTERNAL_ERROR";
        }

        public static class DeviceInterfaceNames
        {
            public const string Display = "Display";
            public const string AlexaPresentationLanguage = "Alexa.Presentation.APL";
            public const string AudioPlayer = "AudioPlayer";
        }
        
        public static class AudioPlayer
        {
            public static class AudioPlayerBehavior
            {
                public const string ReplaceAll = "REPLACE_ALL";
                public const string Enqueue = "ENQUEUE";
                public const string ReplaceEnqueued = "REPLACE_ENQUEUED";
            }
            
            public static class Status
            {
                public const string Idle = "IDLE";
                public const string Paused = "PAUSED";
                public const string Playing = "PLAYING";
                public const string BufferUnderrun = "BUFFER_UNDERRUN";
                public const string Finished = "FINISHED";
                public const string Stopped = "STOPPED";
            }

            public static class Directives
            {
                public const string Play = "AudioPlayer.Play";
                public const string Stop = "AudioPlayer.Stop";
                public const string ClearQueue = "AudioPlayer.ClearQueue";
            }
        }
        
        
        public static class Dialog
        {
            public const string Delegate = "Dialog.Delegate";
            public const string ElicitSlot = "Dialog.ElicitSlot";
            public const string ConfirmSlot = "Dialog.ConfirmSlot";
            public const string ConfirmIntent = "Dialog.ConfirmIntent";
            
            public static class Status
            {
                public const string Completed  = "COMPLETED";
                public const string Started    = "STARTED";
                public const string InProgress = "IN_PROGRESS";
            }

            public static class SlotStatus
            {
                public const string Denied = "DENIED";
                public const string Confirmed = "CONFIRMED";
                public const string None = "NONE";
            }
        }

        public static class Presentation
        {
            public static class Theme
            {
                public const string Auto = "auto";
                public const string Light = "light";
                public const string Dark = "dark";
            }

            public static class Directions
            {
                public const string Column = "column";
                public const string Row = "row";
            }

            public static class Alignment
            {
                public const string Bottom = "bottom";
                public const string BottomLeft = "bottom-left";
                public const string BottomRight = "bottom-right";
                public const string Center = "center";
                public const string Left = "left";
                public const string Right = "right";
                public const string Top = "top";
                public const string TopLeft = "top-left";
                public const string TopRight = "top-right";
            }

            public static class Justification
            {
                public const string Start = "start";
                public const string End = "end";
                public const string Center = "center";

                /*
                 * TODO:
                 *   The APL documentation hints at using the flexbox specification here, but the default is "start"
                 *   (versus 'flex-start' in the CSS spec), so these values need testing on-device
                 *   See here: https://developer.amazon.com/docs/alexa-presentation-language/apl-container.html#justifycontent
                 */

                //public const string Between = "space-between";
                //public const string Around = "space-around";
                //public const string Evenly = "space-evenly";
            }

            public static class Scale
            {
                public const string None = "none";
                public const string Fill = "fill";
                public const string BestFill = "best-fill";
                public const string BestFit = "best-fit";
                public const string BestFitDown = "best-fit-down";
            }

            public static class Directives
            {
                public const string RenderDocument = "Alexa.Presentation.APL.RenderDocument";
            }

            public static class TemplateItems
            {
                public static class ViewportClauses
                {
                    public const string SmallRound = "${@viewportProfile == @hubRoundSmall}";
                    public const string MediumRectangle = "${@viewportProfile == @hubLandscapeMedium}";
                    public const string LargeRectangle = "${@viewportProfile == @hubLandscapeLarge}";
                    public const string ExtraLargeRectangle = "${@viewportProfile == @tvLandscapeXLarge}";
                }

                public static class Types
                {
                    public const string Container = "Container";
                    public const string Image = "Image";
                }
            }
        }

        public static class BuiltInIntents
        {
            public const string CancelIntent = "AMAZON.CancelIntent";
            public const string HelpIntent = "AMAZON.HelpIntent";
            public const string StopIntent = "AMAZON.StopIntent";
            public const string StartOverIntent = "AMAZON.StartOverIntent";
            public const string NoIntent = "AMAZON.NoIntent";
            public const string YesIntent = "AMAZON.YesIntent";
            public const string RepeatIntent = "AMAZON.RepeatIntent";
            public const string PauseIntent = "AMAZON.PauseIntent";
            public const string PreviousIntent = "AMAZON.PreviousIntent";
            public const string NextIntent = "AMAZON.NextIntent";
        }

        public static class RequestType
        {
            public const string LaunchRequest = "LaunchRequest";
            public const string IntentRequest = "IntentRequest";
            public const string SessionEndedRequest = "SessionEndedRequest";
            public const string CanFulfillIntentRequest = "CanFulfillIntentRequest";
            public const string AlexaPresentationLanguageUserEvent = "Alexa.Presentation.APL.UserEvent";
            public const string AlexaDisplayElementSelected = "Display.ElementSelected";
        }

        public static class SlotResolutionStatus
        {
            public const string SuccessfulMatch = "ER_SUCCESS_MATCH";
            public const string FailedMatch = "ER_SUCCESS_NO_MATCH";
        }

        public static class OutputType
        {
            public const string SSMLSpeech = "SSML";
            public const string PlainText  = "PlainText";
        }

        public const string AlexaVersion = "1.0";
    }
}