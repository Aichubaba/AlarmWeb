using System;

namespace BlazorApp2.Helpers
{
    public static class UnixTimeHelper
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime? FromUnix(long? seconds)
        {
            if (seconds == null || seconds == 0)
                return null;
            
            try
            {
                return UnixEpoch.AddSeconds(seconds.Value).ToLocalTime();
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }
    }
}
