using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5.Vehicles
{
    internal class Motorcycle : Vehicle
    {
        public int CylinderVolume { get; set; }

        public Motorcycle (string regNumber, string brand, Color color, int cylVolume) : base(regNumber, brand, color)
        {
            CylinderVolume = cylVolume;
        }

    }
}
