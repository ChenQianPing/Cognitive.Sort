using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cognitive.Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            var iArrary = new int[] {11, 2, 5, 7, 4, 42, 16};

            SortHelper<int>.PigeonHoleSort(iArrary);

            for (var m = 0; m < iArrary.Length; m++)
                Console.Write($"{iArrary[m]} ");

            Console.WriteLine();

            Console.Read();


        }
    }
}
