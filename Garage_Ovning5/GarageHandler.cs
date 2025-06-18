using Garage_Ovning5.Vehicles;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5
{
    internal class GarageHandler
    {
        private Garage<Vehicle>? _garage;


        public void CreateGarage(int maxCapacity)
        {
            try
            {
                _garage = new Garage<Vehicle>(maxCapacity);
                Console.WriteLine("Ett nytt garage har skapats!"); //Todo här har jag text, ej bra
            }
            catch (Exception)
            {
                Console.WriteLine("Något gick snett!");
            }
        }

        //Metod för att parkera ett fordon i garaget
        internal bool ParkVehicle(Vehicle vehicle)
        {
            //if (_garage == null)
            //{
            //    Console.WriteLine("Garaget är inte skapat. Skapa ett garage först.");
            //    return;
            //}

            //Funktion för att jämföra om vehiclen som skickas med har ett unikt registreringsnummer
            if (_garage.Any(v => v != null && v.RegistrationNumber.Equals(vehicle.RegistrationNumber, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"Fordon med registreringsnummer {vehicle.RegistrationNumber} finns redan i garaget.");
                return false;
            }

            _garage.ParkVehicle(vehicle);
            return true;
        }


        // Metod för att ta bort ett fordon från garaget baserat på registreringsnummer
        internal void DeleteVehicle(string regNumber)
        {
            if (_garage == null)
            {
                Console.WriteLine("Garaget är inte skapat. Skapa ett garage först."); //todo ta bort text x flera
                return;
            }

            // Hitta fordonet med rätt registreringsnummer med LINQ
            var vehicle = _garage.FirstOrDefault(v => v != null && v.RegistrationNumber.Equals(regNumber, StringComparison.OrdinalIgnoreCase));
            if (vehicle == null)
            {
                Console.WriteLine($"Inget fordon med registreringsnummer {regNumber} kunde hittas i garaget");
                return;
            }

            bool removed = (_garage as dynamic).RemoveVehicle(regNumber);
            if (removed)
            {
                Console.WriteLine($"Fordonet med registreringsnummer {regNumber} har tagits bort från garaget");
            }
            else
            {
                Console.WriteLine($"Kunde inte ta bort fordonet med regnummer {regNumber}.");
            }
        }


        /*
        internal void ParkAirplane(string regNumber, string brand, Color vehicleColor, int numberOfSeats)
        {

            if (_garage == null)
            {
                Console.WriteLine("Garaget är inte skapat. Skapa ett garage först.");
                return;
            }

            _garage.ParkAirplane(regNumber, brand, vehicleColor, numberOfSeats);
            Console.WriteLine($"FLygplan med regnummer {regNumber} är nu parkerad i garaget! \nDet finns {_garage.NumberOfAvailableSpots} platser kvar i garaget.");

        }

        internal void ParkBoat(string regNumber, string brand, Color vehicleColor, int length)
        {
            if (_garage == null)
            {
                Console.WriteLine("Garaget är inte skapat. Skapa ett garage först.");
                return;
            }
            {

            }
            _garage.ParkBoat(regNumber, brand, vehicleColor, length);
            Console.WriteLine($"Båt med regnummer {regNumber} är nu parkerad i garaget! \nDet finns {_garage.NumberOfAvailableSpots} platser kvar i garaget.");

        }

        internal void ParkBus(string regNumber, string brand, Color vehicleColor, bool hasSeatBelts)
        {
            if (_garage == null)
            {
                Console.WriteLine("Garaget är inte skapat. Skapa ett garage först.");
                return;
            }
            {

            }
            _garage.ParkBus(regNumber, brand, vehicleColor, hasSeatBelts);
            Console.WriteLine($"Buss med regnummer {regNumber} är nu parkerad i garaget! \nDet finns {_garage.NumberOfAvailableSpots} platser kvar i garaget.");
        }

        internal void ParkCar(string regNumber, string brand, Color vehicleColor, FuelType fuelType)
        {
            if (_garage == null)
            {
                Console.WriteLine("Garaget är inte skapat. Skapa ett garage först.");
                return;
            }
            _garage.ParkCar(regNumber, brand, vehicleColor, fuelType);
            Console.WriteLine($"Bil med regnummer {regNumber} är nu parkerad i garaget! \nDet finns {_garage.NumberOfAvailableSpots} platser kvar i garaget.");
        }

        internal void ParkMotorCycle(string regNumber, string brand, Color vehicleColor, int cylinderVolume)
        {
            if (_garage == null)
            {
                Console.WriteLine("Garaget är inte skapat. Skapa ett garage först.");
                return;
            }
            {

            }
            _garage.ParkMotorCycle(regNumber, brand, vehicleColor, cylinderVolume);
            Console.WriteLine($"Motorcykel med regnummer {regNumber} är nu parkerad i garaget! \nDet finns {_garage.NumberOfAvailableSpots} platser kvar i garaget.");
        }


        */

        // Metod för att returnera alla fordon i garaget
        public IEnumerable<Vehicle> ReturnAllVehicles()
        {
            if (_garage == null)
            {
                Console.WriteLine("Garaget är inte skapat. Skapa ett garage först.");
                return Enumerable.Empty<Vehicle>();
            }
            if (_garage.NumberOfParkedVehicles == 0)
            {
                Console.WriteLine("Det finns inga fordon i garaget");
                return Enumerable.Empty<Vehicle>();
            }
            return _garage.GetAllVehicles();
        }

        internal IEnumerable<Vehicle> SearchVehicles(string type, string regNumber, Color? color, string brand)
        {
            if (_garage == null)
            {
                Console.WriteLine("Garaget är inte skapat. Skapa ett garage först.");
                return Enumerable.Empty<Vehicle>();
            }
            if (string.IsNullOrEmpty(type) && string.IsNullOrEmpty(regNumber) && color == null && string.IsNullOrEmpty(brand))
            {
                Console.WriteLine("Ingen sökning gjord. Inget sökkriterium angivet");
                return Enumerable.Empty<Vehicle>();
            }
            return _garage.GetFilteredVehicles(type, regNumber, color, brand);
        }


        internal bool CheckIfGarageExists()
        {
            if (_garage == null)
            {
                return false;
            }
            return true;
        }

        internal Dictionary<string, int> GetVehicleTypes()
        {
            //Måste iterera genom fordonen och plussa på 1 för varje fordon som matchar fordonstypen
            //Borde gå att skapa en IEnumerable med aktuella fordon och sedan räkna antalet i varje lista
            // Måste sedan returnera antalet av varje typ. 

            if (_garage == null)
            {
                return new Dictionary<string, int>(); //eller annan lösning?
            }

            return _garage.Where(v => v != null).
                GroupBy(v => v.GetType().Name).
                ToDictionary(g => g.Key, g => g.Count()); //Nyckeln blir det som det grupperas efter på raden över (klassnamn)




            // var cars = _garage.Where(v => v.GetType() == typeof(Car));
            // int numberOfCars = cars.Count();

            // var boats = _garage.Where(v => v.GetType() == typeof(Boat));
            // int numberOfBoats = boats.Count();

            // var busses = _garage.Where(v => v.GetType() == typeof(Bus));
            // int numberOfBusses = busses.Count();

            // var airplanes = _garage.Where(v => v.GetType() == typeof(Airplane));
            // int numberOfAirplanes = airplanes.Count();

            // var motorcycles = _garage.Where(v => v.GetType() == typeof(Motorcycle));
            // int numberOfMotorcycles = motorcycles.Count();

            //return $"Bilar: {numberOfCars} Båtar: {numberOfBoats} Bussar: {numberOfBusses} Flygplan: {numberOfAirplanes} Motorcyklar: {numberOfMotorcycles}";

        }
    }

    //Metod för att söka fram fordon baserat på egenskaper i basklassen

    //Metod för att visa alla parkerade fordon

    //Metod för att lägga till 10 fordon med ett knapptryck (någon typ av seed??)


}