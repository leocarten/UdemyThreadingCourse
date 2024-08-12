using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.Concurrenct_Collections
{
    class ProducerAndConsumer_
    {
        private BlockingCollection<int> messages = new BlockingCollection<int>(new ConcurrentBag<int>(), 10);

        
        public void RunProducer(CancellationTokenSource token)
        {
            while (true)
            {
                token.Token.ThrowIfCancellationRequested(); 
                int i = new Random().Next(1, 100);
                messages.Add(i); // if there are more than 10 messages, this thread gets blocked
                Console.WriteLine($"+ {i}");
                Thread.Sleep(10);
            }   
        }

        public void RunConsumer(CancellationTokenSource token) 
        {
            foreach (var item in messages.GetConsumingEnumerable())
            {
                token.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"- {item}");
                Thread.Sleep(900);
            }

        }

    }
    class ProducerAndConsumerDriver
    {
        static void Main(string[] args)
        {
            ProducerAndConsumer_ driver = new ProducerAndConsumer_();
            CancellationTokenSource cts = new CancellationTokenSource();

            var producer = Task.Factory.StartNew(() =>
            {
                driver.RunProducer(cts);
            });

            var consmumer = Task.Factory.StartNew(() =>
            {
                driver.RunConsumer(cts);
            });

            //try
            //{
            //    Task.WaitAll(new[] { producer, consmumer }, cts.Token);
            //}
            //catch (AggregateException ae)
            //{
            //    ae.Handle(e => true);
            //}

            Console.ReadKey();
            cts.Cancel();

        }
    }
}
