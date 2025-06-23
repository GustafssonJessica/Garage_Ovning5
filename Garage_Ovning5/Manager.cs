using Garage_Ovning5.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5
{
    public class Manager
    {
        private IUI _ui;
        private IGarageHandler _garageHandler;

        public Manager(IUI ui, IGarageHandler garageHandler)
        {
            _ui = ui;
            _garageHandler = garageHandler;
        }

        internal void Run()
        {
            //Skapa ett nytt garage
            int garageSize = _ui.GetNewGarageInfo();
            bool garageCreated = _garageHandler.CreateGarage(garageSize);
            _ui.PrintGarageCreatedMessage(garageSize, garageCreated);
            do
            {
                //Visa huvudmenyn och returnerar användarens val
                char choice = _ui.ShowMainMenu();

                //Switch-sats för att hantera användarens val
                switch (choice)
                {
                    case '1':
                        //Parkera ett fordon
                        bool garageIsFull = _garageHandler.CheckIfGarageIsFull();
                        if (garageIsFull)
                        {
                            _ui.PrintGarageFullMessage();
                            continue; 
                        }

                        string typeOfVehicle = _ui.ReturnVehicleType(); 
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
                                break;
                            case "Motorcycle":
                                int cylinderVolume;
                                _ui.GetMotorCycleInfo(out regNumber, out brand, out vehicleColor, out cylinderVolume);
                                vehicle = new Motorcycle(regNumber, brand, vehicleColor, cylinderVolume);
                                break;
                            case "Boat":
                                int length;
                                _ui.GetBoatInfo(out regNumber, out brand, out vehicleColor, out length);
                                vehicle = new Boat(regNumber, brand, vehicleColor, length);
                                break;
                            case "Bus":
                                bool hasSeatBelts;
                                _ui.GetBusInfo(out regNumber, out brand, out vehicleColor, out hasSeatBelts);
                                vehicle = new Bus(regNumber, brand, vehicleColor, hasSeatBelts);
                                break;
                            case "Airplane":
                                int numberOfSeats;
                                _ui.GetAirplaneInfo(out regNumber, out brand, out vehicleColor, out numberOfSeats);
                                vehicle = new Airplane(regNumber, brand, vehicleColor, numberOfSeats);
                                break;
                            default:
                                break;
                        }
                        if (vehicle != null)
                        {
                            bool succees = _garageHandler.ParkVehicle(vehicle);
                            if (succees)
                                _ui.PrintVehicleParkedMessage();
                            else
                                _ui.PrintNotUnicRegNmr();
                        }
                        break;

                    case '2':
                        // Radera ett fordon
                        regNumber = _ui.GetDeleteInfo();
                        bool vehicleRemoved = _garageHandler.DeleteVehicle(regNumber);
                        _ui.PrintVehicleRemovedMessage(vehicleRemoved, regNumber);
                        break;

                    case '3':
                        // Visa alla fordon i garaget
                        IEnumerable<Vehicle> vehicles = _garageHandler.ReturnAllVehicles();
                        _ui.PrintAllVehicles(vehicles);
                        break;

                    case '4':
                        // Lista fordonstyper och hur många av varje som står i garaget
                        var typesAndNumbers = _garageHandler.GetVehicleTypes();
                        _ui.ShowVehicleTypes(typesAndNumbers);
                        break;

                    case '5':
                        // Sök efter fordon i garaget
                        _ui.SelectSearchFilters(out string type, out regNumber, out Color? color, out brand);
                        IEnumerable<Vehicle> filteredVehicles = _garageHandler.SearchVehicles(type, regNumber, color, brand);
                        _ui.PrintFilteredVehicles(filteredVehicles);
                        break;

                    case '0':
                        // Avsluta programmet
                        Environment.Exit(0);
                        break;

                    default:
                        // Visa meddelande om ogiltig input
                        _ui.InvalidInputMessage(); 
                        break;
                }
                
                _ui.ReturnToMainMenuMessage();
                Console.ReadLine(); // Väntar på att användaren trycker på Enter innan nästa iteration
                Console.Clear(); // Rensar konsolen för att visa huvudmenyn igen

            } while (true);
        }
    }
}

