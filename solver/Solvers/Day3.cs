using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2023.Solvers
{
    internal class Day3 : ISolver
    {
        private static Regex _regex = new Regex(@"\d+");
        private static Regex _gearRegex = new Regex(@"\*");

        public string SolvePartOne(string puzzleInput, string[] puzzleInputArray = null)
        {
            int sum = 0;

            List<partnumber> numbers = new List<partnumber>();
            for (int i = 0; i < puzzleInputArray.Length; i++)
            {
                string line = puzzleInputArray[i];
                foreach (var matches in _regex.EnumerateMatches(line))
                {
                    int number = int.Parse(line.Substring(matches.Index, matches.Length));
                    numbers.Add(new partnumber() { number = number, Xlocation = matches.Index, Ylocation = i});
                }
            }

            foreach(partnumber pn in numbers)
            {
                int x1 = Math.Max(pn.Xlocation-1, 0);
                int y1 = Math.Max(pn.Ylocation - 1, 0);
                int x2 = Math.Min(pn.Xlocation + pn.number.ToString().Length+1, puzzleInputArray[0].Length);
                int y2 = Math.Min(pn.Ylocation + 1, puzzleInputArray.Count() -1 );

                List<char> surroundingChars = new List<char>();

                for (int i = y1; i <= y2; i++)
                {
                    string temp = puzzleInputArray[i].Substring(x1, x2 - x1);
                    surroundingChars.AddRange(temp.ToCharArray());

                }
                
                bool flag = false;
                foreach(char c in surroundingChars)
                {
                    if (char.IsAsciiDigit(c))
                        continue;

                    if(c.Equals('.'))
                        continue;

                    flag = true;
                    break;
                }

                if(flag)
                    sum += pn.number;

                //Console.WriteLine();
            }

            return sum.ToString();
        }

        class partnumber
        {
            public int number;
            public int Xlocation;
            public int Ylocation;
        }

        class gear
        {
            public int Xlocation;
            public int Ylocation;
        }

        public string SolvePartTwo(string puzzleInput, string[] puzzleInputArray = null)
        {
            int sum = 0;

            List<partnumber> numbers = new List<partnumber>();
            for (int i = 0; i < puzzleInputArray.Length; i++)
            {
                string line = puzzleInputArray[i];
                foreach (var match in _regex.EnumerateMatches(line))
                {
                    int number = int.Parse(line.Substring(match.Index, match.Length));
                    numbers.Add(new partnumber() { number = number, Xlocation = match.Index, Ylocation = i });
                }
            }

            List<partnumber> validNumbers = new List<partnumber>();
            foreach (partnumber pn in numbers)
            {
                int x1 = Math.Max(pn.Xlocation - 1, 0);
                int y1 = Math.Max(pn.Ylocation - 1, 0);
                int x2 = Math.Min(pn.Xlocation + pn.number.ToString().Length + 1, puzzleInputArray[0].Length);
                int y2 = Math.Min(pn.Ylocation + 1, puzzleInputArray.Count() - 1);

                List<char> surroundingChars = new List<char>();

                for (int i = y1; i <= y2; i++)
                {
                    string temp = puzzleInputArray[i].Substring(x1, x2 - x1);
                    surroundingChars.AddRange(temp.ToCharArray());
                }

                bool flag = false;
                foreach (char c in surroundingChars)
                {
                    if (char.IsAsciiDigit(c))
                        continue;

                    if (c.Equals('.'))
                        continue;
                    
                    flag = true;
                    break;
                }

                if (flag)
                    validNumbers.Add(pn);

                //Console.WriteLine();
            }
            
            List<gear> gears = new List<gear>();
            for (int i = 0; i < puzzleInputArray.Length; i++)
            {
                string line = puzzleInputArray[i];
                foreach (var match in _gearRegex.EnumerateMatches(line))
                {
                    gears.Add(new gear() { Xlocation =  match.Index, Ylocation = i });
                }
            }

            int gearInt = 0;
            foreach(var gear in gears)
            {
                //Console.WriteLine($"Gear {gearInt} ({gear.Xlocation}x{gear.Ylocation})");
                List<partnumber> gearPartNumbers = new List<partnumber>();

                foreach(partnumber pn in validNumbers)
                {
                    int x1 = Math.Max(pn.Xlocation - 1, 0);
                    int y1 = Math.Max(pn.Ylocation - 1, 0);
                    int x2 = Math.Min(pn.Xlocation + pn.number.ToString().Length, puzzleInputArray[0].Length);
                    int y2 = Math.Min(pn.Ylocation + 1, puzzleInputArray.Count() - 1);  

                    if (gear.Xlocation >= x1 && gear.Xlocation <= x2 && gear.Ylocation >= y1 && gear.Ylocation <= y2)
                    {
                        gearPartNumbers.Add(pn);

                        for (int i = y1; i <= y2; i++)
                        {
                            string temp = puzzleInputArray[i].Substring(x1, x2 - x1);
                            //Console.WriteLine(temp);
                        }
                        //Console.WriteLine();
                    }
                }

                if(gearPartNumbers.Count == 2)
                    sum += gearPartNumbers[0].number * gearPartNumbers[1].number;

                gearInt++;
            }

            return sum.ToString();
        }
    }
}
