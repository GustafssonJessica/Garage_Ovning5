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
            //Assert - Se om fordonet adderats?
            Assert.Contains(vehicle, garage);
        }

        //Gör en metod där att använda den där metoden med flera parkerade fordon och se om den returnerar rätt antal fordon

        [Fact]
        public void Park5Vehicles_5vehiclesInGarage_Return5Vehicles()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(10);
            // Act
            garage.Park5Vehicles();
            // Assert
            Assert.Equal(5, garage.NumberOfParkedVehicles);
        }

        //en metod för att se vad som händer om garaget är fullt och man lägger till ett till fordon
        // Metod för att se vad som händer om man försöker parkera ett fordon med samma registreringsnummer som ett redan parkerat fordon
        // Om garaget inte är skapat


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
