using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023.Solvers
{
    public interface ISolver
    {
        public virtual void Solve(string puzzleInput)
        {
            string answerOne = SolvePartOne(puzzleInput, puzzleInput.Split('\n', StringSplitOptions.RemoveEmptyEntries));
            string answerTwo = SolvePartTwo(puzzleInput, puzzleInput.Split('\n', StringSplitOptions.RemoveEmptyEntries));

            Console.WriteLine(answerOne);
            Console.WriteLine(answerTwo);
        }

        public abstract string SolvePartOne(string puzzleInput, string[] puzzleInputArray = null);

        public abstract string SolvePartTwo(string puzzleInput, string[] puzzleInputArray = null);
    }
}
