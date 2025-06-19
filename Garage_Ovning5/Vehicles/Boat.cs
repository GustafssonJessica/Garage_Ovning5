using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5.Vehicles
{
    public class Boat : Vehicle
    {
        private int _length;
        public int Length
        {
            get => _length;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Värdet måste vara positivt");
                }
                _length = value;
            }
        }

        public Boat(string regNumber, string brand, Color color, int length) : base(regNumber, brand, color)
        {
            Length = length;
        }
    }
}
