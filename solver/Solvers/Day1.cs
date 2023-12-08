using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2023.Solvers
{
    public partial class Day1 : ISolver
    {
        private readonly Dictionary<string, string> WordToNumberMap = new Dictionary<string, string>
        {
            { "one", "1" },
            { "two", "2" },
            { "three", "3" },
            { "four", "4" },
            { "five", "5" },
            { "six", "6" },
            { "seven", "7" },
            { "eight", "8" },
            { "nine", "9" }
        };

        private readonly Regex NumberRegex = new Regex("[0-9]{1}", RegexOptions.Compiled);

        [GeneratedRegex("(?=([0-9]|one|two|three|four|five|six|seven|eight|nine))", RegexOptions.Compiled)]
        private partial Regex GeneratedRegex();

        public string NumberWordToNumberString(string numberWord)
        {
            if (WordToNumberMap.TryGetValue(numberWord, out var numberString))
                return numberString;

            throw new Exception($"Number word '{numberWord}' not found in the dictionary.");
        }

        public string SolvePartOne(string puzzleInput, string[] puzzleInputArray = null)
        {
            int total = 0;

            foreach (string calibration in puzzleInputArray)
            {
                if (string.IsNullOrWhiteSpace(calibration))
                    continue;

                var matches = NumberRegex.Matches(calibration);
                string numberOne = matches.First().Value;
                string numberTwo = matches.Last().Value;

                total += int.Parse(numberOne + numberTwo);
            }

            return total.ToString();
        }

        public string SolvePartTwo(string puzzleInput, string[] puzzleInputArray = null)
        {
            Regex myRegex = GeneratedRegex();
            int total = 0;

            ConcurrentBag<int> bag = new ConcurrentBag<int>();

            Parallel.ForEach(puzzleInputArray, (calibration) =>
            {
                if (string.IsNullOrWhiteSpace(calibration))
                    return;

                var matches = myRegex.Matches(calibration);
                string numberOne = matches.First().Groups.Values.Last().Value;
                string numberTwo = matches.Last().Groups.Values.Last().Value;

                if (numberOne.Length > 1)
                    numberOne = NumberWordToNumberString(numberOne);

                if (numberTwo.Length > 1)
                    numberTwo = NumberWordToNumberString(numberTwo);

                int.TryParse(numberOne + numberTwo, out int partialResult);

                bag.Add(partialResult);
            });

            return bag.Sum().ToString();
        }
    }
}
