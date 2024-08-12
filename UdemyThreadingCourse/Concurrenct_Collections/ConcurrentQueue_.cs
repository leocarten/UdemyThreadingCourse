using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.Concurrenct_Collections
{
    class ConcurrentQueue_
    {
        private ConcurrentQueue<string> q = new ConcurrentQueue<string>();
        public void addSongToQueue(string songTitle, int numTest)
        {
            Console.WriteLine(numTest);
            q.Enqueue(songTitle);
        }
        public void removeSongFromQueue(string songTitle, int numTest)
        {
            if (numTest == 1)
            {
                Console.WriteLine("Task got it");
                q.TryDequeue(out songTitle);
            }
            else
            {
                Console.WriteLine("Main got it");
                q.TryDequeue(out songTitle);
            }
        }
    }
    class ConQueDriver
    {
        static void Main_(string[] args)
        {
            ConcurrentQueue_ q = new ConcurrentQueue_();

            Task.Factory.StartNew(() =>
            {
                q.addSongToQueue("BANG", 1);
                //q.removeSongFromQueue("BANG", 1);
            });

            q.addSongToQueue("Borderline", 2);
            //q.removeSongFromQueue("BANG", 2);



        }
    }
}
