using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.LINQ_Parallelism
{
    class ParallelQueries
    {
    }

    class ParallelQueriesDriver
    {
        static void Main_(string[] args)
        {
            const int count = 50;
            var items = Enumerable.Range(0, count).ToArray();

            var results = new int[count];

            items.AsParallel().ForAll(x => // this populates 'results' array in parallel
            {
                int newVal = x * x;
                //Console.WriteLine($"id: {Task.CurrentId}");
                results[x] = newVal;
            });

            foreach (var item in results)
            {
                Console.WriteLine(item);
            }

        }
    }

}
