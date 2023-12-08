using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2023.Solvers
{
    public class Day2 : ISolver
    {
        private static Regex _regex = new Regex(@"\d+");

        public string SolvePartOne(string puzzleInput, string[] puzzleInputArray = null)
        {
            int sum = 0;

            foreach (string line in puzzleInputArray)
            {
                int line_R = 0;
                int line_G = 0;
                int line_B = 0;

                int indexColumn = line.IndexOf(':');
                string game = line.Substring(0, indexColumn);
                string gameIdString = _regex.Match(game).Value;
                int gameId = int.Parse(gameIdString);

                string cubesfull = line.Substring(indexColumn + 1);

                string[] parts = cubesfull.Split(';');

                foreach (string part in parts)
                {
                    string[] colors = part.Split(',');

                    int part_R = 0;
                    int part_G = 0;
                    int part_B = 0;

                    foreach (string color in colors)
                    {
                        string numberString = _regex.Match(color).Value;
                        int number = int.Parse(numberString);

                        if (color.Contains("blue", StringComparison.CurrentCultureIgnoreCase))
                            part_B += number;

                        if (color.Contains("red", StringComparison.CurrentCultureIgnoreCase))
                            part_R += number;

                        if (color.Contains("green", StringComparison.CurrentCultureIgnoreCase))
                            part_G += number;
                    }

                    line_R = Math.Max(line_R, part_R);
                    line_G = Math.Max(line_G, part_G);
                    line_B = Math.Max(line_B, part_B);
                }

                if (line_R <= 12 && line_G <= 13 && line_B <= 14)
                    sum += gameId;
            }

            return sum.ToString();
        }

        public string SolvePartTwo(string puzzleInput, string[] puzzleInputArray = null)
        {
            int sum = 0;

            foreach (string line in puzzleInputArray)
            {
                int power;

                int line_R = 0;
                int line_G = 0;
                int line_B = 0;

                int indexColumn = line.IndexOf(':');
                string game = line.Substring(0, indexColumn);
                string gameIdString = _regex.Match(game).Value;
                int gameId = int.Parse(gameIdString);

                string cubesfull = line.Substring(indexColumn + 1);

                string[] parts = cubesfull.Split(';');

                foreach (string part in parts)
                {
                    string[] colors = part.Split(',');

                    int part_R = 0;
                    int part_G = 0;
                    int part_B = 0;

                    foreach (string color in colors)
                    {
                        string numberString = _regex.Match(color).Value;
                        int number = int.Parse(numberString);

                        if (color.Contains("blue", StringComparison.CurrentCultureIgnoreCase))
                            part_B = number;

                        if (color.Contains("red", StringComparison.CurrentCultureIgnoreCase))
                            part_R = number;

                        if (color.Contains("green", StringComparison.CurrentCultureIgnoreCase))
                            part_G = number;
                    }

                    line_R = Math.Max(line_R, part_R);
                    line_G = Math.Max(line_G, part_G);
                    line_B = Math.Max(line_B, part_B);
                }

                if(line_R == 0)
                    line_R = 1;

                if (line_G == 0)
                    line_G = 1;

                if (line_B == 0)
                    line_B = 1;

                Console.WriteLine($"'{line}' = R{line_R} x G{line_G} x B{line_B}");

                sum += line_R * line_G * line_B;
            }

            return sum.ToString();
        }
    }
}
