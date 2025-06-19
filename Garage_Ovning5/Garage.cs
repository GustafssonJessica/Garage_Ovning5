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
        internal void ParkVehicle(T vehicle)
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
