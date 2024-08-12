using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.TaskCoordination
{
    class Continuations_
    {
    }

    class ContinuationDriver
    {
        static void Main_(string[] args)
        {
            //var task = Task.Factory.StartNew(() =>
            //{
            //    Console.WriteLine("Boil water");
            //});

            //var task2 = task.ContinueWith(t =>
            //{
            //    Console.WriteLine("Completed task1 of boiling water...");
            //    Console.WriteLine("Add Coffee");
            //});

            //var task3 = task2.ContinueWith(t =>
            //{
            //    Console.WriteLine("Completed task2\nFinally, pour into cup");
            //});

            //Task.WaitAll(task, task2, task3); // ContinueWith is a way to continue a task after something has happened



            var testTask = Task.Factory.StartNew(() => "TestTask doing computation ...");
            var testTask2 = Task.Factory.StartNew(() => "TestTask2 doing a lot of math stuff ...");
            var testTask3 = Task.Factory.ContinueWhenAll(new[] { testTask, testTask2 }, tasks =>
            {
                Console.WriteLine("Tasks done");
                foreach (var task in tasks)
                {
                    var result = task.Result;
                    Console.WriteLine(result);
                }
                Console.WriteLine("Everything done.");
            });

            testTask3.Wait(); // wait on Task 3 to finish (after the other tasks r all done)

        }
    }
}
