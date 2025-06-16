using Garage_Ovning5.Vehicles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5
{
    public class Garage<T> : IEnumerable<T> where T : Vehicle
        //internal class Garage<T> where T : Vehicle, IEnumerable<T>
    {
        private T[] ParkedVehicles { get; set; }
        public int MaxCapacity { get; private set; }

        public int NumberOfParkedVehicles { get; set; }

        public int NumberOfAvailableSpots => MaxCapacity - NumberOfParkedVehicles;
        public int GarageName { get; set; }

        public bool IsFull { get; set; }



        public Garage(int maxCapacity)
        {
            MaxCapacity = maxCapacity;
            ParkedVehicles = new T[maxCapacity];
        }

        //Metod för att kunna iterera över fordonen i garaget
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T vehicle in ParkedVehicles)
            {
                yield return vehicle;
            }

        }

        //Metod för att den generiska GetEnumerator ovan ska användas
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        //Metod för att parkera ett fordon

        //Metod för att ta bort ett fordon




    }
}
