using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5.Vehicles
{
    internal class Bus : Vehicle
    {
        public bool HasSeatBelts { get; set; }
        public Bus(string regNumber, string brand, Color color, bool seatBelts) : base(regNumber, brand, color)
        {
            HasSeatBelts = seatBelts;
        }
    }
}
