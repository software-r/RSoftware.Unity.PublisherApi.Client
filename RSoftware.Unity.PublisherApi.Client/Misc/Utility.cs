
namespace RSoftware.Unity.PublisherApi.Client.Misc
{
    using System;
    using System.Globalization;

    internal static class Utility
    {
        private const string DATE_FORMAT = "yyyy-MM-dd";

        public static float ParseFloat(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0.0f;
            }

            var spliited = value.Split(' ');

            return float.TryParse(spliited[0], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var result) ? result : 0.0f;
        }

        public static float ParseCurrency(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0.0f;
            }

            var spliited = value.Split(' ');

            return float.TryParse(spliited[1], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var result) ? result : 0.0f;
        }

        public static DateTimeOffset ParseDt(string value, string format = DATE_FORMAT)
        {
            return DateTimeOffset.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var result) ? result : default;
        }
    }
}
