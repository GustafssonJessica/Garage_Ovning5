using Garage_Ovning5.Vehicles;
using System.Collections.Generic;

namespace Garage_Ovning5.Tests
{
    public class Garage_tests
    {
        [Fact]
        public void ParkVehicle_AddingANewUnicVehicle_VehicleAdded()
        {
            //[MethodName_StateUnderTest_ExpectedBehavior]

            //Public void Sum_NegativeNumberAs1stParam_ExceptionThrown() 

            //Arrange 
            Garage<Vehicle> garage = new Garage<Vehicle>(5);
            Vehicle vehicle = new Car("ABC123", "Ford", Color.Red, FuelType.Diesel);
            //Ask 
            garage.ParkVehicle(vehicle);
            //Assert 
            Assert.Contains(vehicle, garage);
        }

        // Todo ta bort Test för att se att fordon av olika slag kan läggas till i garaget
        [Fact]
        public void Park5Vehicles_5vehiclesInGarage_Returns5Vehicles()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(10);
            // Act
            garage.Park5Vehicles();
            // Assert
            Assert.Equal(5, garage.NumberOfParkedVehicles);
        }

        //en metod för att se vad som händer om garaget är fullt och man lägger till ett till fordon
        // Metod för att se vad som händer om man försöker parkera ett fordon med samma registreringsnummer som ett redan parkerat fordon - NEJ, ligger i GarageHandler nu
        // Om garaget inte är skapat


        // Test för att testa enumeratorn fungerar
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
            Garage<Vehicle> garage = new Garage<Vehicle>(5);
            garage.Park5Vehicles();

            IEnumerable<Vehicle> vehicles = garage.GetFilteredVehicles("", "", Color.Green, "");

            Assert.Contains(vehicles, v => v.Color == Color.Green && v is Boat);
        }

        // Test för att se att inga fordon returneras om ej sökning matchar kriterier
        [Fact]
        public void GetFilteredVehicles_SearchBrandToyota_NoVehiclesReturned()
        {
            Garage<Vehicle> garage = new Garage<Vehicle>(5);
            garage.Park5Vehicles();

            IEnumerable<Vehicle> vehicles = garage.GetFilteredVehicles("", "", null, "Toyota");

            Assert.DoesNotContain(vehicles, v => v.Brand == "Toyota");
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
