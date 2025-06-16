using System.Reflection.Metadata;

namespace Garage_Ovning5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UI ui = new UI(); // Du kan även använda dependency injection om du vill
            GarageHandler garageHandler = new GarageHandler();

            Manager manager = new Manager(ui, garageHandler);

            manager.Run();

            //Skapar upp en manager (typ game.cs)

  







        }
    }
}
