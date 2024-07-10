namespace BookingTask.Utilities
{
    public static class DateTimeChecking
    {
        public static bool AreDatesWithinAWeek(List<DateTime> dateTimes)
        {
            if (dateTimes.Count > 7)
            {
                return false;
            }

            DateTime minDate = dateTimes.Min();
            DateTime maxDate = dateTimes.Max();

            TimeSpan difference = maxDate - minDate;

            return difference.Days <= 7;
        }

        public static bool IsLessThan24HoursAway(DateTime dateTime1, DateTime dateTime2)
        {
            TimeSpan difference = dateTime1 - dateTime2;

            return Math.Abs(difference.TotalHours) < 24;
        }
    }
}
