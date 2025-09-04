using System.Globalization;

namespace TodoManager.Helpers
{
    public static class DateValidationHelper
    {
        private static readonly string[] acceptedDateFormats = { "M/d/yyyy", "MM/dd/yyyy", "d/M/yyyy", "dd/MM/yyyy"};

        public static bool IsValidDate(string? dateString)
        {
            if (string.IsNullOrEmpty(dateString))
                return false;

            return DateTime.TryParseExact(
                dateString,
                acceptedDateFormats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _);
        }
    }
}
