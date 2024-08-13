using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.Parallel_Loops
{
    class StoppingParallelLoops
    {
    }
    class StoppingParallelLoopsDriver
    {
        static void Main_(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            long parallel_sum = 0;
            stopwatch.Start();
            Parallel.For(1, 1_000_000, (int myNum, ParallelLoopState state) => // this is how you can cancel it, by having a state
            {
                if(myNum == 502)
                {
                    state.Stop();
                }
                else
                {
                    parallel_sum += myNum;
                }
            });
            stopwatch.Stop();
            Console.WriteLine(parallel_sum.ToString());
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Elapsed time parallel: {elapsed}");
        }
    }
}
