using Microsoft.Extensions.Logging;
using System;
using TDD.Kernel;
using TDD_Katas.Katas.Greeter;

namespace TDD.Katas.GreeterKata
{
    public class Greeter
    {
        private readonly ILogger logger;
        private readonly IDateTime dateTime;

        // These would be configurable in a real project
        private readonly DateTime morningStart = new DateTime().AddHours(6);
        private readonly DateTime morningEnd = new DateTime().AddHours(12);

        private readonly DateTime eveningStart = new DateTime().AddHours(18);
        private readonly DateTime eveningEnd = new DateTime().AddHours(22);

        private readonly DateTime nightStart = new DateTime().AddHours(22);
        private readonly DateTime nightEnd = new DateTime().AddHours(06);

        public Greeter(IDateTime dateTimeService, ILogger logger)
        {
            dateTime = dateTimeService;
            this.logger = logger;
        }

        public string Greet(string name)
        {
            logger.LogInformation($"Greeting {name}");

            return name
                .Trim()
                .ToTitleCase()
                .Prepend(GetGreetingMessage());
        }

        private string GetGreetingMessage()
        {
            if (IsMorning())
            {
                return "Good Morning";
            }
            if (IsEvening())
            {
                return "Good Evening";
            }
            if (IsNight())
            {
                return "Good Night";
            }

            return "Hello";
        }

        private bool IsNight()
        {
            return dateTime.UtcNow().Hour >= nightStart.Hour || dateTime.UtcNow().Hour < nightEnd.Hour;
        }

        private bool IsEvening()
        {
            return dateTime.UtcNow().Hour >= eveningStart.Hour && dateTime.UtcNow().Hour < eveningEnd.Hour;
        }

        private bool IsMorning()
        {
            return dateTime.UtcNow().Hour >= morningStart.Hour && dateTime.UtcNow().Hour < morningEnd.Hour;
        }
    }
}
