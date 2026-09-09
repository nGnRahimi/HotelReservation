using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extentions
{
    public static class DateTimeExtention
    {

        public static DateTime ToMiladiDateTime(this string persianDate)
        {

            persianDate = persianDate.ToEnglishNumber();

            var dateParts = persianDate.Split('/');

            if (dateParts.Length != 3)
            {
                throw new FormatException("Invalid Persian date format. The format should be 'yyyy/MM/dd'.");
            }

            int year = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int day = int.Parse(dateParts[2]);

            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }



        public static string ToEnglishNumber(this string persianNumber)
        {
            // Persian and Arabic numbers
            char[] persianDigits = new char[] { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };
            char[] englishDigits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            for (int i = 0; i < persianDigits.Length; i++)
            {
                persianNumber = persianNumber.Replace(persianDigits[i], englishDigits[i]);
            }

            return persianNumber;
        }

        public static string ToPersianDate(this DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetYear(dateTime);
            int month = persianCalendar.GetMonth(dateTime);
            int day = persianCalendar.GetDayOfMonth(dateTime);



            return $"{year:0000}/{month:00}/{day:00}";
        }


        public static string ToPersianDayOfWeek(this DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            DayOfWeek dayOfWeek = persianCalendar.GetDayOfWeek(dateTime);


            switch (dayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return "شنبه";



                case DayOfWeek.Sunday:
                    return "یکشنبه";





                case DayOfWeek.Monday:
                    return "دوشنبه";





                case DayOfWeek.Tuesday:
                    return "سه شنبه";




                case DayOfWeek.Wednesday:
                    return "چهارشنبه";




                case DayOfWeek.Thursday:
                    return "پنج شنبه";



                case DayOfWeek.Friday:
                    return "جمعه";

                default:
                    throw new ArgumentOutOfRangeException();

            }

        }

        public static List<DateTime> GetNextSevenDays(DateTime? date = null)
        {
            date = date ?? DateTime.Now;
            var days = new List<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                days.Add(date.Value.AddDays(i));

            }

            return days;


        }

        }
    }



