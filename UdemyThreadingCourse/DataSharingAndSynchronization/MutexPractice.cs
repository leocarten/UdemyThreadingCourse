using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.DataSharingAndSynchronization
{
    public class BankAccount3
    {
        public int Balance { get; set; }
        public object padLock = new object();
        public void Deposit(int amount)
        {
            Balance += amount;
        }
        public void Withdraw(int amount)
        {
            Balance -= amount;
        }
        public void Transfer(BankAccount3 where, int amount)
        {
            this.Balance -= amount;
            where.Balance += amount;
            Console.WriteLine($"Just transferred {amount}.");
        }
    }

    class MutexPractice
    {
        static void Main_(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount3();
            var ba2 = new BankAccount3();

            Mutex mutex = new Mutex();
            Mutex mutex2 = new Mutex();

            for (int i = 0; i < 10; ++i)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; ++j)
                    {
                        bool haveLock = mutex.WaitOne(); // wait for mutex to be available
                        try
                        {
                            ba.Deposit(1); // deposit 10000 overall
                        }
                        finally
                        {
                            if (haveLock) mutex.ReleaseMutex(); // relase the lock
                        }
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; ++j)
                    {
                        bool haveLock = mutex2.WaitOne(); // new mutex2 to control ba2
                        try
                        {
                            ba2.Deposit(1); // deposit 10000
                        }
                        finally
                        {
                            if (haveLock) mutex2.ReleaseMutex();
                        }
                    }
                }));

                // transfer needs to lock both accounts
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = Mutex.WaitAll(new[] { mutex, mutex2 }); // wait for both mutex and mutex2 to be available
                        try
                        {
                            ba.Transfer(ba2, 1); 
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex.ReleaseMutex(); // release
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance is: ba={ba.Balance}, ba2={ba2.Balance}.");
        }
    }
}
