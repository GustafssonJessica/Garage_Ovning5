using Garage_Ovning5.Vehicles;

namespace Garage_Ovning5
{
    public interface IGarageHandler
    {
        bool CheckIfGarageExists();
        bool CheckIfGarageIsFull();
        void CreateDefaultGarage();
        bool CreateGarage(int maxCapacity);
        bool DeleteVehicle(string regNumber);
        Dictionary<string, int> GetVehicleTypes();
        bool ParkVehicle(Vehicle vehicle);
        IEnumerable<Vehicle> ReturnAllVehicles();
        IEnumerable<Vehicle> SearchVehicles(string type, string regNumber, Color? color, string brand);
    }
}