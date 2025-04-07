using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Threads
{
    internal class Car
    {
        public CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        private int _raceDistance = 5000;

        public Thread Thread { get; set; } = new Thread(() => { });
        public string Name { get; set; }
        public int Speed { get; set; } = 120;
        public decimal Progress { get; private set; } = 0;
        public int Distance
        {
            get
            {
                return Distance;
            }
            set
            {
                if (value >= _raceDistance)
                {
                    Distance = _raceDistance;
                }
                Progress = ((decimal)Distance / _raceDistance) * 100;
            }
        }
        public Car(string initialName)
        {
            Name = initialName;
        }


    }
}
