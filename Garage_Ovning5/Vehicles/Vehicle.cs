using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Garage_Ovning5.Vehicles
{
    internal class Vehicle
    {
       
        public string _registrationNumber { get; set; } //måste vara unikt
        public Color Color {  get; set; }
        public int NumberOfWheels { get; set; }



        public string RegistrationNumber
        {
            get => _registrationNumber;
            set
            {
                //om man gör det till en CharArray, och plats 0,1,2 är bokstav och plats 3,4,5 är siffror!
                for (int i = 0; i < 3; i++)
                {
                    if (!Regex.IsMatch(value[i].ToString(), "^[a-öA-Ö]$"))
                    {
                        throw new ArgumentException("Ogiltigt registreringsnummer!");
                    }
                    if()
                    
                }
                {

                }
            }
        }

    }
}
