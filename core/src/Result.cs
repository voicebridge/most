using System;
using VoiceBridge.Most.Directives;
using VoiceBridge.Most.VoiceModel.Alexa.LanguageModel;

namespace VoiceBridge.Most
{
    /// <summary>
    /// Fluent methods to construct responses to intents
    /// </summary>
    public static class Result
    {
        /// <summary>
        /// Creates a virtual directive to send a prompt back to the user
        /// </summary>
        /// <param name="prompt">Prompt</param>
        /// <returns>SayDirective</returns>
        public static IVirtualDirective Say(Prompt prompt)
        {
            return new SayDirective
            {
                Prompt = prompt
            };
        }
        

        /// <summary>
        /// Should intent conditions be satisfied, a prompt is sent back to the user
        /// </summary>
        /// <param name="intent">IntentConfiguration</param>
        /// <param name="prompt">Prompt</param>
        /// <returns>SayDirective</returns>
        public static IRequestHandlerBuilder Say(
            this IRequestHandlerBuilder intent, 
            Prompt prompt)
        {
            return ApplyAction(intent, Say(prompt));
        }


        /// <summary>
        /// Should intent conditions be satisfied, user will be asked to fill a slot with provided prompt
        /// </summary>
        /// <param name="intent">IntentConfiguration</param>
        /// <param name="parameterName">Parameter (slot) name</param>
        /// <param name="prompt">Prompt to use</param>
        /// <param name="expectedIntentName">Intent name hint (used by Google Actions)</param>
        /// <returns>Itself</returns>
        public static IRequestHandlerBuilder AskFor(
            this IRequestHandlerBuilder intent, 
            string parameterName,
            Prompt prompt, 
            string expectedIntentName = null)
        {
            return ApplyAction(intent, 
                AskFor(parameterName, prompt, expectedIntentName));
        }


        /// <summary>
        /// Create a virtual directive to ask user to fill a slot (parameter)
        /// </summary>
        /// <param name="parameterName">Parameter (slot) name</param>
        /// <param name="prompt">Prompt to use</param>
        /// <param name="expectedIntentName">Intent name hint (used by Google Actions)</param>
        /// <returns>AskForValueDirective</returns>
        public static IVirtualDirective AskFor(string parameterName,
            Prompt prompt,
            string expectedIntentName = null)
        {
            return new AskForValueDirective
            {
                ParameterName = parameterName,
                Prompt = prompt,
                ExpectedIntentName = expectedIntentName
            };
        }


        /// <summary>
        /// Should intent conditions be satisfied, audio media will be played on the user's device
        /// </summary>
        /// <param name="handlerBuilder">IntentConfiguration</param>
        /// <param name="media">Audio Media</param>
        /// <param name="prompt">Prompt to play before the audio</param>
        /// <returns>Itself</returns>
        public static IRequestHandlerBuilder PlayAudio(
            this IRequestHandlerBuilder handlerBuilder,
            Media media, 
            Prompt prompt)
        {
            return ApplyAction(handlerBuilder, PlayAudio(media, prompt));
        }
        

        /// <summary>
        /// Create a virtual directive to play audio media
        /// </summary>
        /// <param name="media">Media</param>
        /// <param name="prompt">Prompt to play before the audio</param>
        /// <returns>PlayAudioDirective</returns>
        public static IVirtualDirective PlayAudio(
            Media media, 
            Prompt prompt)
        {
            return new PlayMediaDirective(media, prompt);
        }


        /// <summary>
        /// Should intent conditions be satisfied, display an image on the user's device (if supported)
        /// </summary>
        /// <param name="handlerBuilder">Instance</param>
        /// <param name="image">Either an Image or ResponsiveImage</param>
        /// <returns>Itself</returns>
        public static IRequestHandlerBuilder ShowImage(
            this IRequestHandlerBuilder handlerBuilder,
            IImage image)
        {
            return ApplyAction(handlerBuilder, ShowImage(image));
        }


        /// <summary>
        /// Create a virtual directive to display an image (if supported)
        /// </summary>
        /// <param name="image">Either an Image or ResponsiveImage</param>
        /// <returns>ImageDirective</returns>
        public static IVirtualDirective ShowImage(IImage image)
        {
            return new ImageDirective
            {
                Image = image
            };
        }

        
        /// <summary>
        /// Controls whether to keep the session open or not
        /// </summary>
        /// <returns>SessionDirective</returns>
        public static IVirtualDirective KeepSessionOpen()
        {
            return new SessionDirective();
        }


        /// <summary>
        /// Controls whether to keep the session open or not
        /// </summary>
        /// <returns>Itself</returns>
        public static IRequestHandlerBuilder KeepSessionOpen(
            this IRequestHandlerBuilder intent)
        {
            return ApplyAction(intent, KeepSessionOpen());
        }


        private static IRequestHandlerBuilder ApplyAction(
            IRequestHandlerBuilder intent, 
            IVirtualDirective directive)
        {
            intent.Do(context => directive);
            return intent;
        }
    }
}