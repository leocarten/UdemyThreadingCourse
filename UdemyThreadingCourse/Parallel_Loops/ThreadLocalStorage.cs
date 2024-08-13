using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.Parallel_Loops
{
    class ThreadLocalStorage
    {
    }
    class ThreadLocalStorageDriver
    { 
        // this is a mess, but it works
        static void Main_(string[] args)
        {
            int sum = 0;
            Parallel.For(1, 1001,
                () => 0, 
                (x, state, tls) =>
                {
                    tls += x;
                    Console.WriteLine($"Task {Task.CurrentId} has sum {tls}");
                    return tls;
                },
                partialSum =>
                {
                    Interlocked.Add(ref sum, partialSum);
                }
            );
            Console.WriteLine($"Sum of 1...1000 = {sum}"); 
        }
    }
}
