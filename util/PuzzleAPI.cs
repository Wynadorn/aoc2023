using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace util
{
    public static class PuzzleAPI
    {
        private static readonly string BASE_INPUT_URL = "https://adventofcode.com/2023/day/";
        private static readonly string CACHE_ROOT = @"C:\Src\usr\aoc\aoc2023\cache\";

        public static string GetPuzzleInput(int day, int star = 1, bool forceRefresh = false)
        {
            if (day < 1 || day > 25)
                throw new ArgumentOutOfRangeException(nameof(day), "Out of festive range exception");

            string cacheFileName = Path.Combine(CACHE_ROOT, $"{day}-{star}-input.txt");

            if (File.Exists(cacheFileName) && !forceRefresh)
                return File.ReadAllText(cacheFileName);

            var url = new Uri($"{BASE_INPUT_URL}{day}/input");

            using HttpClientHandler handler = new HttpClientHandler { UseCookies = false };
            using HttpClient client = new HttpClient(handler);

            var message = new HttpRequestMessage(HttpMethod.Get, url);
            message.Headers.Add("Cookie", File.ReadAllText($".\\cache\\cookie.txt"));
            HttpResponseMessage result = client.Send(message);
            result.EnsureSuccessStatusCode();

            string? stringResult = result.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(stringResult))
            {
                var directory = Path.GetDirectoryName(cacheFileName);

                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                File.WriteAllText(cacheFileName, stringResult);
                return stringResult;
            }
            else
            {
                throw new Exception($"Unable to get puzzle input for day {day}.");
            }
        }

        public static void Solve(Puzzle puzzle)
        {
            puzzle.GetSolution();

            ISolver? solver;

            try
            {
                Type? type = Type.GetType($"AoC2022.Days.Day{day}", true);
                if (type == null)
                    throw new Exception("Couln't find solver");

                solver = Activator.CreateInstance(type) as ISolver;
                if (solver == null)
                    throw new Exception("Couln't find solver");
            }
            catch (Exception)
            {
                ShowFail(day);
                return;
            }

            string rawInput = GetPuzzleInput(day);
            solver.Solve(rawInput);
            return;
        }

        //private static void ShowFail(int day)
        //{
        //    Console.WriteLine($"Unable to find a solution for day {day}.");
        //}
    }
}