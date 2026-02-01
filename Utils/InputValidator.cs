using System.Text.RegularExpressions;

namespace GreenLifeStore.Utils
{
    internal class InputValidator
    {
        // Letters and spaces only
        public static bool IsTextOnly(string input)
        {
            return !string.IsNullOrWhiteSpace(input) &&
                   Regex.IsMatch(input, @"^[a-zA-Z\s]+$");
        }

        public static bool IsValidDouble(string input)
        {
            return double.TryParse(input, out _);
        }

        public static bool IsValidInt(string input)
        {
            return int.TryParse(input, out _);
        }

        public static bool IsValidContactNumber(string input)
        {
            return !string.IsNullOrWhiteSpace(input) &&
                   Regex.IsMatch(input, @"^\d{7,15}$");
        }
    }
}
