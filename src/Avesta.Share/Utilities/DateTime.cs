using MoreLinq;
using MoreLinq.Extensions;
using Avesta.Share.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Utilities
{
    public class DateTimeUtilities
    {
        public static int InitNewWeekOfStartTimeAndFindDistance(DayOfWeek startDay, DayOfWeek specificDay)
        {
            var week = new DayOfWeek[7];
            var start = (int)startDay;
            for (int i = 0; i < 7; i++)
            {
                var result = startDay + i;
                week[i] = (int)(result) >= 7 ? result - 7 : result;
            }
            var index = Array.FindIndex(week, i => i == specificDay);
            return index;
        }


        public static (DateTime start, DateTime end) GetLastWeekPeriod()
        {
            int days = DateTime.Now.DayOfWeek - DayOfWeek.Sunday;
            DateTime pastDate = DateTime.Now.AddDays(-(days + 7));
            DateTime futureDate = pastDate.AddDays(6);

            return (pastDate, futureDate);
        }
    }
}
