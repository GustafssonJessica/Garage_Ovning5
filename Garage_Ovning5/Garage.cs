using Garage_Ovning5.Vehicles;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5
{
    public class Garage<T> : IEnumerable<T> where T : Vehicle
       
    {
        private T[] _parkedVehicles;
        public int MaxCapacity { get; private set; }

        public int NumberOfParkedVehicles { get; set; }

        public int NumberOfAvailableSpots => MaxCapacity - NumberOfParkedVehicles;

        public bool IsFull { get; set; }


        //Konstruktor som skapar ett garage med en maxkapacitet
        public Garage(int maxCapacity)
        {
            MaxCapacity = maxCapacity;
            _parkedVehicles = new T[maxCapacity];
        }

        //Metod för att kunna iterera över fordonen i garaget
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T vehicle in _parkedVehicles)
            {
                yield return vehicle;
            }

        }

        //Metod för att den generiska GetEnumerator ovan ska användas
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        // Metod för att parkera en bil i garaget
        internal void ParkCar(string regNumber, string brand, Color vehicleColor, FuelType fuelType)
        {
            if (NumberOfParkedVehicles == MaxCapacity)
            {
                Console.WriteLine("Tyvärr, garaget är fullt. Bilen kunde inte parkeras"); //TOdo, ändra så att park-metoderna returnerar en bool som isåfall leder till felemddelande istället
                return;
            }

            Vehicle newCar = new Car(regNumber, brand, vehicleColor, fuelType);

            for (int i = 0; i < _parkedVehicles.Length; i++)
            {
                if (_parkedVehicles[i] == null)
                {
                    _parkedVehicles[i] = (T)newCar;
                    NumberOfParkedVehicles++;
                    break;
                }
            }
        }

        internal void ParkMotorCycle(string regNumber, string brand, Color vehicleColor, int cylinderVolume)

        {
            Vehicle newMotorcycle = new Motorcycle(regNumber, brand, vehicleColor, cylinderVolume);
            for (int i = 0; i < _parkedVehicles.Length; i++)
            {
                if (_parkedVehicles[i] == null)
                {
                    _parkedVehicles[i] = (T)newMotorcycle;
                    NumberOfParkedVehicles++;
                    break;
                }
            }
        }

        internal void ParkBoat(string regNumber, string brand, Color vehicleColor, int length)
        {
            Vehicle newBoat = new Boat(regNumber, brand, vehicleColor, length);
            for (int i = 0; i < _parkedVehicles.Length; i++)
            {
                if (_parkedVehicles[i] == null)
                {
                    _parkedVehicles[i] = (T)newBoat;
                    NumberOfParkedVehicles++;
                    break;
                }
            }
        }

        internal void ParkBus(string regNumber, string brand, Color vehicleColor, bool hasSeatBelts)
        {
            Vehicle newBus = new Bus(regNumber, brand, vehicleColor, hasSeatBelts);
            for (int i = 0; i < _parkedVehicles.Length; i++)
            {
                if (_parkedVehicles[i] == null)
                {
                    _parkedVehicles[i] = (T)newBus;
                    NumberOfParkedVehicles++;
                    break;
                }
            }
        }

        internal void ParkAirplane(string regNumber, string brand, Color vehicleColor, int numberOfSeats)
        {
            Vehicle newAirplane = new Airplane(regNumber, brand, vehicleColor, numberOfSeats);
            for (int i = 0; i < _parkedVehicles.Length; i++)
            {
                if (_parkedVehicles[i] == null)
                {
                    _parkedVehicles[i] = (T)newAirplane;
                    NumberOfParkedVehicles++;
                    break;
                }
            }
        }

        //Metod för att ta bort ett fordon
        internal bool RemoveVehicle(string regNumber)
        {
            for (int i = 0; i < _parkedVehicles.Length; i++)
            {
                if (_parkedVehicles[i] != null &&
                    string.Equals(_parkedVehicles[i].RegistrationNumber, regNumber, StringComparison.OrdinalIgnoreCase)) //Jämför strängarna, ok med stora/små bokstäver
                {
                    _parkedVehicles[i] = null; //todo fixa den udnerstrukna
                    NumberOfParkedVehicles--;
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return _parkedVehicles.Where(v => v != null); //Returnerar alla fordon som inte är null
        }

        // Metod för att hämta filtrerade fordon i garaget med hjälp av LINQ
        internal IEnumerable<Vehicle> GetFilteredVehicles(string type, string regNumber, Color? color, string brand)
        {
            var selection = _parkedVehicles.Where(v => v != null); //sorterar bort alla null-värden i arrayen
            var selection2 = selection.Where(v =>
                    (string.IsNullOrEmpty(type) || v.GetType().Name.Equals(type, StringComparison.OrdinalIgnoreCase)) && //todo kanske inte  inte optimalt att jämföra med namn på klasserna
                    (string.IsNullOrEmpty(regNumber) || v.RegistrationNumber.Equals(regNumber, StringComparison.OrdinalIgnoreCase)) &&
                    (!color.HasValue || v.Color == color.Value) &&
                    (string.IsNullOrEmpty(brand) || v.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase))
                );
            return selection2;
        }
    }
}
