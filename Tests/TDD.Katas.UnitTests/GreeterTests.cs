using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using TDD.Katas.GreeterKata;
using TDD.Kernel;
using Xunit;

namespace TDD.Katas.UnitTests
{
    public class GreeterTests
    {
        private readonly Greeter greeter;
        private readonly Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
        private readonly Mock<ILogger> loggerMock = new Mock<ILogger>();

        private const string goodMorningGreet = "Good Morning";
        private const string goodEveningGreet = "Good Evening";
        private const string goodNightGreet = "Good Night";

        public GreeterTests()
        {
            greeter = new Greeter(dateTimeMock.Object, loggerMock.Object);
        }

        [Fact]
        public void GreetGoodMorningShouldTrimInputString()
        {
            SetMorningTime();
            AssertTrimmedInput(goodMorningGreet);
        }

        [Fact]
        public void GreetGoodEveningShouldTrimInputString()
        {
            SetEveningTime();
            AssertTrimmedInput(goodEveningGreet);
        }

        [Fact]
        public void GreetGoodNightShouldTrimInputString()
        {
            SetNightTime();
            AssertTrimmedInput(goodNightGreet);
        }

        private void AssertTrimmedInput(string greeting)
        {
            greeter.Greet("     Martin   ").Should().Be($"{greeting} Martin");
        }

        private void SetMorningTime()
        {
            SetCurrentTime(07, 00);
        }

        private void SetEveningTime()
        {
            SetCurrentTime(20, 00);
        }

        private void SetNightTime()
        {
            SetCurrentTime(00, 00);
        }

        [Fact]
        public void GreetGoodMorningShouldTitleCaseName()
        {
            SetMorningTime();
            AssertTitleCaseName(goodMorningGreet);
        }

        [Fact]
        public void GreetGoodEveningShouldTitleCaseName()
        {
            SetEveningTime();
            AssertTitleCaseName(goodEveningGreet);
        }

        [Fact]
        public void GreetGoodNightShouldTitleCaseName()
        {
            SetNightTime();
            AssertTitleCaseName(goodNightGreet);
        }

        private void AssertTitleCaseName(string greeting)
        {
            greeter.Greet("martin landart").Should().Be($"{greeting} Martin Landart");
        }

        [Fact]
        public void GreetGoodMorningShouldTrimAndCapitalizeName()
        {
            SetMorningTime();
            greeter.Greet("    martin  ").Should().Be($"{goodMorningGreet} Martin");
        }

        [Theory]
        [InlineData(6,0)]
        [InlineData(7,0)]
        [InlineData(8,30)]
        [InlineData(11,30)]
        [InlineData(11,59)]
        public void ShouldReturnGoodMorningIfBetween06and12Hours(int hours, int minutes)
        {
            SetCurrentTime(hours, minutes);

            greeter.Greet("Martin").Should().Be("Good Morning Martin");
        }

        [Theory]
        [InlineData(18, 0)]
        [InlineData(19, 0)]
        [InlineData(20, 30)]
        [InlineData(20, 55)]
        [InlineData(21, 59)]
        public void ShouldReturnGoodEveningIfBetween18and22Hours(int hours, int minutes)
        {
            SetCurrentTime(hours, minutes);

            greeter.Greet("Martin").Should().Be("Good Evening Martin");
        }

        [Theory]
        [InlineData(22, 0)]
        [InlineData(23, 0)]
        [InlineData(00, 30)]
        [InlineData(02, 55)]
        [InlineData(5, 59)]
        public void ShouldReturnGoodNightIfBetween22and06Hours(int hours, int minutes)
        {
            SetCurrentTime(hours, minutes);

            greeter.Greet("Martin").Should().Be("Good Night Martin");
        }

        [Fact]
        public void GreetShouldLogInformationOnce()
        {
            greeter.Greet("Martin");

            loggerMock.Verify(x => x.Log(LogLevel.Information,
               It.IsAny<EventId>(),
               It.IsAny<It.IsAnyType>(),
               It.IsAny<Exception>(),
               (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
               Times.Once);
        }

        private void SetCurrentTime(int hours, int minutes)
        {
            var dateTime = new DateTime().AddHours(hours).AddMinutes(minutes);
            var time = dateTimeMock.Setup(d => d.UtcNow()).Returns(dateTime);
        }
    }
}
