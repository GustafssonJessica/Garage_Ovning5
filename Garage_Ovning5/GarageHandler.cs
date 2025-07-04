﻿using Garage_Ovning5.Vehicles;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5
{
    public class GarageHandler : IGarageHandler
    {
        private Garage<Vehicle>? _garage;

        // Metod för att skapa ett garage med en maxkapacitet
        public bool CreateGarage(int maxCapacity)
        {
            try
            {
                _garage = new Garage<Vehicle>(maxCapacity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Metod för att parkera ett fordon i garaget
        public bool ParkVehicle(Vehicle vehicle)
        {
            //Funktion för att jämföra om fordonet som skickas med har ett unikt registreringsnummer
            if (_garage!.Any(v => v != null && v.RegistrationNumber.Equals(vehicle.RegistrationNumber, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }
            if (_garage!.IsFull)
            {
                return false;
            }
            _garage.ParkVehicle(vehicle);
            return true;
        }


        // Metod för att ta bort ett fordon från garaget baserat på registreringsnummer
        public bool DeleteVehicle(string regNumber)
        {
            var vehicle = _garage!.FirstOrDefault(v => v != null && v.RegistrationNumber.Equals(regNumber, StringComparison.OrdinalIgnoreCase));
            if (vehicle == null)
            {
                return false;
            }

            _garage!.RemoveVehicle(regNumber);
            return true;
        }

        // Metod för att returnera alla fordon i garaget
        public IEnumerable<Vehicle> ReturnAllVehicles()
        {
            if (_garage!.NumberOfParkedVehicles == 0)
            {
                return Enumerable.Empty<Vehicle>();
            }
            return _garage.GetAllVehicles();
        }

        // Metod för att hämta fordon i garaget baserat på olika kriterier
        public IEnumerable<Vehicle> SearchVehicles(string type, string regNumber, Color? color, string brand)
        {
            if (string.IsNullOrEmpty(type) && string.IsNullOrEmpty(regNumber) && color == null && string.IsNullOrEmpty(brand))
            {
                return Enumerable.Empty<Vehicle>();
            }
            return _garage!.GetFilteredVehicles(type, regNumber, color, brand);
        }


        // Metod för att kontrollera om garaget finns och har skapats
        public bool CheckIfGarageExists()
        {
            if (_garage == null)
            {
                return false;
            }
            return true;
        }

        // Metod för att hämta fordonstyper och antal i garaget
        public Dictionary<string, int> GetVehicleTypes()
        {
            if (_garage == null)
            {
                return new Dictionary<string, int>();
            }

            return _garage.Where(v => v != null).
                GroupBy(v => v.GetType().Name).
                ToDictionary(g => g.Key, g => g.Count()); //Nyckeln blir det som det grupperas efter på raden över (klassnamn)
        }

        // Metod för att kontrollera om garaget är fullt
        public bool CheckIfGarageIsFull()
        {
            if (_garage != null && _garage.IsFull)
            {
                return true;
            }
            return false;
        }

        public void CreateDefaultGarage()
        {
            _garage = new Garage<Vehicle>(10);
            _garage!.Park5Vehicles();
        }
    }
}