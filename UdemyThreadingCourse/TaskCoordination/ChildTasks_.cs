using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.TaskCoordination
{
    class ChildTasks_
    {
    }
    class ChildTasksDriver
    {
        static void Main_(string[] args)
        {
            // what happens when you create a task within a task
            var parent = new Task(() =>
            {

                var child = new Task(() =>
                {
                    Console.WriteLine("Child executing...");
                    Thread.Sleep(1000);
                    Console.WriteLine("Child done.");
                }, TaskCreationOptions.AttachedToParent); // attach it to the parent

                var completed = child.ContinueWith(t => // continue with the child after it finished its previous task
                {
                    Console.WriteLine("Done");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

                child.Start();

            });
            parent.Start();

            parent.Wait();
        }
    }
}
