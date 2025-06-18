using Garage_Ovning5.Vehicles;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5
{
    public class GarageHandler : IGarageHandler
    {
        private Garage<Vehicle>? _garage;


        public bool CreateGarage(int maxCapacity)
        {
            try
            {
                _garage = new Garage<Vehicle>(maxCapacity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Metod för att parkera ett fordon i garaget
        public bool ParkVehicle(Vehicle vehicle)
        {
            //Funktion för att jämföra om vehiclen som skickas med har ett unikt registreringsnummer
            if (_garage.Any(v => v != null && v.RegistrationNumber.Equals(vehicle.RegistrationNumber, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }
            _garage.ParkVehicle(vehicle);
            return true;
        }


        // Metod för att ta bort ett fordon från garaget baserat på registreringsnummer
        public bool DeleteVehicle(string regNumber)
        {
            // Hitta fordonet med rätt registreringsnummer med LINQ
            var vehicle = _garage.FirstOrDefault(v => v != null && v.RegistrationNumber.Equals(regNumber, StringComparison.OrdinalIgnoreCase));
            if (vehicle == null)
            {
                return false;
            }

            _garage.RemoveVehicle(regNumber);
            return true;
        }

        // Metod för att returnera alla fordon i garaget
        public IEnumerable<Vehicle> ReturnAllVehicles() //todo lägga till de unika egenskaperna i utskriften?
        {
            if (_garage.NumberOfParkedVehicles == 0)
            {
                return Enumerable.Empty<Vehicle>();
            }
            return _garage.GetAllVehicles();
        }

        public IEnumerable<Vehicle> SearchVehicles(string type, string regNumber, Color? color, string brand)
        {
            if (string.IsNullOrEmpty(type) && string.IsNullOrEmpty(regNumber) && color == null && string.IsNullOrEmpty(brand))
            {
                return Enumerable.Empty<Vehicle>();
            }
            return _garage.GetFilteredVehicles(type, regNumber, color, brand);
        }


        //Metod för att kontrollera om garaget finns och har skapats
        public bool CheckIfGarageExists()
        {
            if (_garage == null)
            {
                return false;
            }
            return true;
        }

        // Metod för att hämta fordonstyper och antal i garaget
        public Dictionary<string, int> GetVehicleTypes()
        {
            if (_garage == null)
            {
                return new Dictionary<string, int>(); //eller annan lösning?
            }

            return _garage.Where(v => v != null).
                GroupBy(v => v.GetType().Name).
                ToDictionary(g => g.Key, g => g.Count()); //Nyckeln blir det som det grupperas efter på raden över (klassnamn)
        }
    }

    //Metod för att lägga till 10 fordon med ett knapptryck (någon typ av seed??)


}