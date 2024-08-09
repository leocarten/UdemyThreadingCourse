using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.DataSharingAndSynchronization
{
    class DriveCar
    {
        // 2 people drive a car
        // figure out when you need to re-fuel it
        public int GasInTank;
        public object myLock = new object();
        public DriveCar()
        {
            this.GasInTank = 0;
        }

        public void FillUpCar(int gallonsPutIn)
        {
            lock (myLock)
            {
                this.GasInTank += gallonsPutIn;
            }
        }
        public void Drive(int milesDriven)
        {
            lock (myLock)
            {
                this.GasInTank -= milesDriven;
            }
        }

    }
    class ProgramDriver
    {
        static void Main(string[] args)
        {
            var car = new DriveCar();
            var tasks = new List<Task>(); // create a list of tasks
            Random random = new Random();
            for (int i = 0; i < 1000; i++) 
            {
                tasks.Add(Task.Factory.StartNew(() => {
                    for (int j = 0; j < 100; j++) 
                    {
                        car.FillUpCar(2);
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() => {
                    for (int k = 0; k < 100; k++)
                    {
                        car.Drive(2);
                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Car has {car.GasInTank} miles in tank.");
        }
    }
}
