using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace util
{
    public interface ISolver
    {
        public virtual void Solve(string puzzleInput)
        {
            SolvePartOne(puzzleInput);
            SolvePartTwo(puzzleInput);
        }

        public abstract string SolvePartOne(string puzzleInput, string[] puzzleInputArray = null);
        public abstract string SolvePartTwo(string puzzleInput, string[] puzzleInputArray = null);
    }
}
