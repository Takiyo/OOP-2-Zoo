using Animals;
using System;
using System.Collections;
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
    [Serializable]
    public static class SortHelper
    {
        /// <summary>
        /// Runs a bubble sorting algorithm by name.
        /// </summary>
        /// <param name="animals">The list of animals to be sorted through.</param>
        /// <returns>The sorted list.</returns>
        public static SortResult BubbleSort(IList list, Func<object, object, int> comparer)
        {
            // initialize a swap counter variable and stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int swapCounter = 0;

            // Initialize compare counter
            int compareCount = 0;

            // use a for loop to loop backward through the list
            // e.g. initialize the loop variable to one less than the length of the list and decrement the variable instead of increment
            for (int i = list.Count - 1; i > 0; i--)
            {
                // loop forward as long as the loop variable is less than the outer loop variable
                for (int j = 0; j < i; j++)
                {
                    compareCount++;

                    // if the name of the current animal is more than the name of the next animal, 
                    // swap the two animals and increment the swap count
                     if (comparer(list[j], list[j + 1]) > 0)
                    {
                        SortHelper.Swap(list, j, j + 1);
                        swapCounter++;
                    }
                }
            }

            stopwatch.Stop();
            SortResult result = new SortResult
            {
                Objects = list as List<object>,
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
        public static SortResult SelectionSort(IList list, Func<object, object, int> comparer)
        {
            // initialize a swap counter variable, smallest weight found, and stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int swapCounter = 0;
            int minAnimalIndex = 0;

            // Initialize compare counter
            int compareCount = 0;

            // loop forward through the list
            for (int i = 0; i < list.Count - 1; i++)
            {
                // declare a variable to hold the animal with the current minimum weight
                // set the variable to the current animal
                Object minObject = list[i];

                // loop through the remaining animals in the list to find the animal with the lowest weight
                for (int j = i + 1; j < list.Count; j++)
                {
                    // if the weight of the current animal is less than the weight of the animal with the minimum weight,
                    // set the variable holding the animal with the current minimum weight to the current animal
                    if (comparer(list[j], minObject) == -1)
                    {
                        minObject = list[j];
                        minAnimalIndex = j;
                    }
                }

                compareCount++;

                // after finding the animal with the lowest weight
                // if the current animal's weight does not equal the weight of the animal with 
                // current minimum weight, swap the two animals and increment the swap count
                if (comparer(list[i], minObject) != 0)
                {
                    SortHelper.Swap(list, i, minAnimalIndex);
                    swapCounter++;
                }
            }

            stopwatch.Stop();
            SortResult result = new SortResult
            {
                Objects = list as List<object>,
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
        public static SortResult InsertionSort(IList list, Func<object, object, int> comparer)
        {
            // initialize a swap counter variable and stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int swapCounter = 0;

            // Initialize compare counter
            int compareCount = 0;

            // loop forward through the list
            for (int i = 1; i < list.Count; i++)
            {
                compareCount++;

                // loop backward through the section of the list that has already been sorted to find the correct spot for the animal
                // loop while the value is greater than 0 and while the current animal's name is less than the previous animal's name
                    for (int j = i; j > 0 && comparer(list[j], list[j - 1]) != 1; j--)
                {
                    SortHelper.Swap(list, j, j - 1);
                    swapCounter++;
                }
                

                
            }

            stopwatch.Stop();
            SortResult result = new SortResult
            {
                Objects = list as List<object>,
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
        private static void Swap(IList list, int index1, int index2)
        {
            IList placeholderList;
            placeholderList = list[index1] as List<object>;
            list[index1] = list[index2];
            list[index2] = placeholderList;
        }

        /// <summary>
        /// Uses a quick sort algorithm to sort by name.
        /// </summary>
        /// <param name="animals">The animals to be sorted.</param>
        /// <param name="leftIndex">The lowest index of the list.</param>
        /// <param name="rightIndex">The highest index of the list.</param>
        /// <param name="sortResult">The resulting sort.</param>
        /// <returns></returns>
        public static SortResult QuickSort(IList list, int leftIndex, int rightIndex, SortResult sortResult, Func<object, object, int> comparer)
        {
            // define variables to keep track of the left and right points in the list
            // initialize them to the passed-in indexes
            int leftPointer = leftIndex;
            int rightPointer = rightIndex;

            // initialize a swap counter variable and stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int swapCounter = 0;

            // Initialize compare counter
            int compareCount = 0;

            // find the animal to pivot on (the middle animal in this case)
            Object pivotObject = list[(leftIndex + rightIndex) / 2];

            // define and initialize a loop variable
            bool done = false;

            // start looping
            while (!done)
            {
                // while the name of the animal at the left pointer spot in the list is less than the pivot animal's name
                while (comparer(list[leftPointer], pivotObject) < 0)
                {
                    // increment the left pointer
                    leftPointer++;
                    // increment the sort result's compare count
                    compareCount++;
                }

                // while the pivot animal's name is less than the name of the animal at the right pointer spot in the list
                while (comparer(pivotObject, list[rightPointer]) > 0)
                {
                    // decrement right pointer
                    rightPointer--;
                    // increment the sort result's compare count
                    compareCount++;
                }

                // if the left pointer is less than or equal to the right pointer
                if (leftPointer <= rightPointer)
                {
                    // swap the animals at the left pointer and right pointer spots
                    SortHelper.Swap(list, leftPointer, rightPointer);

                    // increment the sort result's swap count
                    swapCounter++;
                    // then increment the left pointer and decrement the right pointer
                    leftPointer++;
                    rightPointer--;
                }
                // if the left pointer is greater than the right pointer,
                // stop the outer while loop
                if (leftPointer > rightPointer)
                {
                    done = true;
                }
            }

            // if the left index is less than the right pointer
            // use recursion to sort the animals within the left index and right pointer
            if (leftIndex < rightPointer)
            {
                SortHelper.QuickSort(list, leftIndex, rightPointer, sortResult, comparer);
            }

            // if the left pointer is less than the right index
            // use recursion to sort the animals within the left pointer and right index
            if (leftPointer < rightIndex)
            {
                SortHelper.QuickSort(list, leftPointer, rightIndex, sortResult, comparer);
            }


            stopwatch.Stop();
            SortResult result = new SortResult
            {
                Objects = list as List<object>,
                SwapCount = swapCounter,
                CompareCount = compareCount,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
            };

            // return the SortResult
            return result;
        }
    }
}
