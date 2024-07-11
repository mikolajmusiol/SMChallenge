using BookingTask.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTask.UnitTests.UtilitiesTests
{
    public class DateTimeCheckingTests
    {
        [Theory]
        [MemberData(nameof(AreDateWithin_CorrectData))]
        public void AreDatesWithinAWeek_ForCorrectInput_ReturnsTrue(List<DateTime> dateTimes)
        {
            var result = DateTimeChecking.AreDatesWithinAWeek(dateTimes);
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(AreDateWithin_IncorrectData))]
        public void AreDatesWithinAWeek_ForIncorrectInput_ReturnsFalse(List<DateTime> dateTimes)
        {
            var result = DateTimeChecking.AreDatesWithinAWeek(dateTimes);
            Assert.False(result);
        }

        public static TheoryData<List<DateTime>> AreDateWithin_CorrectData =>
            new TheoryData<List<DateTime>>
            {
                new List<DateTime>() { new DateTime(2000,10,10), new DateTime(2000,10,11), new DateTime(2000,10,12) },
                new List<DateTime>() { new DateTime(2000,10,10), new DateTime(2000,10,15), new DateTime(2000,10,17) },
                new List<DateTime>() { new DateTime(2000,10,10), new DateTime(2000,10,16) }
            };
        public static TheoryData<List<DateTime>> AreDateWithin_IncorrectData =>
            new TheoryData<List<DateTime>>
            {
                new List<DateTime>() { new DateTime(2000,10,10), new DateTime(2000,10,17), new DateTime(2000,10,18) },
                new List<DateTime>() { new DateTime(2000,10,10), new DateTime(2000,10,19) },
                new List<DateTime>() { new DateTime(2000,10,10), new DateTime(2000,10,11), new DateTime(2000,10,12),
                                       new DateTime(2000,10,13), new DateTime(2000,10,14), new DateTime(2000,10,15),
                                       new DateTime(2000,10,16), new DateTime(2000,10,17), new DateTime(2000,10,18) }
            };


        [Theory]
        [MemberData(nameof(IsLessThan_CorrectData))]
        public void IsLessThan24HoursAway_ForCorrectInput_ReturnsTrue(DateTime dateTime1, DateTime dateTime2)
        {
            var result = DateTimeChecking.IsLessThan24HoursAway(dateTime1, dateTime2);
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(IsLessThan_IncorrectData))]
        public void IsLessThan24HoursAway_ForIncorrectInput_ReturnsFalse(DateTime dateTime1, DateTime dateTime2)
        {
            var result = DateTimeChecking.IsLessThan24HoursAway(dateTime1, dateTime2);
            Assert.False(result);
        }

        public static TheoryData<DateTime, DateTime> IsLessThan_CorrectData =>
            new TheoryData<DateTime, DateTime>
            {
                { DateTime.Now, DateTime.Now.AddHours(2) },
                { DateTime.Now, DateTime.Now },
                { DateTime.Now, DateTime.Now.AddHours(23) },
            };
        public static TheoryData<DateTime, DateTime> IsLessThan_IncorrectData =>
            new TheoryData<DateTime, DateTime>
            {
                { DateTime.Now, DateTime.Now.AddDays(1) },
                { DateTime.Now, DateTime.Now.AddHours(24) },
                { DateTime.Now, DateTime.Now.AddYears(1) },
            };
    }
}
