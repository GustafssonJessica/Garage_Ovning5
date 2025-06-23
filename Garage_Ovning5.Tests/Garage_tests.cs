using Garage_Ovning5.Vehicles;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace Garage_Ovning5.Tests
{
    public class Garage_tests
    {
        // Test för att se att det går att lägga till ett fordon i garaget
        [Fact]
        public void ParkVehicle_AddingANewUnicVehicle_VehicleAdded()
        {
            //Arrange 
            Garage<Vehicle> garage = new Garage<Vehicle>(5);
            Vehicle vehicle = new Car("ABC123", "Ford", Color.Red, FuelType.Diesel);
            //Ask 
            garage.ParkVehicle(vehicle);
            //Assert 
            Assert.Contains(vehicle, garage);
        }

        // Test för att se att enumeratorn fungerar
        [Fact]
        public void GetEnumerator_EnumerateVehicles_ReturnsAllParkedVehicles()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(5);
            garage.Park5Vehicles();
            // Act
            List<Vehicle> parkedVehicles = new List<Vehicle>();
            foreach (var vehicle in garage)
            {
                parkedVehicles.Add(vehicle);
            }
            // Assert
            Assert.Equal(5, parkedVehicles.Count);
        }

        // Test för att se att man kan söka på fordon i garaget
        [Fact]
        public void GetFilteredVehicles_SearchColorGreen_ReturnBoat()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(5);
            garage.Park5Vehicles();
            // Act
            IEnumerable<Vehicle> vehicles = garage.GetFilteredVehicles("", "", Color.Green, "");
            // Assert
            Assert.Contains(vehicles, v => v.Color == Color.Green && v is Boat);
        }

        // Test för att se att inga fordon returneras om ej sökning matchar kriterier
        [Fact]
        public void GetFilteredVehicles_SearchBrandToyota_NoVehiclesReturned()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(5);
            garage.Park5Vehicles();
            // Act
            IEnumerable<Vehicle> vehicles = garage.GetFilteredVehicles("", "", null, "Toyota");
            // Assert
            Assert.DoesNotContain(vehicles, v => v.Brand == "Toyota");
        }

        // Test för att se att metoden GetAllVehicles returnerar alla fordon i garaget
        [Fact]
        public void GetAllVehicles_5ParkedVehicles_Returns5Vehicles()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(10);
            garage.Park5Vehicles();
            // Act
            var result = garage.GetAllVehicles();
            // Assert
            Assert.Equal(5, result.Count());
        }

        // Test för att se att man kan radera ett fordon från garaget
        [Fact]
        public void RemoveVehicle_ValidRegNumber_VehicleRemoved()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(5);
            Vehicle vehicle = new Car("ABC123", "Ford", Color.Red, FuelType.Diesel);
            garage.ParkVehicle(vehicle);
            // Act
            bool result = garage.RemoveVehicle("ABC123");
            // Assert
            Assert.True(result);
            Assert.DoesNotContain(vehicle, garage);
        }


        // Test för att se att fordon ej raderas om registreringsnumret inte finns i garaget
        [Fact]
        public void RemoveVehicle_InvalidRegNumber_VehicleNotRemoved()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(5);
            Vehicle vehicle = new Car("ABC123", "Ford", Color.Red, FuelType.Diesel);
            garage.ParkVehicle(vehicle);
            // Act
            bool result = garage.RemoveVehicle("XYZ789");
            // Assert
            Assert.False(result);
            Assert.Contains(vehicle, garage);
        }
    }
}
