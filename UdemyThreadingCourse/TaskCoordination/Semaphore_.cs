using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.TaskCoordination
{
    class Semaphore_
    {

    }
    class SemaphoreDriver
    {
        static void Main_(string[] args)
        {
            var sem = new SemaphoreSlim(2, 4); // it takes a max as to how many requests it can handle concurrently
            for (int i = 0; i < 12; i++) // spawn 20 threads
            {
                Task.Factory.StartNew(() => 
                {
                    Console.WriteLine($"Entering Task: {Task.CurrentId}");
                    sem.Wait();
                    Console.WriteLine($"Sem Count in for loops: {sem.CurrentCount}");
                    Console.WriteLine($"Processing Task: {Task.CurrentId}");
                });
            }
            while(sem.CurrentCount <= 2)
            { 
                // when you press a key, you allow 2 TASKS to execute 
                Console.WriteLine($"Sem Count in while loop: {sem.CurrentCount}");
                Console.ReadKey();
                sem.Release(2);
            }
        }
    }
}
