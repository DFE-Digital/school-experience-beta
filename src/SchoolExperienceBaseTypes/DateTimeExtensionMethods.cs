using System;

namespace SchoolExperienceBaseTypes
{
    public static class DateTimeExtensionMethods
    {
        public static DateTime StartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, date.Kind);
        }

        public static DateTime EndOfMonth(this DateTime date)
        {
            date = date.AddMonths(1);
            date = new DateTime(date.Year, date.Month, 1, 0, 0, 0, date.Kind);
            return date.Subtract(TimeSpan.FromMilliseconds(1));
        }
    }
}
