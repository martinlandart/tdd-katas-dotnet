using System.Collections.Generic;
using System.Linq;

namespace TDD_Katas.Katas.StringCalculator
{
    public class StringCalculator
    {
        private List<int> numbers = new List<int>();

        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            numbers = new InputParser(input).Parse().ToList();

            CheckForNegatives();
            RemoveNumbersOverThreshold(1000);

            return Sum(numbers);
        }

        private void RemoveNumbersOverThreshold(int threshold)
        {
            numbers = this.numbers.Where(n => n < threshold).ToList();
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

        private class InputParser
        {
            private string input = string.Empty;

            public InputParser(string input)
            {
                this.input = input;
            }

            public IEnumerable<int> Parse()
            {
                var numbers = new List<int>();
                foreach (var stringNumber in ParseInputToStringArray())
                {
                    numbers.Add(int.Parse(stringNumber));
                }

                return numbers;
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
                    if (IsMulticharacterCustomDelimiter(delimiterEndIndex))
                        customDelimiter = GetMultiCharacterDelimiter(delimiterEndIndex);
                    else
                        customDelimiter = GetSingleCharacterCustomDelimiter();
                    return true;
                }
                return false;
            }

            private string GetSingleCharacterCustomDelimiter()
            {
                return input[2].ToString();
            }

            private static bool IsMulticharacterCustomDelimiter(int delimiterEndIndex)
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
        }
    }
}
