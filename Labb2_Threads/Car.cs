using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Threads
{
    internal class Car
    {
        public Thread Thread { get; set; } = null!;
        public string Name { get; private set; }
        public int Speed { get; set; } = 120;
        public double Distance { get; set; }
        public Car(string initialName)
        {
            Name = initialName;
        }
        public Car(string initialName, int initialSpeed)
        {
            Name = initialName;
            Speed = initialSpeed;
        }
    }
}
