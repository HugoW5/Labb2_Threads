using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Threads
{
    internal class Race
    {
        private static readonly Random Rng = new Random();
        private readonly List<Delegate> Problems = [
            (Car car) =>  {
                if(Rng.Next(0, 50) == 0)
                {
                    Console.WriteLine($"⛽ {car.Name} Fick slut på bensin!");
                    Thread.Sleep(15000);
                }
            },
            (Car car) => {
                if(Rng.Next(0, 25) == 0){
                    Console.WriteLine($"🛞 {car.Name} Behöver byta däck");
                    Thread.Sleep(10000);
                }
            },
            (Car car) => {
                if (Rng.Next(0, 10) == 0)
                {
                    Console.WriteLine($"🦅 {car.Name} Behöver tvätta vindrutan");
                    Thread.Sleep(5000);
                }
            },
            (Car car) => {
                if (Rng.Next(0, 5) == 0)
                {
                    Console.WriteLine($"👨‍🔧 {car.Name} Fick motor fel, Hastigheten på bilen sänks med 1 km/h");
                    car.Speed -= 1;
                }
            }
        ];
        private List<Car> FinishedCars = new List<Car>();
        public List<Car> Cars { get; set; } = new List<Car>();
        public int Length { get; set; } 

        public void StartRace()
        {
            foreach (var car in Cars)
            {
                car.Thread = new Thread(() =>
                {
                    while (car.Distance < Length)
                    {
                        Thread.Sleep(1000);
                        car.Distance += (car.Speed / 3.6);

                        foreach (var problem in Problems)
                        {
                            problem.DynamicInvoke(car);
                        }
                    }
                    if (FinishedCars.Count == 0)
                    {
                        Console.WriteLine($"{car.Name} Vann");
                    }
                    else
                    {
                        Console.WriteLine($"{car.Name} Kommer på {FinishedCars.Count + 1} plats");
                    }
                    FinishedCars.Add(car);
                });
                car.Thread.Start();
            }
            RaceInformaiton();

        }
        /// <summary>
        /// Race Constructor
        /// </summary>
        /// <param name="raceCars">Race cars</param>
        /// <param name="length">Race length</param>
        public Race(List<Car> raceCars, int length)
        {
            Length = length;
            Cars = raceCars;
        }
        private void RaceInformaiton()
        {
            while (true)
            {
                while (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                    {

                        foreach (var car in Cars)
                        {
                            Console.WriteLine($"{car.Name}\t{CalculateCarProgress(car.Distance)}%");
                        }
                    }
                }
            }
        }
        private string CalculateCarProgress(double distance)
        {
            return $"{((distance / (float)Length) * 100).ToString("N2")}";
        }
    }
}
