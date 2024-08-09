using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.DataSharingAndSynchronization
{
    public class BankAccount
    {
        public int Balance { get; set; }
        public object padLock = new object();
        public void Deposit(int amount)
        {
            // this is 2 operations
            // the '+=' operation reads, adds, and then writes back
            // to make sure we can run this atomically, we need a critical section 
            lock (padLock) // lock the padLock, do our thing (increment the value), then release it
            {
                Console.WriteLine("Deposit is going");
                Balance += amount;
            }
        }
        public void Withdraw(int amount)
        {
            lock (padLock)
            {
                Console.WriteLine("Withdraw is going");
                Balance -= amount;
            }
        }
    }

    class CriticalSections
    {
        static void Main_(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            for (int i = 0; i < 100; i++) {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 2; j++)
                    {
                        ba.Deposit(100);

                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 2; j++)
                    {
                        ba.Withdraw(100);

                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final Balance: ${ba.Balance}");
            // why do we NOT get the balance of 0? Well, its because the Deposit() and Withdraw() methods r not Atomic

        }
    }
}
