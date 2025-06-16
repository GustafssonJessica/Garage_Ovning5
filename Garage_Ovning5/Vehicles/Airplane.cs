using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5.Vehicles
{
    internal class Airplane : Vehicle
    {
        private int _numberOfSeats;

        public int NumberOfSeats
        {
            get => _numberOfSeats;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Värdet får inte vara negativt");
                }
                _numberOfSeats = value;
            }
        }




        public Airplane(string regNumber, string brand, Color color, int numbOfSeats) : base(regNumber, brand, color)
        {
            NumberOfSeats = numbOfSeats;
        }


    }
}
