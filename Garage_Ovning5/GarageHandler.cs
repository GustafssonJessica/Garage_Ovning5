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
        private Garage<Vehicle>? _garage;

        //public GarageHandler(Garage<Vehicle> garage)
        //{
        //    _garage = garage;
        //}


        public void CreateGarage(int maxCapacity)
        {
            _garage = new Garage<Vehicle>(maxCapacity);
        }

    }

    //Metod för att söka fram fordon baserat på egenskaper i basklassen

    //Metod för att visa alla parkerade fordon

    //Metod för att lägga till 10 fordon med ett knapptryck (någon typ av seed??)


}