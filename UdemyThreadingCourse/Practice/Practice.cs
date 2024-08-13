using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.Practice
{
    class Practice
    {
        static void Main_(string[] args)
        {
            //// write a LINQ as practice
            //int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
            ////var filtered = arr.Where(variable => variable > 4 && variable < 12 ).AsParallel();
            //var filtered = arr.Where(x => x > 5 && x < 14);

            //// use a parallel-for loop to generate squares of things
            //var para_arr = new int[arr.Length];
            //Parallel.For(0, arr.Length, num =>
            //{
            //    Console.WriteLine($"number: {num}\tid: {Task.CurrentId}");
            //    para_arr[num] = num * num;
            //});

            //foreach (var item in para_arr)
            //{
            //    Console.WriteLine(item);
            //}

        }
    }
}
