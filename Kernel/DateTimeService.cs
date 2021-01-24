using System;

namespace TDD.Kernel
{
    public class DateTimeService : IDateTime
    {
        public DateTime UtcNow() => DateTime.UtcNow;
    }
}
