using Garage_Ovning5.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5
{
    internal class Manager
    {
        private UI _ui; //todo gör om dessa till interface
        private GarageHandler _garageHandler;

        public Manager(UI ui, GarageHandler garageHandler)
        {
            _ui = ui;
            _garageHandler = garageHandler;
        }

        internal void Run()
        {
            do
            {
                //Visar huvudmenyn och returnerar användarens val
                char choice = _ui.ShowMainMenu();

                //Kollar så att ett garage existerar innan vissa val kan göras
                bool garageExists = true;
                if (choice == '2' || choice == '3' || choice == '4' || choice == '5' || choice == '6')
                {
                    garageExists = GarageExists(); 
                }
                if (!garageExists)
                {
                    continue;
                }

                //Switch-sats för att hantera användarens val
                switch (choice)
                {
                    case '1':
                        //Skapar ett nytt garage
                        int garageSize = _ui.GetNewGarageInfo();
                       bool garageCreated = _garageHandler.CreateGarage(garageSize);
                        _ui.PrintGarageCreatedMessage(garageSize, garageCreated); // Meddelande om att garaget har skapats
                        break;
                    case '2':
                        //Parkera ett fordon
                        string typeOfVehicle = _ui.ReturnVehicleType(); // Metod för att få tillbaka fordonstypen
                        string regNumber;
                        string brand;
                        Color vehicleColor;
                        Vehicle? vehicle = null;

                        switch (typeOfVehicle)
                        {
                            case "Car":
                                FuelType fuelType;
                                _ui.GetCarInfo(out regNumber, out brand, out vehicleColor, out fuelType);
                                vehicle = new Car(regNumber, brand, vehicleColor, fuelType);
                              //  _garageHandler.ParkCar(regNumber, brand, vehicleColor, fuelType);
                                break;

                            case "Motorcycle":
                                int cylinderVolume;
                                _ui.GetMotorCycleInfo(out regNumber, out brand, out vehicleColor, out cylinderVolume);
                                vehicle = new Motorcycle(regNumber, brand, vehicleColor, cylinderVolume);
                                // _garageHandler.ParkMotorCycle(regNumber, brand, vehicleColor, cylinderVolume);
                                break;

                            case "Boat":
                                int length;
                                _ui.GetBoatInfo(out regNumber, out brand, out vehicleColor, out length);
                                vehicle = new Boat(regNumber, brand, vehicleColor, length);
                                //_garageHandler.ParkBoat(regNumber, brand, vehicleColor, length);
                                break;
                            case "Bus":
                                bool hasSeatBelts;
                                _ui.GetBusInfo(out regNumber, out brand, out vehicleColor, out hasSeatBelts);
                                vehicle = new Bus(regNumber, brand, vehicleColor, hasSeatBelts);
                                //_garageHandler.ParkBus(regNumber, brand, vehicleColor, hasSeatBelts);
                                break;
                            case "Airplane":
                                int numberOfSeats;
                                _ui.GetAirplaneInfo(out regNumber, out brand, out vehicleColor, out numberOfSeats);
                                vehicle = new Airplane(regNumber, brand, vehicleColor, numberOfSeats);
                                //_garageHandler.ParkAirplane(regNumber, brand, vehicleColor, numberOfSeats);
                                break;
                            default:
                                break;
                        }
                        if (vehicle != null)
                        {
                            bool succees = _garageHandler.ParkVehicle(vehicle);
                            if (succees)
                            {
                                _ui.PrintVehicleParkedMessage();
                            }
                            else
                            {
                                _ui.PrintGarageFullMessage();
                            }
                        }

                        break;
                    case '3':
                        // Radera ett fordon
                        regNumber = _ui.GetDeleteInfo();
                        bool vehicleRemoved = _garageHandler.DeleteVehicle(regNumber);
                        _ui.PrintVehicleRemovedMessage(vehicleRemoved, regNumber);
                        break;
                    case '4':
                        // Visa alla fordon i garaget
                        IEnumerable<Vehicle> vehicles = _garageHandler.ReturnAllVehicles();
                        _ui.PrintAllVehicles(vehicles);
                        break;
                        case '5':
                        //todo Lista fordonstyper och hur många av varje som står i garaget
                        var typesAndNumbers = _garageHandler.GetVehicleTypes();
                        _ui.ShowVehicleTypes(typesAndNumbers);
                        //Måste iterera genom fordonen och plussa på 1 för varje fordon som matchar fordonstypen
                        //Borde gå att skapa en IEnumerable med aktuella fordon och sedan räkna antalet i varje lista
                        //Sedan returnera en IEnumerable med fordonstyper och antal
                        break;
                    case '6':
                        // Sök efter fordon i garaget
                        _ui.SelectSearchFilters(out string type, out regNumber, out Color? color, out brand);
                        IEnumerable<Vehicle> filteredVehicles = _garageHandler.SearchVehicles(type, regNumber, color, brand);
                        _ui.PrintFilteredVehicles(filteredVehicles);
                        break;
                    case '0':
                        Environment.Exit(0); // Avslutar programmet
                        break;
                    default:
                        break;
                }
            } while (true);
        }



        internal bool GarageExists()
        {
            bool garageExists = _garageHandler.CheckIfGarageExists(); 
            if (!garageExists)
            {
                _ui.PrintNoGarageMessage();
                return false;
            }
            return true;
        }

        internal void RunTestMode()
        {
            _garageHandler.CreateGarage(5); // Skapar ett garage med 5 platser för testning
            _garageHandler.ParkVehicle(new Car("ABC123", "Volvo", Color.Red, FuelType.Gasoline));
            _garageHandler.ParkVehicle(new Motorcycle("DEF456", "Yamaha", Color.Blue, 600));
            _garageHandler.ParkVehicle(new Boat("GHI789", "Buster", Color.Green, 5));
            _garageHandler.ParkVehicle(new Bus("JKL012", "Scania", Color.Yellow, true));
            _garageHandler.ParkVehicle(new Airplane("MNO345", "Boeing", Color.White, 150));
            IEnumerable<Vehicle> allVehicles = _garageHandler.ReturnAllVehicles();
            _ui.PrintAllVehicles(allVehicles); // Skriver ut alla fordon i garaget
        }


    }
}
