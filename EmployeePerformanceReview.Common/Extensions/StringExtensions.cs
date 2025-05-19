using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Check if a string is null, empty, or only whitespace.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Capitalizes the first letter of a string.
        /// </summary>
        public static string CapitalizeFirstLetter(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;
            return char.ToUpper(input[0]) + input.Substring(1);
        }

        /// <summary>
        /// Converts a string to Title Case.
        /// </summary>
        public static string ToTitleCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }

        /// <summary>
        /// Removes all HTML tags from a string.
        /// </summary>
        public static string StripHtml(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Truncates the string to a specified length and adds ellipsis (…) if needed.
        /// </summary>
        public static string Truncate(this string input, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Length <= maxLength) return input;
            return input.Substring(0, maxLength) + "...";
        }

        /// <summary>
        /// Checks if a string contains only digits.
        /// </summary>
        public static bool IsNumeric(this string input)
        {
            return input.All(char.IsDigit); 
        }
    }
}
