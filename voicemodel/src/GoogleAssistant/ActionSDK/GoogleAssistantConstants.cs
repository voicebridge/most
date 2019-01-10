namespace VoiceBridge.Most.VoiceModel.GoogleAssistant.ActionSDK
{
    public static class GoogleAssistantConstants
    {
        public static class Permission
        {
            public const string Unspecified = "UNSPECIFIED_PERMISSION";
            public const string UsersName = "NAME";
            public const string DeviceGeoLocation = "DEVICE_PRECISE_LOCATION";
            public const string DevicePhysicalAddress = "DEVICE_COARSE_LOCATION";
            public const string SendUpdates = "UPDATE";
        }

        public static class SkuType
        {
            public const string Unspecified = "TYPE_UNSPECIFIED";
            public const string InAppPurchase = "IN_APP";
            public const string Subscriptions = "SUBSCRIPTIONS";
            public const string PaidApps = "APP";
        }

        public static class ConversationType
        {
            public const string Unspecified = "TYPE_UNSPECIFIED";
            public const string New = "NEW";
            public const string Active = "ACTIVE";
        }

        public static class InputType
        {
            public const string Unspecified = "UNSPECIFIED_INPUT_TYPE";
            public const string Touch = "TOUCH";
            public const string Voice = "VOICE";
            public const string Keyboard = "KEYBOARD";
            public const string Url = "URL";
        }

        public static class UrlTypeHint
        {
            public const string Unspecified = "URL_TYPE_HINT_UNSPECIFIED";
            public const string AcceleratedMobilePages = "AMP_CONTENT";
        }

        public static class ImageDisplayOptions
        {
            public const string Default = "DEFAULT";
            public const string WhiteFill = "WHITE";
            public const string Cropped = "CROPPED";
        }

        public static class MediaType
        {
            public const string Unspecified = "MEDIA_TYPE_UNSPECIFIED";
            public const string Audio = "AUDIO";
        }

        public static class HorizontalAlignment
        {
            public const string Leading = "LEADING";
            public const string Center = "CENTER";
            public const string Trailing = "TRAILING";
        }

        public static class CommonIntents
        {
            public const string MediaStatusChange = "actions.intent.MEDIA_STATUS";
            public const string Text = "actions.intent.TEXT";
            public const string Main = "actions.intent.MAIN";
            public const string Cancel = "actions.intent.CANCEL";
            public const string ConfigureUpdates = "actions.intent.CONFIGURE_UPDATES";
            public const string AskForConfirmation = "actions.intent.CONFIRMATION";
            public const string AskForDateTime = "actions.intent.DATETIME";
            public const string AskForDeliveryAddress = "actions.intent.DELIVERY_ADDRESS";
            public const string AskForHandoffPermission = "actions.intent.NEW_SURFACE";
            public const string NoInput = "actions.intent.NO_INPUT";
            public const string AskForCarouselItem = "actions.intent.OPTION";
            public const string AskForUserInfoPermission = "actions.intent.PERMISSION";
            public const string AskForAccountLinking = "actions.intent.SIGN_IN";
            public const string CheckTxRequirementsMet = "actions.intent.TRANSACTION_REQUIREMENTS_CHECK";
            public const string AskToConfirmTransaction = "actions.intent.TRANSACTION_DECISION";
            public const string AskToRegisterForUpdates = "actions.intent.REGISTER_UPDATE";
        }

        public static class EventNames
        {
            public const string DisplayElementSelection = "actions_intent_OPTION";
            public const string MediaStatusChange = "actions_intent_MEDIA_STATUS";
        }

        public static class SmartHomeIntents
        {
            public const string GetDeviceList = "action.devices.SYNC";
            public const string GetStateOfDevices = "action.devices.QUERY";
            public const string ExecuteCommand = "action.devices.EXECUTE";
            public const string OnUserUnlinks = "action.devices.DISCONNECT";
        }

        public static class Capabilities
        {
            public const string WebBrowser = "actions.capability.WEB_BROWSER";
            public const string AudioOut = "actions.capability.AUDIO_OUTPUT";
            public const string ScreenOut = "actions.capability.SCREEN_OUTPUT";
            public const string AudioMediaResponse = "actions.capability.MEDIA_RESPONSE_AUDIO";
        }
    }
}