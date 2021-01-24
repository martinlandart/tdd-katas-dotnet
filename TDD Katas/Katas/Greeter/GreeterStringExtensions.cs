namespace TDD_Katas.Katas.Greeter
{
    public static class GreeterStringExtensions
    {
        public static string CapitalizeFirstLetter(this string value)
        {
            return value[0].ToString().ToUpper() + value[1..];
        }

        public static string PrependGreetingMessage(this string value, string greetingMessage)
        {
            return $"{greetingMessage} {value}";
        }
    }
}
