using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Schema;

namespace MergeSortBase
{
    public static class MergeSort<T>
    {
        public static List<T[]> History { get; set; }
        public static int Wave { get; set; }
        public static T[] Do(T[] input)
        {
            History = new List<T[]>();
            Wave = 0;
            return SortHalf(input);
        }

        private static T[] SortHalf(T[] input)
        {
            History.Add(input);
            if (input.Length == 1)
            {
                return input;
            }

            double numerator = input.Length;
            double prepivot = numerator / 2 ;
            int pivot = (int)Math.Ceiling(prepivot);
            T[] left = new T[pivot];
            T[] right = new T[input.Length - pivot];
            Array.Copy(input, 0, left, 0, pivot);
            Array.Copy(input, pivot, right, 0, input.Length - pivot);
            var SortedLeft = SortHalf(left);
            var SortedRight = SortHalf(right);
            var result = Merge(SortedLeft, SortedRight);
            return result;
        }

        private static T[] Merge(T[] left, T[] right)
        {
            int totalLength = left.Length + right.Length;
            int leftindex = 0;
            int rightindex = 0;
            T[] result = new T[totalLength];

            for (int i = 0; i < totalLength; i++)
            {
                if (leftindex == left.Length)
                {
                    result[i] = right[rightindex];
                    rightindex++;
                }
                else if (rightindex == right.Length)
                {
                    result[i] = left[leftindex];
                    leftindex++;
                }

                else if (Comparer<T>.Default.Compare(left[leftindex], right[rightindex]) < 0)
                {
                    result[i] = left[leftindex];
                    leftindex++;
                }
                else if (Comparer<T>.Default.Compare(left[leftindex], right[rightindex]) > 0)
                {
                    result[i] = right[rightindex];
                    rightindex++;
                }
                else if (Comparer<T>.Default.Compare(left[leftindex], right[rightindex]) == 0)
                {
                    result[i] = left[leftindex];
                    leftindex++;
                }
            }
            History.Add(result);
            return result;
        }
    }
}