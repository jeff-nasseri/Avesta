using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Avesta.Share.Utilities
{
    public class PersianCulture : CultureInfo
    {

        public static void Initialize()
        {
            var persianCulture = new PersianCulture();
            persianCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            persianCulture.DateTimeFormat.LongDatePattern = "dddd d MMMM yyyy";
            persianCulture.DateTimeFormat.AMDesignator = "صبح";
            persianCulture.DateTimeFormat.PMDesignator = "عصر";
            Thread.CurrentThread.CurrentCulture = persianCulture;
            Thread.CurrentThread.CurrentUICulture = persianCulture;
        }

        private readonly Calendar calendar;
        private readonly Calendar[] optionals;

        /// <summary>
        /// کلاس فارسی ساز فرهنگ فارسی + تقویم شمسی
        /// </summary>
        /// <param name="cultureName">fa-IR</param>
        /// <param name="useUserOverride">true</param>
        public PersianCulture() : this("fa-IR", true)
        {
        }

        public PersianCulture(string cultureName, bool useUserOverride)
            : base(cultureName, useUserOverride)
        {
            //Temporary Value for cal.
            calendar = base.OptionalCalendars[0];

            //populating new list of optional calendars.
            var optionalCalendars = new List<Calendar>();
            optionalCalendars.AddRange(base.OptionalCalendars);
            optionalCalendars.Insert(0, new PersianCalendar());


            Type formatType = typeof(DateTimeFormatInfo);
            Type calendarType = typeof(Calendar);


            PropertyInfo idProperty = calendarType.GetProperty("ID", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo optionalCalendarfield = formatType.GetField("optionalCalendars",
                                                                  BindingFlags.Instance | BindingFlags.NonPublic);

            ////populating new list of optional calendar ids
            //var newOptionalCalendarIDs = new Int32[optionalCalendars.Count];
            //for (int i = 0; i < newOptionalCalendarIDs.Length; i++)
            //    newOptionalCalendarIDs[i] = (Int32)idProperty.GetValue(optionalCalendars[i], null);

            //optionalCalendarfield.SetValue(DateTimeFormat, newOptionalCalendarIDs);

            optionals = optionalCalendars.ToArray();
            calendar = optionals[0];
            DateTimeFormat.Calendar = optionals[0];
            DateTimeFormat.MonthNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
            DateTimeFormat.MonthGenitiveNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
            DateTimeFormat.AbbreviatedMonthNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
            DateTimeFormat.AbbreviatedMonthGenitiveNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };


            DateTimeFormat.AbbreviatedDayNames = new string[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
            DateTimeFormat.ShortestDayNames = new string[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
            DateTimeFormat.DayNames = new string[] { "یکشنبه", "دوشنبه", "ﺳﻪ شنبه", "چهارشنبه", "پنج شنبه", "جمعه", "شنبه" };

            DateTimeFormat.AMDesignator = "ق.ظ";
            DateTimeFormat.PMDesignator = "ب.ظ";

            DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            DateTimeFormat.LongDatePattern = "yyyy-MM-dd";

            DateTimeFormat.SetAllDateTimePatterns(new[] { "yyyy-MM-dd" }, 'd');
            DateTimeFormat.SetAllDateTimePatterns(new[] { "dddd, dd MMMM yyyy" }, 'D');
            DateTimeFormat.SetAllDateTimePatterns(new[] { "yyyy MMMM" }, 'y');
            DateTimeFormat.SetAllDateTimePatterns(new[] { "yyyy MMMM" }, 'Y');

            NumberFormat.NumberDecimalSeparator = ".";
            //NumberFormat.NumberGroupSeparator = " ";
            NumberFormat.CurrencyDecimalSeparator = ".";


        }

        public override Calendar Calendar
        {
            get { return calendar; }
        }

        public override Calendar[] OptionalCalendars
        {
            get { return optionals; }
        }
    }

}
