using System.Collections.Generic;
using System.Linq;

namespace TDD_Katas.Katas.StringCalculator
{
    public class StringCalculator
    {
        private List<int> numbers = new List<int>();
        private string input = string.Empty;

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
            this.input = input;
            foreach (var stringNumber in ParseInputToStringArray())
            {
                numbers.Add(int.Parse(stringNumber));
            }
        }

        private string[] ParseInputToStringArray()
        {
            var delimiters = new HashSet<string> { ",", "\n" };

            if (TryGetCustomDelimiter(out string customDelim))
            {
                delimiters.Add(customDelim);
                RemoveDelimiterInfoFromInput(customDelim);
            }

            return SplitInput(delimiters);
        }

        private string[] SplitInput(HashSet<string> delimiters)
        {
            return input.Split(delimiters.ToArray(), System.StringSplitOptions.None);
        }

        private void RemoveDelimiterInfoFromInput(string delimiter)
        {
            if (IsSingleCharDelimiter(delimiter))
                input = input[3..];
            else
                input = input[(3 + delimiter.Length)..];
        }

        private static bool IsSingleCharDelimiter(string cd)
        {
            return cd.Length == 1;
        }

        private bool TryGetCustomDelimiter(out string customDelimiter)
        {
            customDelimiter = string.Empty;
            if (CustomDelimiterIsDefined())
            {
                var delimiterEndIndex = GetMulticharacterDelimiterEndIndex();
                if (IsMulticharacterDelimiter(delimiterEndIndex))
                    customDelimiter = GetMultiCharacterDelimiter(delimiterEndIndex);
                else
                    customDelimiter = GetSingleCharacterDelimiter();
                return true;
            }
            return false;
        }

        private string GetSingleCharacterDelimiter()
        {
            return input[2].ToString();
        }

        private static bool IsMulticharacterDelimiter(int delimiterEndIndex)
        {
            return delimiterEndIndex > -1;
        }

        private int GetMulticharacterDelimiterEndIndex()
        {
            return input.IndexOf('\n');
        }

        private string GetMultiCharacterDelimiter(int delimiterEndIndex)
        {
            return input[2..delimiterEndIndex];
        }

        private bool CustomDelimiterIsDefined()
        {
            return input.StartsWith("//");
        }

        private static bool IsEmptyInput(string input)
        {
            return string.IsNullOrEmpty(input);
        }
    }
}
