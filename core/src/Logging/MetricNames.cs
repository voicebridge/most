namespace VoiceBridge.Most.Logging
{
    public static class MetricNames
    {
        public const string SessionRestoreTime = "most_session_restore_ms";
        public const string SessionSaveTime = "most_session_restore_ms";
        public const string ExecutingRequestHandlers = "most_handler_execution_ms";
        public const string ProcessingVirtualDirectives = "most_processing_virtual_directives_ms";
        public const string FullEvaluationTime = "most_engine_eval_ms";
    }
}