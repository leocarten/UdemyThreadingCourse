using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.Tasks
{
    class CancellingTasks
    {
        static void Main_(string[] args)
        {

            var cts = new CancellationTokenSource();
            var token = cts.Token;

            Console.WriteLine(cts.ToString());

            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine($"Thread with id {Task.CurrentId} cancelled.");
                        break;
                    }
                    else
                    {
                        Console.Write($"{i++}");
                    }
                }
            }, token);

            // start the thread, seperate from the main thread.
            t.Start();

            Console.ReadKey();
            cts.Cancel(); // cancel t, and stop it from running using the token by cancelling it
            /*
             * a TASK is a unit of work that gets picked up by a thread in the available threadpool
             * TASKs are then executed by threads in a threadPool
             *      (A threadPool is just an abstraction created by the C# stdlib that creates threads as you need)
             *      you can think of the threadPool as a dormitory --> instead of building your own house, then destroying it (threading), you have a dorm with 100 rooms that people can use and then leave (threadPool)
             * however, if you have an AWAIT keyword in a task, it releases the thread, executes by itself, and when its done, it gets added back to the queue of tasks to be picked up by threads in the thread pool
             * 
             */
        }
    }
}
