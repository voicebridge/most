namespace VoiceBridge.Most
{
    /// <summary>
    /// Helpers methods for Request Handler Builder
    /// </summary>
    public static class RequestHandlerBuilderExtensions
    {
        /// <summary>
        /// True when a parameter is missing
        /// </summary>
        /// <param name="builder">RequestHandlerBuilder instance</param>
        /// <param name="parameterName">parameter name</param>
        /// <returns>Itself</returns>
        public static IRequestHandlerBuilder WhenParameterIsMissing(this IRequestHandlerBuilder builder, string parameterName)
        {
            return builder.When(context => !context.RequestModel.ParameterHasValue(parameterName));
        }

        /// <summary>
        /// True when a parameter has a value
        /// </summary>
        /// <param name="builder">RequestHandlerBuilder instance</param>
        /// <param name="parameterName">parameter name</param>
        /// <returns>Itself</returns>
        public static IRequestHandlerBuilder WhenParameterIsSupplied(this IRequestHandlerBuilder builder, string parameterName)
        {
            return builder.When(context => context.RequestModel.ParameterHasValue(parameterName));
        }
    }
}