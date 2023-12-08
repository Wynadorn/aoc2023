using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace util
{
    public enum Star
    {
        Star1,
        Star2
    }

    public class Puzzle
    {
        public string PuzzleInput => GetPuzzleInput(Day);
        public string[] PuzzleInputArray => PuzzleInput.Split('\n');

        private string GetPuzzleInput(int day, bool renewCache = false)
        {
            return PuzzleAPI.GetPuzzleInput(day, forceRefresh: renewCache);
        }

        public Func<string, string[], string> Star1Solver { get; set; } = (input, inputArray) => { return "No sovler set"; };
        public Func<string, string[], string> Star2Solver { get; set; } = (input, inputArray) => { return "No sovler set"; };

        public long Star1Millis { get; private set; }
        public long Star2Millis { get; private set; }

        public long Star1Ticks { get; private set; }
        public long Star2Ticks { get; private set; }

        public string Star1TimeString => $"Solved day {Day} star 1 in {Star1Millis}ms ({Star1Ticks} ticks)! Answer {Star1Answer}.";
        public string Star2TimeString => $"Solved day {Day} star 2 in {Star2Millis}ms ({Star2Ticks} ticks)! Answer {Star2Answer}.";

        public string Star1Answer { get; private set; }
        public string Star2Answer { get; private set; }


        private int _day;
        public int Day => _day;

        public Puzzle(int day)
        {
            _day = day;
        }

        public string GetSolution(Star star = Star.Star1, string input = "", string[] inputArray = null)
        {
            if (string.IsNullOrWhiteSpace(input))
                input = PuzzleInput;

            if(inputArray == null)
                inputArray = input.Split('\n');

            Func<string, string[], string> solver = star == Star.Star1 ? Star1Solver : Star2Solver;

            var watch = Stopwatch.StartNew();
            string answer = solver(input, inputArray);
            watch.Stop();

            if (star == Star.Star1)
            {
                Star1Ticks = watch.ElapsedTicks;
                Star1Millis = watch.ElapsedMilliseconds;
                Star1Answer = answer;
                Debug.WriteLine(Star1TimeString);
            }
            else
            {
                Star2Ticks = watch.ElapsedTicks;
                Star2Millis = watch.ElapsedMilliseconds;
                Star2Answer = answer;
                Debug.WriteLine(Star2TimeString);
            }
            
            return answer;
        }
    }
}