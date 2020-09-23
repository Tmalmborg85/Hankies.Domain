using System;
namespace Hankies.Domain.HelperClasses
{
    public static class TimePassed
    {
        public const double FreshUpperLimit = 10;
        public const double OkUpperLimit = 30;
        public const double AgeingUpperLimit = 60;

        public static double MinutesPassed(this DateTimeOffset then)
        {
            var now = DateTimeOffset.UtcNow;
            var timeDiff = now.Subtract(then);
            return timeDiff.TotalMinutes;
        }

        public static StalenessTypes HowStale(this DateTimeOffset time)
        {
            // How much time has passed
            var minutesPassed = MinutesPassed(time);

            if (minutesPassed < FreshUpperLimit)
                return StalenessTypes.Fresh;

            if (minutesPassed >= FreshUpperLimit &&
                minutesPassed < OkUpperLimit)
                return StalenessTypes.Ok;

            if (minutesPassed >= OkUpperLimit &&
                minutesPassed < AgeingUpperLimit)
                return StalenessTypes.Ageing;

            return StalenessTypes.Stale;
        }
    }
}
