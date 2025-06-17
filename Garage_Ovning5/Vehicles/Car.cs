using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5.Vehicles
{
    internal class Car : Vehicle
    {
        public FuelType FuelType { get; set; }

        public Car(string regNumber, string brand, Color color, FuelType fuelType) : base(regNumber, brand, color)
        {
            FuelType = fuelType;
        }
    }
}
