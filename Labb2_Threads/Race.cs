using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Threads
{
    internal class Race
    {
        public List<Car> Cars { get; set; } = new List<Car>();

        public void StartRace()
        {
            foreach (var car in Cars)
            {
                car.Thread = new Thread(() =>
                {
                    while (!car.CancellationTokenSource.Token.IsCancellationRequested)
                    {
                        car.Distance += car.Speed;
                        Console.WriteLine($"{car.Name} is at {car.Progress}%");
                        Thread.Sleep(1000);
                    }
                });
                car.Thread.Start();
            }
        }
    }
}
