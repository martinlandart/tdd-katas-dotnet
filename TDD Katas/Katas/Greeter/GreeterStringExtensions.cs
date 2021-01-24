using System.Globalization;

namespace TDD.Katas.GreeterKata
{
    public static class GreeterStringExtensions
    {
        public static string ToTitleCase(this string value)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(value);
        }

        public static string Prepend(this string value, string greetingMessage)
        {
            return $"{greetingMessage} {value}";
        }
    }
}
