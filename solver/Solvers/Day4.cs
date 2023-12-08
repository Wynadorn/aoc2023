using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023.Solvers
{
    public class Day4 : ISolver
    {
        public string SolvePartOne(string puzzleInput, string[] puzzleInputArray = null)
        {
            int sum = 0;
            foreach(string line in puzzleInputArray)
            {
                var split = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                string cardString = split[0];
                string numbersString = split[1];

                var split2 = numbersString.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                string winningString = split2[0];
                string myNumbersString = split2[1];

                var myNumbersSplit = myNumbersString.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                int[] myNumbers = Array.ConvertAll(myNumbersSplit.ToArray(), int.Parse);

                var winningNumbersSplit = winningString.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                int[] winningNumbers = Array.ConvertAll(winningNumbersSplit.ToArray(), int.Parse);

                int matches = 0;
                foreach(int winningNumber in winningNumbers)
                {
                    foreach(int myNumber in myNumbers)
                    {
                        if(winningNumber == myNumber)
                            matches++;
                    }
                }

                int points = 0;

                if(matches > 0)
                    points = Convert.ToInt32(Math.Pow(2, matches - 1));

                sum += points;
            }

            return sum.ToString();
        }

        public string SolvePartTwo(string puzzleInput, string[] puzzleInputArray = null)    
        {
            int sum = 0;
            foreach (string line in puzzleInputArray)
            {
                GetPoints(puzzleInputArray, line);
                //var split = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                //string cardString = split[0];
                //string numbersString = split[1];

                //var split2 = numbersString.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                //string winningString = split2[0];
                //string myNumbersString = split2[1];

                //var myNumbersSplit = myNumbersString.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                //int[] myNumbers = Array.ConvertAll(myNumbersSplit.ToArray(), int.Parse);

                //var winningNumbersSplit = winningString.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                //int[] winningNumbers = Array.ConvertAll(winningNumbersSplit.ToArray(), int.Parse);

                //int matches = 0;
                //foreach (int winningNumber in winningNumbers)
                //{
                //    foreach (int myNumber in myNumbers)
                //    {
                //        if (winningNumber == myNumber)
                //            matches++;
                //    }
                //}

                //int points = 0;

                //if (matches > 0)
                //    points = Convert.ToInt32(Math.Pow(2, matches - 1));

                //sum += points;
            }

            return part2Sum.ToString();
        }

        int part2Sum = 0;
        public void GetPoints(string[] puzzleInputArray, string line)
        {
            part2Sum++;

            var split = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            string cardString = split[0];
            int cardNumber = int.Parse(cardString.Split(' ').Last());

            string numbersString = split[1];

            var split2 = numbersString.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            string winningString = split2[0];
            string myNumbersString = split2[1];

            var myNumbersSplit = myNumbersString.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            int[] myNumbers = Array.ConvertAll(myNumbersSplit.ToArray(), int.Parse);

            var winningNumbersSplit = winningString.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            int[] winningNumbers = Array.ConvertAll(winningNumbersSplit.ToArray(), int.Parse);

            int matches = 0;
            foreach (int winningNumber in winningNumbers)
            {
                foreach (int myNumber in myNumbers)
                {
                    if (winningNumber == myNumber)
                        matches++;
                }
            }

            for(int i = cardNumber; i <= cardNumber-1+matches; i++)
            {
                //Console.WriteLine($"loop {i}");
                if(i < puzzleInputArray.Length)
                {
                    GetPoints(puzzleInputArray, puzzleInputArray[i]);
                }
            }
        }
    }
}
