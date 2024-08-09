using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.DataSharingAndSynchronization
{
    public class BankAccount2
    {
        public int Balance { get; set; }
        public object padLock = new object();
        public void Deposit(int amount)
        {
            // this is 2 operations
            // the '+=' operation reads, adds, and then writes back
            // to make sure we can run this atomically, we need a critical section 
            Balance += amount;  
        }
        public void Withdraw(int amount)
        {
            Balance -= amount;
        }
    }

    class LockRecursion
    {
        static void Main_(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount2();
            SpinLock sl = new SpinLock();
            for (int i = 0; i < 100; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 2; j++)
                    {
                        var lockTaken = false;
                        try
                        { // try to take a lock, but sometimes you might fail
                            sl.Enter(ref lockTaken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if (lockTaken)
                            {
                                sl.Exit();
                            }
                        }

                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 2; j++)
                    {
                        var lockTaken = false;
                        try
                        { // try to take a lock, but sometimes you might fail
                            sl.Enter(ref lockTaken);
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if (lockTaken)
                            {
                                sl.Exit();
                            }
                        }

                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final Balance: ${ba.Balance}");
            // why do we NOT get the balance of 0? Well, its because the Deposit() and Withdraw() methods r not Atomic

        }
    }
}
