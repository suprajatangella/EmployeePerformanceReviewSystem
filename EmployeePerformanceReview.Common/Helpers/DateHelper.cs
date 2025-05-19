using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Common.Helpers
{
    public static class DateHelper
    {
        public static DateTime GetCurrentDateTime() //Get Current Date/Time in Local Timezone
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.Local);
        }
        public static string FormatDate(DateTime date, string format = "dd-MM-yyyy") // Format Date to String (e.g., "dd-MM-yyyy")
        {
            return date.ToString(format);
        }
        public static bool IsDateInRange(DateTime date, DateTime start, DateTime end) // Check if Date is Within Range
        {
            return date >= start && date <= end;
        }

        public static int GetDaysBetween(DateTime start, DateTime end) // Get Days Between Two Dates
        {
            return (end - start).Days;
        }
        public static bool IsWeekend(DateTime date)  //  Check if Date is Weekend
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
        public static DateTime? ParseDate(string input, string format = "dd-MM-yyyy") // Convert String to DateTime Safely
        {
            if (DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out DateTime result))
                return result;
            return null;
        }

        public static int CalculateAge(DateTime dob) // Calculate Age from DOB
        {
            var today = DateTime.Today;
            int age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--;
            return age;
        }

        public static DateTime GetStartOfWeek(DateTime date) // Get Start of the Week
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        public static DateTime GetEndOfWeek(DateTime date) // Get End of the Week
        {
            return GetStartOfWeek(date).AddDays(6);
        }
    }
}
