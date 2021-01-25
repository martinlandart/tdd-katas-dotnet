using Microsoft.Extensions.Logging;
using TDD.Kernel;

namespace TDD.Katas.GreeterKata
{
    public class Greeter
    {
        private readonly ILogger logger;
        private readonly IDateTime dateTime;

        // These would be configurable in a real project
        private readonly int morningStart = 6;
        private readonly int eveningStart = 18;
        private readonly int nightStart = 22;

        public Greeter(IDateTime dateTimeService, ILogger logger)
        {
            dateTime = dateTimeService;
            this.logger = logger;
        }

        public string Greet(string name)
        {
            logger.LogInformation($"Greeting {name}");

            return $"{GetGreetingMessage()} {name.Trim().ToTitleCase()}";
        }

        private string GetGreetingMessage()
        {
            if (IsMorning()) return "Good Morning";
            if (IsEvening()) return "Good Evening";
            return "Good Night";
        }

        private bool IsEvening()
        {
            return dateTime.UtcNow.Hour >= eveningStart && dateTime.UtcNow.Hour < nightStart;
        }

        private bool IsMorning()
        {
            return dateTime.UtcNow.Hour >= morningStart && dateTime.UtcNow.Hour < eveningStart;
        }
    }
}
