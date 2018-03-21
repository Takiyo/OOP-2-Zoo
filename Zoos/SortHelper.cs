using Animals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// Runs a bubble sorting algorithm by name.
        /// </summary>
        /// <param name="animals">The list of animals to be sorted through.</param>
        /// <returns>The sorted list.</returns>
        public static SortResult BubbleSortByName(List<Animal> animals)
        {
            // initialize a swap counter variable and stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int swapCounter = 0;

            // Initialize compare counter
            int compareCount = 0;

            // use a for loop to loop backward through the list
            // e.g. initialize the loop variable to one less than the length of the list and decrement the variable instead of increment
            for (int i = animals.Count - 1; i > 0; i--)
            {
                // loop forward as long as the loop variable is less than the outer loop variable
                for (int j = 0; j < i; j++)
                {
                    compareCount++;

                    // if the name of the current animal is more than the name of the next animal, 
                    // swap the two animals and increment the swap count
                    int comparedNames = string.Compare(animals[j].Name, animals[j + 1].Name);
                    if (comparedNames == 1)
                    {
                        SortHelper.Swap(animals, j, j + 1);
                        swapCounter++;
                    }
                }
            }

            stopwatch.Stop();
            SortResult result = new SortResult
            {
                Animals = animals,
                SwapCount = swapCounter,
                CompareCount = compareCount,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
            };

            // return the SortResult
            return result;

        }

        /// <summary>
        /// Runs a bubble sorting algorithm by weight.
        /// </summary>
        /// <param name="animals">The list of animals to be sorted through.</param>
        /// <returns>The sorted list.</returns>
        public static SortResult BubbleSortByWeight(List<Animal> animals)
        {
            // initialize a swap counter variable and stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int swapCounter = 0;

            // Initialize compare counter
            int compareCount = 0;

            // use a for loop to loop backward through the list
            // e.g. initialize the loop variable to one less than the length of the list and decrement the variable instead of increment
            for (int i = animals.Count - 1; i > 0; i--)
            {
                // loop forward as long as the loop variable is less than the outer loop variable
                for (int j = 0; j < i; j++)
                {
                    compareCount++;

                    // if the weight of the current animal is more than the weight of the next animal, 
                    // swap the two animals and increment the swap count
                    if (animals[j].Weight > animals[j + 1].Weight)
                    {
                        SortHelper.Swap(animals, j, j + 1);
                        swapCounter++;
                    }
                }
            }

            stopwatch.Stop();
            SortResult result = new SortResult
            {
                Animals = animals,
                SwapCount = swapCounter,
                CompareCount = compareCount,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
            };

            // return the SortResult
            return result;
        }

        /// <summary>
        /// Sorts animals by name with a selection algorithm.
        /// </summary>
        /// <param name="animals">The list to be sorted through.</param>
        /// <returns>The resulting sort.</returns>
        public static SortResult SelectionSortByName(List<Animal> animals)
        {
            // initialize a swap counter variable, smallest weight found, and stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int swapCounter = 0;
            int minNameIndex = 0;

            // Initialize compare counter
            int compareCount = 0;

            // loop forward through the list
            for (int i = 0; i < animals.Count - 1; i++)
            {
                // declare a variable to hold the animal with the current minimum weight
                // set the variable to the current animal
                Animal minName = animals[i];

                // loop through the remaining animals in the list to find the animal with the lowest weight
                for (int j = i + 1; j < animals.Count; j++)
                {                  
                    // if the weight of the current animal is less than the weight of the animal with the minimum weight,
                    // set the variable holding the animal with the current minimum weight to the current animal
                    int comparedNames = string.Compare(animals[j].Name, minName.Name);
                    if (comparedNames != 1)
                    {
                        minName = animals[j];
                        minNameIndex = j;
                    }
                }

                compareCount++;

                // after finding the animal with the lowest weight
                // if the current animal's weight does not equal the weight of the animal with 
                // current minimum weight, swap the two animals and increment the swap count
                if (animals[i].Name != minName.Name)
                {
                    SortHelper.Swap(animals, i, minNameIndex);
                    swapCounter++;
                }
            }

            stopwatch.Stop();
            SortResult result = new SortResult
            {
                Animals = animals,
                SwapCount = swapCounter,
                CompareCount = compareCount,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
            };

            // return the SortResult
            return result;
        }

        /// <summary>
        /// Sorts animals by weight with a selection algorithm.
        /// </summary>
        /// <param name="animals">The list to be sorted through.</param>
        /// <returns>The resulting sort.</returns>
        public static SortResult SelectionSortByWeight(List<Animal> animals)
        {
            // initialize a swap counter variable, smallest weight found, and stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int swapCounter = 0;
            int minWeightIndex = 0;

            // Initialize compare counter
            int compareCount = 0;

            // loop forward through the list
            for (int i = 0; i < animals.Count - 1; i++)
            {
                // declare a variable to hold the animal with the current minimum weight
                // set the variable to the current animal
                Animal minWeight = animals[i];

                // loop through the remaining animals in the list to find the animal with the lowest weight
                for (int j = i + 1; j < animals.Count; j++)
                {
                    compareCount++;

                    // if the weight of the current animal is less than the weight of the animal with the minimum weight,
                    // set the variable holding the animal with the current minimum weight to the current animal
                    if (animals[j].Weight < minWeight.Weight)
                    {
                        minWeight = animals[j];
                        minWeightIndex = j;
                    }
                }

                // after finding the animal with the lowest weight
                // if the current animal's weight does not equal the weight of the animal with 
                // current minimum weight, swap the two animals and increment the swap count
                if (animals[i].Weight != minWeight.Weight)
                {
                    SortHelper.Swap(animals, i, minWeightIndex);
                    swapCounter++;
                }
            }

            stopwatch.Stop();
            SortResult result = new SortResult
            {
                Animals = animals,
                SwapCount = swapCounter,
                CompareCount = compareCount,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
            };

            // return the SortResult
            return result;
        }

        /// <summary>
        /// Sorts animals by name using an insertion algorithm.
        /// </summary>
        /// <param name="animals">List to be sorted.</param>
        /// <returns>The resulting sort.</returns>
        public static SortResult InsertionSortByName(List<Animal> animals)
        {
            // initialize a swap counter variable and stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int swapCounter = 0;

            // Initialize compare counter
            int compareCount = 0;

            // loop forward through the list
            for (int i = 1; i < animals.Count; i++)
            {
                compareCount++;

                // loop backward through the section of the list that has already been sorted to find the correct spot for the animal
                // loop while the value is greater than 0 and while the current animal's name is less than the previous animal's name
                    for (int j = i; j > 0 && string.Compare(animals[j].Name, animals[j - 1].Name) != 1; j--)
                {
                    SortHelper.Swap(animals, j, j - 1);
                    swapCounter++;
                }
            }

            stopwatch.Stop();
            SortResult result = new SortResult
            {
                Animals = animals,
                SwapCount = swapCounter,
                CompareCount = compareCount,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
            };

            // return the SortResult
            return result;
        }

        /// <summary>
        /// Sorts animals by weight using an insertion algorithm.
        /// </summary>
        /// <param name="animals">List to be sorted.</param>
        /// <returns>The resulting sort.</returns>
        public static SortResult InsertionSortByWeight(List<Animal> animals)
        {
            // initialize a swap counter variable and stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int swapCounter = 0;

            // Initialize compare counter
            int compareCount = 0;

            // loop forward through the list
            for (int i = 1; i < animals.Count; i++)
            {
                compareCount++;

                // loop backward through the section of the list that has already been sorted to find the correct spot for the animal
                // loop while the value is greater than 0 and while the current animal's weight is less than the previous animal's weight
                for (int j = i; j > 0 && animals[j].Weight < animals[j - 1].Weight; j--)
                {
                    SortHelper.Swap(animals, j, j - 1);
                    swapCounter++;
                }
            }

            stopwatch.Stop();
            SortResult result = new SortResult
            {
                Animals = animals,
                SwapCount = swapCounter,
                CompareCount = compareCount,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
            };

            // return the SortResult
            return result;
        }

        /// <summary>
        /// Swaps 2 animals in the array.
        /// </summary>
        /// <param name="animals">The list to tamper with.</param>
        /// <param name="index1">The first index.</param>
        /// <param name="index2">The next index.</param>
        private static void Swap(List<Animal> animals, int index1, int index2)
        {
            Animal placeholderAnimal;
            placeholderAnimal = animals[index1];
            animals[index1] = animals[index2];
            animals[index2] = placeholderAnimal;
        }
    }
}
