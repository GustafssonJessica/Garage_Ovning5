using System.Reflection.Metadata;

namespace Garage_Ovning5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UI ui = new UI();
            GarageHandler garageHandler = new GarageHandler();

            Manager manager = new Manager(ui, garageHandler);

            manager.Run();
        }
    }
}
