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

        public int NumberOfAvailableSpots => MaxCapacity - NumberOfParkedVehicles; //TOdo ta bort denna, finns ingen referens till den

        public bool IsFull => NumberOfParkedVehicles == MaxCapacity ? true : false;


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


        //Metod för att parkera ett fordon i garaget 
        public void ParkVehicle(T vehicle)
        {
            if (IsFull)
                throw new InvalidOperationException("Garaget är fullt.");//TOdo gör så att den returnerar en bool
            for (int i = 0; i < _parkedVehicles.Length; i++)
            {
                if (_parkedVehicles[i] == null)
                {
                    _parkedVehicles[i] = vehicle;
                    NumberOfParkedVehicles++;
                    return;
                }
            }
        }

        //Metod för att ta bort ett fordon
        public bool RemoveVehicle(string regNumber)
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

        //Metod för att hämta alla fordon i garaget
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return _parkedVehicles.Where(v => v != null); //Returnerar alla fordon som inte är null
        }

        // Metod för att hämta filtrerade fordon i garaget med hjälp av LINQ
        public IEnumerable<Vehicle> GetFilteredVehicles(string type, string regNumber, Color? color, string brand)
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


        // Metod för att köra testläge, skapar ett garage och parkerar fordon för testning
        public void Park5Vehicles()
        {
            ParkVehicle((T)(Vehicle)new Car("ABC123", "Volvo", Color.Red, FuelType.Gasoline));
            ParkVehicle((T)(Vehicle)new Motorcycle("DEF456", "Yamaha", Color.Blue, 600));
            ParkVehicle((T)(Vehicle)new Boat("GHI789", "Buster", Color.Green, 5));
            ParkVehicle((T)(Vehicle)new Bus("JKL012", "Scania", Color.Yellow, true));
            ParkVehicle((T)(Vehicle)new Airplane("MNO345", "Boeing", Color.White, 150));
        }
    }
}
