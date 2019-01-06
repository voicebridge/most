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
        /// <param name="keepSessionOpen">False by default. If true, a response will be expected</param>
        /// <returns>SayDirective</returns>
        public static IVirtualDirective Say(
            Prompt prompt, 
            bool keepSessionOpen = false)
        {
            return new SayDirective
            {
                Prompt = prompt,
                KeepSessionOpen = keepSessionOpen
            };
        }
        

        /// <summary>
        /// Should intent conditions be satisfied, a prompt is sent back to the user
        /// </summary>
        /// <param name="intent">IntentConfiguration</param>
        /// <param name="prompt">Prompt</param>
        /// <param name="keepSessionOpen">False by default. If true, a response will be expected</param>
        /// <returns>SayDirective</returns>
        public static IntentConfiguration Say(
            this IntentConfiguration intent, 
            Prompt prompt, 
            bool keepSessionOpen = false)
        {
            return ApplyAction(intent, Say(prompt, keepSessionOpen));
        }


        /// <summary>
        /// Should intent conditions be satisfied, user will be asked to fill a slot with provided prompt
        /// </summary>
        /// <param name="intent">IntentConfiguration</param>
        /// <param name="parameterName">Parameter (slot) name</param>
        /// <param name="prompt">Prompt to use</param>
        /// <param name="expectedIntentName">Intent name hint (used by Google Actions)</param>
        /// <returns>Itself</returns>
        public static IntentConfiguration AskFor(
            this IntentConfiguration intent, 
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
        /// <param name="intent">IntentConfiguration</param>
        /// <param name="media">Audio Media</param>
        /// <param name="prompt">Prompt to play before the audio</param>
        /// <param name="keepSessionOpen">False by default. If true, a response will be expected</param>
        /// <returns>Itself</returns>
        public static IntentConfiguration PlayAudio(
            this IntentConfiguration intent,
            Media media, 
            Prompt prompt, 
            bool keepSessionOpen = false)
        {
            return ApplyAction(intent, PlayAudio(media, prompt, keepSessionOpen));
        }
        

        /// <summary>
        /// Create a virtual directive to play audio media
        /// </summary>
        /// <param name="media">Media</param>
        /// <param name="prompt">Prompt to play before the audio</param>
        /// <param name="keepSessionOpen">False by default. If true, a response will be expected</param>
        /// <returns>PlayAudioDirective</returns>
        public static IVirtualDirective PlayAudio(
            Media media, 
            Prompt prompt, 
            bool keepSessionOpen = false)
        {
            return new PlayMediaDirective(media, prompt)
            {
                KeepSessionOpen = keepSessionOpen
            };
        }


        /// <summary>
        /// Should intent conditions be satisfied, display an image on the user's device
        /// </summary>
        /// <param name="imageUri">The Uri of the image to display</param>
        /// <param name="accessibilityText">A description of the image</param>
        /// <param name="keepSessionOpen">False by default. If true, a response will be expected</param>
        /// <returns>Itself</returns>
        public static IntentConfiguration ShowImage(
            this IntentConfiguration intent,
            Uri imageUri,
            string accessibilityText,
            bool keepSessionOpen = false)
        {
            return ApplyAction(intent, ShowImage(imageUri, accessibilityText, keepSessionOpen));
        }


        /// <summary>
        /// Create a virtual directive to display an image
        /// </summary>
        /// <param name="imageUri">The Uri of the image to display</param>
        /// <param name="accessibilityText">A description of the image</param>
        /// <param name="keepSessionOpen">False by default. If true, a response will be expected</param>
        /// <returns>ImageDirective</returns>
        public static IVirtualDirective ShowImage(
            Uri imageUri,
            string accessibilityText,
            bool keepSessionOpen = false)
        {
            return new ImageDirective
            {
                AccessibilityText = accessibilityText,
                Image = imageUri,
                KeepSessionOpen = keepSessionOpen
            };
        }


        private static IntentConfiguration ApplyAction(
            IntentConfiguration intent, 
            IVirtualDirective directive)
        {
            intent.Do(context => directive);
            return intent;
        }
    }
}