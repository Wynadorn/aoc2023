using AoC2023.Solvers;
using AoC2023.Util;

namespace AoC2023
{
    internal class Program
    {
        static bool ApplicationIsRunning = true;
        private static readonly string _sessionCookie = "53616c7465645f5f9347d82b61496e950b57fa02364981e5e1c2c3e822a9cb8a2176cd454c03c01eec1ca6d7c2ae20f9ff606179958a00459193c74ddd323ce8";

        static void Main(string[] args)
        {
            while (ApplicationIsRunning)
                SolvePuzzles();
        }

        private static void SolvePuzzles()
        {
            int dayToSolve = GetDayToSolve();
            PuzzleAPI.Solve(dayToSolve);

            Console.WriteLine();
            Console.WriteLine("Would you like to solve another puzzle? (Y/n)");
            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.N || key.Key == ConsoleKey.Escape)
                ApplicationIsRunning = false;
        }

        private static int GetDayToSolve()
        {
            return 6;

            int day;

            Console.WriteLine("Which day would you like to solve?");
            string? userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out day))
            {
                Console.WriteLine("Invalid input, try again.");
                userInput = Console.ReadLine();
            }

            if (day < 1 || day > 25)
            {
                Console.WriteLine("Out of festive range exception");
                return GetDayToSolve();
            }

            return day;
        }
    }
}