using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSortBase
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] sample = {12, 11, 13, 5, 6, 7};
            foreach (int i in sample)
            {
                Console.Write($"{i}, ");
            }

            var x = MergeSort<int>.Do(sample);
            Console.WriteLine("");
            foreach (int i in x)
            {
                Console.Write($"{i}, ");
            }

            var y = MergeSort<int>.History;
            Console.ReadLine();
        }
    }
}
