using Garage_Ovning5.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5
{
    internal class GarageHandler
    {
        private Garage<Vehicle> _garage;

        public GarageHandler(Garage<Vehicle> garage)
        {
            _garage = garage;
        }
    }
}