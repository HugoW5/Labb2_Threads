namespace Labb2_Threads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Race race = new([
                new Car("Pick up  🛻"),
                new Car("Brandbil 🚒"),
                new Car("Polisbil 🚓"),
                new Car("Buss     🚌"),
                ], 5000);
            race.StartRace();
        }
    }
}
