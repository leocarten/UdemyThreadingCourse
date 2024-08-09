using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.Tasks
{
    class TasksPractice
    {
        public static void write(char c)
        {
            int i = 1000;
            while(i -- > 0)
            {
                Console.Write(c);
            }
        }

        public static void write(object c)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(c);
            }
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"Task with id {Task.CurrentId} is processing {o}...");
            return o.ToString().Length;
        }

        static void Main_(string[] args)
        {
            // concurrently writing to the buffer when you make tasks. the OS's thread scheduler decides which thread gets access to the lock
            /*            var t1 = new Task(() => write('.'));
                        var t2 = new Task(() => write('?'));

                        t1.Start();
                        t2.Start();

                        write('<')*/

            string text1 = "Testing";
            string text2 = "hi";

            var task1 = new Task<int>(TextLength, text1);
            task1.Start();

            Task<int> task2 = Task.Factory.StartNew<int>(TextLength, text2 );
            Console.WriteLine($"Length of '{text1}' = {task1.Result}");
            Console.WriteLine($"Length of '{text2}' = {task2.Result}");

            Console.WriteLine("Main program done.");

        }
    }
}
