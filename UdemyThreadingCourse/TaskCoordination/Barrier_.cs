using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.TaskCoordination
{
    class Barrier_
    {
        private Barrier barrier = new Barrier(2, b =>
        {
            Console.WriteLine($"Phase: {b.CurrentPhaseNumber} is done.");
        });
        
        public void BoilWater()
        {
            Console.WriteLine("Waiting for water to boil...");
            Thread.Sleep(2000);

            barrier.SignalAndWait();

            Console.WriteLine("Water is done! Pouring into cup.");
            barrier.SignalAndWait();
        }
        
        public void GetCup()
        {
            Console.WriteLine("Looking in cabinets for cup...");
            Thread.Sleep(100);
            barrier.SignalAndWait();
            Console.WriteLine("Found cup!");
            barrier.SignalAndWait();
        }

    }
    class BarrierDriver
    {
        static void Main_(string[] args)
        {
            var b = new Barrier_();
            var water = Task.Factory.StartNew(b.BoilWater);
            var cup = Task.Factory.StartNew(b.GetCup);

            var tea = Task.Factory.ContinueWhenAll(new[] {water, cup}, tasks =>
            {
                Console.WriteLine("Now we can make tea!");
            });

            tea.Wait();
        }
    }
}
