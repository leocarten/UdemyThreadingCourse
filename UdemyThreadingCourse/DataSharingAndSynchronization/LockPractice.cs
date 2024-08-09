using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.DataSharingAndSynchronization
{
    class LockPractice
    {
        public object myLock = new object();
        private int accountBalance;

        public LockPractice()
        {
            this.accountBalance = 0;
        }

        public void Deposit(int amount)
        {
            lock (myLock)
            {
                this.accountBalance += amount;
                Console.WriteLine($"Just deposited ${amount}. Current balance is: {this.accountBalance}.");
            }
        }

        public void Withdraw(int amount)
        {
            lock (myLock)
            {
                if (this.accountBalance - amount > 0)
                {
                    this.accountBalance -= amount;
                    Console.WriteLine($"Just withdrew ${amount}. Current balance is: {this.accountBalance}.");
                }
                else
                {
                    Console.WriteLine($"Could not withdraw {amount} from {this.accountBalance}.");
                }

            }
        }

    }

    class Driver
    {
        static void Main_(string[] args)
        {
            var lockPractice = new LockPractice();
            var tasks = new List<Task>(); // create list of tasks
            Random rnd = new Random(); // random
            for (int i = 0; i < 20; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 2; j++)
                    {
                        int random_num = rnd.Next(1,100);
                        lockPractice.Deposit(random_num);

                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 2; j++)
                    {
                        int random_num = rnd.Next(50, 200);
                        lockPractice.Withdraw(random_num);

                    }
                }));

            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Main thread done.");
        }
    }

}
