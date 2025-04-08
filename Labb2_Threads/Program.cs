namespace Labb2_Threads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Race race = new([
                new Car("Volvo"),
                new Car("Saab"),
                ], 1000);
            race.StartRace();
        }
    }
}
