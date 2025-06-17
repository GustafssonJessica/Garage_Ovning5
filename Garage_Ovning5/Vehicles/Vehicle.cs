using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Garage_Ovning5.Vehicles
{
    public abstract class Vehicle
    {
        public string _registrationNumber { get; set; } //TOdo måste vara unikt
        public string Brand { get; set; }
        public Color Color { get; set; }


        public Vehicle(string regNumber, string brand, Color color)
        {
            _registrationNumber = regNumber;
            Brand = brand;
            Color = color;
        }

        public string RegistrationNumber
        {
            get => _registrationNumber;
            set
            {
                //Validerar att första 3 är bokstäver och sista 3 är siffror
                for (int i = 0; i < 3; i++)
                {
                    if (!Regex.IsMatch(value[i].ToString(), "^[a-öA-Ö]$"))
                    {
                        throw new ArgumentException("Ogiltigt registreringsnummer! Första tre tecken måste vara bokstäver");
                    }
                }
                for (int i = 3; i < 6; i++)
                    if (!Regex.IsMatch(value[i].ToString(), "^[0-9]$"))
                    {
                        throw new ArgumentException("Ogiltigt registreringsnummer! Sista tre tecken måste vara siffror.");
                    }
            }
        }
    }
}
