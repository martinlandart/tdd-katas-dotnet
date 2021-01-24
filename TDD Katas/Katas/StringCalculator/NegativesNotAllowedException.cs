using System;
using System.Collections.Generic;

namespace TDD_Katas.Katas.StringCalculator
{
    public class NegativesNotAllowedException : Exception
    {
        public NegativesNotAllowedException() : base()
        {
        }

        public NegativesNotAllowedException(IEnumerable<int> negatives)
            : base($"Negatives not allowed: {string.Join(", ", negatives)}")
        {
        }

        public NegativesNotAllowedException(string message) : base(message)
        {
        }

        public NegativesNotAllowedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
