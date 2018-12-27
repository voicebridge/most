using System.Security.Cryptography.X509Certificates;

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
        
        public static class AudioPlayerStatus
        {
            public const string Idle = "IDLE";
            public const string Paused = "PAUSED";
            public const string Playing = "PLAYING";
            public const string BufferUnderrun = "BUFFER_UNDERRUN";
            public const string Finished = "FINISHED";
            public const string Stopped = "STOPPED";
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