using System.Collections.Generic;
using System.Linq;

namespace TDD_Katas.Katas.StringCalculator
{
    public class StringCalculator
    {
        private List<int> numbers = new List<int>();

        public int Add(string input)
        {
            if (IsEmptyInput(input))
                return 0;

            ParseInput(input);

            CheckForNegatives();

            RemoveNumbersOver1000();

            return Sum(numbers);
        }

        private void RemoveNumbersOver1000()
        {
            this.numbers = this.numbers.Where(n => n < 1000).ToList();
        }

        private void CheckForNegatives()
        {
            var negatives = GetNegatives(numbers);
            if (negatives.Count > 0)
                throw new NegativesNotAllowedException(negatives);
        }

        private static List<int> GetNegatives(IEnumerable<int> numbers)
        {
            return numbers.Where(n => n < 0).ToList();
        }

        private static int Sum(IEnumerable<int> numbers)
        {
            var result = 0;
            foreach (var number in numbers)
            {
                result += number;
            }

            return result;
        }

        private void ParseInput(string input)
        {
            foreach (var stringNumber in ParseInputToStringArray(input))
            {
                numbers.Add(int.Parse(stringNumber));
            }
        }

        private static string[] ParseInputToStringArray(string input)
        {
            return input.Split(new char[] { ',', '\n' });
        }

        private static bool IsEmptyInput(string input)
        {
            return string.IsNullOrEmpty(input);
        }
    }
}
