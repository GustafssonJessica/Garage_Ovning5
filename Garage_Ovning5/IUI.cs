﻿using Garage_Ovning5.Vehicles;

namespace Garage_Ovning5
{
    public interface IUI
    {
        void GetAirplaneInfo(out string regNumber, out string brand, out Color vehicleColor, out int numberOfSeats);
        void GetBoatInfo(out string regNumber, out string brand, out Color vehicleColor, out int length);
        void GetBusInfo(out string regNumber, out string brand, out Color vehicleColor, out bool hasSeatBelts);
        void GetCarInfo(out string regNumber, out string brand, out Color vehicleColor, out FuelType fuelType);
        string GetDeleteInfo();
        void GetMotorCycleInfo(out string regNumber, out string brand, out Color vehicleColor, out int cylinderVolume);
        int GetNewGarageInfo();
        void GetVehicleInfo(out string regNumber, out string brand, out Color vehicleColor);
        void InvalidInputMessage();
        void PrintAllVehicles(IEnumerable<Vehicle> vehicles);
        void PrintDefaultGarageCreatedMessage();
        void PrintFilteredVehicles(IEnumerable<Vehicle> filteredVehicles);
        void PrintGarageCreatedMessage(int garageSize, bool garageCreated);
        void PrintGarageFullMessage();
        void PrintNoGarageMessage();
        void PrintNotUnicRegNmr();
        void PrintVehicleParkedMessage();
        void PrintVehicleRemovedMessage(bool vehicleRemoved, string regNumber);
        int PrintWelcomeMessage();
        char ReturnChar(string message);
        int ReturnInt(string message);
        string ReturnString(string message);
        void ReturnToMainMenuMessage();
        string ReturnVehicleType();
        void SelectSearchFilters(out string nameOfVehicle, out string regNmr, out Color? color, out string brand);
        char ShowMainMenu();
        void ShowVehicleTypes(Dictionary<string, int> typesAndNumbers);
    }
}