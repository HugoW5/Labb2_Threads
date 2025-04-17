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
            return true;
                }
                return false;
            },
            (Car car) => {
                if(Rng.Next(0, 25) == 0){
                    Console.WriteLine($"🛞 {car.Name} Behöver byta däck");
                    Thread.Sleep(10000);
                   return true;
                }
                return false;
            },
            (Car car) => {
                if (Rng.Next(0, 10) == 0)
                {
                    Console.WriteLine($"🦅 {car.Name} Behöver tvätta vindrutan");
                    Thread.Sleep(5000);
                   return true;
                }
                return false;
            },
            (Car car) => {
                if (Rng.Next(0, 5) == 0)
                {
                    Console.WriteLine($"👨‍🔧 {car.Name} Fick motor fel, Hastigheten på bilen sänks med 1 km/h");
                    car.Speed -= 1;
                   return true;
                }
                return false;
            }
        ];
        private List<Car> FinishedCars = new List<Car>();
        public List<Car> Cars { get; set; } = new List<Car>();
        public int Length { get; set; }

        public void StartRace()
        {
            Console.WriteLine("Tävlingen har startat");
            foreach (var car in Cars)
            {
                car.Stopwatch.Start();
                car.Thread = new Thread(() =>
                {
                    while (car.Distance < Length)
                    {
                        Thread.Sleep(1000);
                        car.Timer++;
                        car.Distance += (car.Speed / 3.6);

                        if (car.Timer == 10)
                        {
                            car.Timer = 0;
                            foreach (var problem in Problems)
                            {
                                if ((bool)problem.DynamicInvoke(car)!)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    car.Stopwatch.Stop();
                    if (FinishedCars.Count == 0)
                    {
                        Console.WriteLine($"{car.Name} Vann. Tid: {car.Stopwatch.Elapsed.ToString(@"mm\:ss")}s");
                    }
                    else
                    {
                        Console.WriteLine($"{car.Name} Kommer på {FinishedCars.Count + 1} plats. Tid: {car.Stopwatch.Elapsed.ToString(@"mm\:ss")}");
                    }
                    FinishedCars.Add(car);
                });
                car.Thread.Start();
            }
            RaceInformaiton();

        }
        public Race(List<Car> raceCars, int length)
        {
            Length = length;
            Cars = raceCars;
        }
        private void RaceInformaiton()
        {
            while (FinishedCars.Count != Cars.Count)
            {
                while (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {

                        foreach (var car in Cars)
                        {
                            Console.WriteLine($"{car.Name}\t{car.Speed}km/h\t{CalculateCarProgress(car.Distance)}%");
                        }
                    }
                }
            }
            Console.WriteLine('\n' + new string('-', 35));
            for (int i = 0; i < FinishedCars.Count; i++)
            {
                Console.WriteLine($"{i+1}. {FinishedCars[i].Name} Kommer på {i + 1} plats. Tid: {FinishedCars[i].Stopwatch.Elapsed.ToString(@"mm\:ss")}");
            }
        }
        private string CalculateCarProgress(double distance)
        {
            if (((distance / (float)Length) * 100) > 100)
            {
                return "100";
            }
            return $"{((distance / (float)Length) * 100).ToString("N2")}";
        }
    }
}
