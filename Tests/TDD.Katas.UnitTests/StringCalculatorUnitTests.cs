using FluentAssertions;
using TDD_Katas.Katas.StringCalculator;
using Xunit;

namespace TDD.Katas.UnitTests
{
    public class StringCalculatorUnitTests
    {
        private readonly StringCalculator calc;

        public StringCalculatorUnitTests()
        {
            calc = new StringCalculator();
        }

        [Fact]
        public void EmptyStringShouldReturnZero()
        {
            calc.Add(string.Empty).Should().Be(0);
        }

        [Theory]
        [InlineData("1",1)]
        [InlineData("2",2)]
        [InlineData("5",5)]
        public void SingleNumberShouldReturnValue(string input, int value)
        {
            calc.Add(input).Should().Be(value);
        }

        [Theory]
        [InlineData("1,2", 3)]
        [InlineData("10,20", 30)]
        public void TwoNumbersCommaDelimitedShouldReturnSumOfNumbers(string input, int sum)
        {
            calc.Add(input).Should().Be(sum);
        }

        [Theory]
        [InlineData("1\n2", 3)]
        [InlineData("10\n20", 30)]
        public void TwoNumbersNewlineDelimitedShouldReturnSumOfNumbers(string input, int sum)
        {
            calc.Add(input).Should().Be(sum);
        }

        [Theory]
        [InlineData("1\n2,5", 8)]
        [InlineData("10\n20,1", 31)]
        public void ThreeNumbersMixedDelimiterShouldReturnSumOfNumbers(string input, int sum)
        {
            calc.Add(input).Should().Be(sum);
        }

        [Fact]
        public void NegativeNumbersShouldThrowExceptionWithDetails()
        {
            const string input = "-10,7,9, -1";

            calc.Invoking(x => x.Add(input))
                .Should().Throw<NegativesNotAllowedException>().WithMessage("Negatives not allowed: -10, -1");
        }

        [Fact]
        public void NumbersOver1000ShouldBeIgnored()
        {
            const string input = "1,1,1,1,1,1000";

            calc.Add(input).Should().Be(5);
        }

        [Theory]
        [InlineData("//#1#1#1#1#1")]
        [InlineData("//*1*1*1*1*1")]
        [InlineData("//z1z1z1z1z1")]
        public void SingleCharDelimiterShouldBeAccepted(string input)
        {
            calc.Add(input).Should().Be(5);
        }

        [Theory]
        [InlineData("//###\n1###1###1###1###1")]
        [InlineData("//**\n1**1**1**1**1")]
        [InlineData("//zzzzz\n1zzzzz1zzzzz1zzzzz1zzzzz1")]
        public void MultipleCharDelimiterShouldBeAccepted(string input)
        {
            calc.Add(input).Should().Be(5);
        }
    }
}
