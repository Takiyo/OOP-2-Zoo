using Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoos
{
    /// <summary>
    /// The class which represents the result of the sorting algorithm.
    /// </summary>
    [Serializable]
    public class SortResult
    {
        /// <summary>
        /// Gets or sets the list of animals to be sorted.
        /// </summary>
        public List<object> Objects { get; set; }

        /// <summary>
        /// Gets or sets the compare count of the sort results.
        /// </summary>
        public int CompareCount { get; set; }

        /// <summary>
        /// Gets or sets how many milliseconds elapse during a sort.
        /// </summary>
        public double ElapsedMilliseconds { get; set; }

        /// <summary>
        /// Gets or sets the amount of times two values are swapped in an array.
        /// </summary>
        public int SwapCount { get; set; }
    }
}
