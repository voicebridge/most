using System;

namespace VoiceBridge.Most.Test.TestData
{
    public static class Generic
    {
        public static string Id()
        {
            return Guid.NewGuid().ToString();
        }
    }
}