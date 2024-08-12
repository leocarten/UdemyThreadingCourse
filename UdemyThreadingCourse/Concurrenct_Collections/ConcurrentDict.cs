using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.Concurrenct_Collections
{
    class ConcurrentDict
    {
        private ConcurrentDictionary<string, string> capitals = new ConcurrentDictionary<string, string>();

        public void addItem(string key, string value, int testNum)
        {
            Console.WriteLine(testNum);
            capitals.TryAdd(key, value);
        }

        public void printDict()
        {
            foreach (var key in capitals.Keys)
            {
                Console.WriteLine($"{key}: {capitals[key]}");
            }

        }
        class DictTester
        {
            static void Main_(string[] args)
            {
                // in something like this, there really is no saying as to which thread will add an item first (the TASK, or the main thread)...
                ConcurrentDict map = new ConcurrentDict();
                Task.Factory.StartNew(() => // task to add item
                {
                    map.addItem("NH", "Concord", 1);
                });

                Task.Factory.StartNew(() => // task to add item
                {
                    map.addItem("MA", "Boston", 3);
                });

                map.addItem("FL", "Tallehasse", 2); // main thread

                // the concurrent collections come with built-in methods that help you with concurrency (for example, GetOrAdd helps you incase a TASK accesses the dictionary before the main thread)


                map.printDict();


            }
        }
    }
}
