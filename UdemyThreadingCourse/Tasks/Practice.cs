using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.Tasks
{
    class Practice
    {
        static void Main(string[] args)
        {
            var task1 = new Task<int>(() =>
            {
                int sum = 0;
                for (int i = 0; i < 10; i++) { 
                    sum += i;
                }
                Console.WriteLine(sum);
                return sum;
            });

            var task2 = new Task<int>(() =>
            {
                int sum = 0;
                for (int i = 10; i < 20; i++)
                {
                    sum += i;
                }
                Console.WriteLine(sum);
                return sum;
            });

            task1.Start();
            task2.Start();

            int zero_through_ten = task1.Result;
            int ten_through_twenty = task2.Result;
            Console.WriteLine(zero_through_ten + ten_through_twenty);
            Console.WriteLine("Main ended");
        }
    }
}
