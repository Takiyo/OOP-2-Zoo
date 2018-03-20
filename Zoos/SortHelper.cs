using Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoos
{
    /// <summary>
    /// The class used to represent a sorting algorithm helper.
    /// </summary>
    public static class SortHelper
    {
        /// <summary>
        /// Runs a bubble sorting algorithm.
        /// </summary>
        /// <param name="animals">The list of animals to be sorted through.</param>
        /// <returns>The sorted list.</returns>
        public static SortResult BubbleSortByWeight(List<Animal> animals)
        {
            SortResult result = null;
            return result;
        }

        private static void Swap(List<Animal> animals, int index1, int index2)
        {
            Animal placeholderAnimal;
            placeholderAnimal = animals[index1];
            animals[index1] = animals[index2];
            animals[index2] = placeholderAnimal;
        }
    }
}
