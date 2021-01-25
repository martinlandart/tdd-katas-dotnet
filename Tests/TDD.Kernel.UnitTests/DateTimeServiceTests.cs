using FluentAssertions;
using System;
using Xunit;

namespace TDD.Kernel.UnitTests
{
    public class DateTimeServiceTests
    {
        [Fact]
        public void ShouldReturnDateTime()
        {
            var dateTimeService = new DateTimeService();

            dateTimeService.UtcNow.Should().BeCloseTo(DateTime.UtcNow, 1000);
        }
    }
}
