using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023.Solvers
{
    internal class Day6 : ISolver
    {
        public string SolvePartOne(string puzzleInput, string[] puzzleInputArray = null)
        {
            int sum = 1;
            int i = 0;
            int[][] games = [[49, 263], [97, 1532], [94, 1378], [94, 1851]];
            int[][] exampleGames = [[7, 9], [15, 40], [30, 200]];
            foreach (int[] game in games)
            {
                int time = game[0];
                int distance = game[1];
                int distanceToWin = distance;

                decimal timePow = (decimal)Math.Pow(time, 2);
                decimal innerCalc = timePow + (4 * (-1 * distanceToWin));
                decimal mathRoot = Sqrt(innerCalc);
                decimal maxTime = (-time - mathRoot) / -2;
                decimal minTime = (-time + mathRoot) / -2;

                int minTimeInt = (int)Math.Ceiling(minTime);
                int maxTimeInt = (int)Math.Floor(maxTime);

                if (minTimeInt == (distance / (time - minTimeInt)))
                    minTimeInt++;

                if (maxTimeInt == (decimal)(distance / (time - maxTimeInt)))
                    maxTimeInt--;

                int options = (maxTimeInt - minTimeInt + 1);

                Console.WriteLine($"game {++i}");
                Console.WriteLine($"    time: {time}");
                Console.WriteLine($"    distance: {distance}");
                Console.WriteLine($"    min time: {minTimeInt} ({minTime})");
                Console.WriteLine($"        min time distance: {minTimeInt * (time - minTimeInt)}");
                Console.WriteLine($"        min time hold: {minTimeInt} min time move {time - minTimeInt}");
                Console.WriteLine($"        min time move {time - minTimeInt}");
                Console.WriteLine($"            {minTimeInt - 1} is not enough because {time}-{minTimeInt - 1}={time - minTimeInt - 1}, so you'll move {minTimeInt - 1}x{time - minTimeInt - 1}={(minTimeInt - 1) * (time - minTimeInt - 1)} <= {distance}");
                Console.WriteLine($"            {minTimeInt} is correct because {time}-{minTimeInt}={time - minTimeInt}, so you'll move {minTimeInt}x{time - minTimeInt}={minTimeInt * (time - minTimeInt)} > {distance}");
                Console.WriteLine($"    max time: {maxTimeInt} ({maxTime})");
                Console.WriteLine($"        max time distance: {maxTimeInt * (time - maxTimeInt)}");
                Console.WriteLine($"        max time hold: {maxTimeInt} max time move {time - maxTimeInt}");
                Console.WriteLine($"        max time move {time - maxTimeInt}");
                Console.WriteLine($"            {maxTimeInt + 1} is too much because {time}-{maxTimeInt + 1}={time - (maxTimeInt + 1)}, so you'll move {maxTimeInt + 1}x{time - (maxTimeInt + 1)}={(maxTimeInt + 1) * (time - (maxTimeInt + 1))} <= {distance}");
                Console.WriteLine($"            {maxTimeInt} is correct because {time}-{maxTimeInt}={time - maxTimeInt}, so you'll move {maxTimeInt}x{time - maxTimeInt}={maxTimeInt * (time - maxTimeInt)} > {distance}");
                Console.WriteLine($"    options: {options}");
                Console.WriteLine();

                sum *= options;
            }

            Console.WriteLine($"'7136856' is a wrong answer!");
            Console.WriteLine(sum);
            return sum.ToString();
        }

        public string SolvePartTwo(string puzzleInput, string[] puzzleInputArray = null)
        {
            int sum = 1;
            int i = 0;
            long[][] games = [[49979494, 263153213781851]];
            int[][] exampleGames = [[7, 9], [15, 40], [30, 200]];
            foreach (long[] game in games)
            {
                long time = game[0];
                long distance = game[1];
                long distanceToWin = distance;

                decimal timePow = (decimal)Math.Pow(time, 2);
                decimal innerCalc = timePow + (4 * (-1 * distanceToWin));
                decimal mathRoot = Sqrt(innerCalc);
                decimal maxTime = (-time - mathRoot) / -2;
                decimal minTime = (-time + mathRoot) / -2;

                int minTimeInt = (int)Math.Ceiling(minTime);
                int maxTimeInt = (int)Math.Floor(maxTime);

                if (minTimeInt == (distance / (time - minTimeInt)))
                    minTimeInt++;

                if (maxTimeInt == (decimal)(distance / (time - maxTimeInt)))
                    maxTimeInt--;

                int options = (maxTimeInt - minTimeInt + 1);

                Console.WriteLine($"game {++i}");
                Console.WriteLine($"    time: {time}");
                Console.WriteLine($"    distance: {distance}");
                Console.WriteLine($"    min time: {minTimeInt} ({minTime})");
                Console.WriteLine($"        min time distance: {minTimeInt * (time - minTimeInt)}");
                Console.WriteLine($"        min time hold: {minTimeInt} min time move {time - minTimeInt}");
                Console.WriteLine($"        min time move {time - minTimeInt}");
                Console.WriteLine($"            {minTimeInt - 1} is not enough because {time}-{minTimeInt - 1}={time - minTimeInt - 1}, so you'll move {minTimeInt - 1}x{time - minTimeInt - 1}={(minTimeInt - 1) * (time - minTimeInt - 1)} <= {distance}");
                Console.WriteLine($"            {minTimeInt} is correct because {time}-{minTimeInt}={time - minTimeInt}, so you'll move {minTimeInt}x{time - minTimeInt}={minTimeInt * (time - minTimeInt)} > {distance}");
                Console.WriteLine($"    max time: {maxTimeInt} ({maxTime})");
                Console.WriteLine($"        max time distance: {maxTimeInt * (time - maxTimeInt)}");
                Console.WriteLine($"        max time hold: {maxTimeInt} max time move {time - maxTimeInt}");
                Console.WriteLine($"        max time move {time - maxTimeInt}");
                Console.WriteLine($"            {maxTimeInt + 1} is too much because {time}-{maxTimeInt + 1}={time - (maxTimeInt + 1)}, so you'll move {maxTimeInt + 1}x{time - (maxTimeInt + 1)}={(maxTimeInt + 1) * (time - (maxTimeInt + 1))} <= {distance}");
                Console.WriteLine($"            {maxTimeInt} is correct because {time}-{maxTimeInt}={time - maxTimeInt}, so you'll move {maxTimeInt}x{time - maxTimeInt}={maxTimeInt * (time - maxTimeInt)} > {distance}");
                Console.WriteLine($"    options: {options}");
                Console.WriteLine();

                sum *= options;
            }

            Console.WriteLine($"'7136856' is a wrong answer!");
            Console.WriteLine(sum);
            return sum.ToString();
        }

        public static decimal Sqrt(decimal x, decimal epsilon = 0.0M)
        {
            if (x < 0) throw new OverflowException("Cannot calculate square root from a negative number");

            decimal current = (decimal)Math.Sqrt((double)x), previous;
            do
            {
                previous = current;
                if (previous == 0.0M) return 0;
                current = (previous + x / previous) / 2;
            }
            while (Math.Abs(previous - current) > epsilon);
            return current;
        }
    }
}
