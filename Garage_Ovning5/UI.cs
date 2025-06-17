using Garage_Ovning5.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5
{
    internal class UI
    {

        //Huvudmeny
        public char ShowMainMenu()
        {
            string message = "Välkommen till garageapplikationen. Du kommer nu få ett antal alternativ:" +
                             "\n1) Skapa ett nytt garage" +
                             "\n2) Parkera fordon i garaget" +
                             "\n3) Ta bort fordon från garaget"+
                             "\n4) Lista samtliga fordon i garage" +
                             "\n5) Sök efter fordon i garage" +
                             "\n0) Stäng applikationen" +
                             "\n\nAnge ett val (0-5):";

            char choice = ReturnChar(message);
            return choice;
        }

        internal int GetNewGarageInfo()
        {
            //todo Kolla så det inte redan finns ett garage
            string message = "Du har valt att skapa ett nytt garage. Hur många parkeringsplatser ska garaget innehålla?";
            return ReturnInt(message);
        }

        public string ReturnString(string message)
        {
            Console.WriteLine(message);
            do
            {
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)) // ok validera för båda dessa här?
                {
                    Console.WriteLine("Tom inmatning. Var god försök igen");
                }
                else
                {
                    return input;
                }
            } while (true);
        }

        public char ReturnChar(string message)
        {
            do
            {
                string input = ReturnString(message); //jag tror jag kommer bli missnöjd för att ReturnChar och returnInt kommer att skriva ut message varje gång anv gör fel
                if (input.Length > 1)
                {
                    Console.WriteLine("Du har angett mer än ett tecken. Var god försök igen");
                }
                else
                {
                    Char[] charArray = input.ToCharArray();
                    return charArray[0];
                }
            } while (true);
        }

        public int ReturnInt(string message)
        {
            int result;
            bool success;
            do
            {
                string input = ReturnString(message);
                success = int.TryParse(input, out result);
                if (!success)
                {
                    Console.WriteLine("Endast siffror är tillåtna");
                    continue;
                }
                if (result < 0)
                {
                    Console.WriteLine("Negativa tal är ej tillåtna");
                    continue;
                }
                return result;
            } while (true);
        }

        // Metod för att returnera namnet på fordonstypen som användaren väljer
        public string ReturnVehicleType()
        {
            bool correctVehicleNumber = true;
            int typeOfVehicle;
            string nameOfVehicle = string.Empty;
            do
            {
                typeOfVehicle = ReturnInt("Du vill parkera ett nytt fordon. Välj först fordonstyp:\n1) Bil \n2) Båt \n3) Buss \n4) Flygplan \n5) Motorcyckel");
                if (typeOfVehicle > 5)
                {
                    correctVehicleNumber = false;
                    Console.WriteLine("Ogitlig inmatning");
                }
            } while (!correctVehicleNumber);

            switch (typeOfVehicle)
            {
                case 1:
                    nameOfVehicle = "Car";
                    break;
                case 2:
                    nameOfVehicle = "Boat";
                    break;
                case 3:
                    nameOfVehicle = "Bus";
                    break;
                case 4:
                    nameOfVehicle = "Airplane";
                    break;
                case 5:
                    nameOfVehicle = "Motorcycle";
                    break;
                default:
                    break;
            }
            return nameOfVehicle;
        }

        // Metod för att hämta information om ett fordon som ska parkeras
        internal void GetVehicleInfo(out string regNumber, out string brand, out Color vehicleColor)
        {
            regNumber = ReturnString("Registreringsnummer: ");
            brand = ReturnString("Märke: ");

            //todo denna finns både här och i filtrera fordon-metoden, kanske bryta ut?
            Console.WriteLine("Välj färg genom att ange rätt nummer: ");
            foreach (Color color in Enum.GetValues(typeof(Color)))
            {
                Console.WriteLine($"{(int)color} - {color}");
            }

            while (true)
            {
                int colorChoice = ReturnInt("");
                if (Enum.IsDefined(typeof(Color), colorChoice))
                {
                    vehicleColor = (Color)colorChoice;
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltigt färgval. Försök igen.");
                }
            }
        }


        internal void GetCarInfo(out string regNumber, out string brand, out Color vehicleColor, out FuelType fuelType)
        {
            GetVehicleInfo(out regNumber, out brand, out vehicleColor);
            Console.WriteLine("Ange drivmedel genom att välja rätt nummer: ");

            foreach (FuelType fuels in Enum.GetValues(typeof(FuelType)))
            {
                Console.WriteLine($"{(int)fuels} - {fuels}");
            }
            while (true)
            {
                int fuel = ReturnInt("");
                if (Enum.IsDefined(typeof(FuelType), fuel))
                {
                    fuelType = (FuelType)fuel;
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltigt drivmedel. Försök igen.");
                }
            }
        }
        internal void GetBoatInfo(out string regNumber, out string brand, out Color vehicleColor, out int length)
        {
            GetVehicleInfo(out regNumber, out brand, out vehicleColor);

            length = ReturnInt("Ange båtens längd: ");
        }

        internal void GetMotorCycleInfo(out string regNumber, out string brand, out Color vehicleColor, out int cylinderVolume)
        {
            GetVehicleInfo(out regNumber, out brand, out vehicleColor);
            cylinderVolume = ReturnInt("Ange motorcykelns cylindervolym: ");
        }

        internal void GetBusInfo(out string regNumber, out string brand, out Color vehicleColor, out bool hasSeatBelts)
        {
            GetVehicleInfo(out regNumber, out brand, out vehicleColor);
            int seatBelts;
            do
            {
                seatBelts = ReturnInt("Finns säkerhetsbäten på bussen? \n1) Ja \n2) Nej");
                if (seatBelts > 2 || seatBelts < 1)
                {
                    Console.WriteLine("Ogiltig inmatning. Var god försök igen");
                    continue;
                }
                break;
            } while (true);
            if (seatBelts == 1)
            {
                hasSeatBelts = true;
            }
            else
            {
                hasSeatBelts = false;
            }
        }

        internal void GetAirplaneInfo(out string regNumber, out string brand, out Color vehicleColor, out int numberOfSeats)
        {
            GetVehicleInfo(out regNumber, out brand, out vehicleColor);
            numberOfSeats = ReturnInt("Hur många säten har flygplanet? ");
        }

        // Metod för att hämta registreringsnummer på ett fordon som ska tas bort från garaget
        internal string GetDeleteInfo()
        {
            string message = ("Du vill ta bort ett fordon från garaget. Var god mata in fordonets registreringsnummer: ");
            string regNumber = ReturnString(message);
            return regNumber;
        }

        // Metod för att skriva ut alla fordon i garaget
        internal void PrintAllVehicles(IEnumerable<Vehicle> vehicles)
        {
            if (!vehicles.Any())
            {
                Console.WriteLine("Inga fordon finns i garaget.");
                return;
            }

            Console.WriteLine("Parkerade fordon i garaget: ");
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"Fordonstyp: {vehicle.GetType().Name} Registreringsnummer: {vehicle.RegistrationNumber}, Färg: {vehicle.Color}, Märke: {vehicle.Brand}");

            }
        }

        // Metod för att hämta sökkriterier för att filtrera fordon i garaget
        internal void SelectSearchFilters(out string nameOfVehicle, out string regNmr, out Color? color, out string brand)
        {
            //svårt använda returnString när det får vara null/empty

            int type;
            bool correctVehicleNumber = true;
             nameOfVehicle = string.Empty;
            do
            {
                type = ReturnInt("Ange fordonstyp (eller tryck Enter för att hoppa över:):\n1) Bil \n2) Båt \n3) Buss \n4) Flygplan \n5) Motorcyckel");
                if (type > 5)
                {
                    correctVehicleNumber = false;
                    Console.WriteLine("Ogitlig inmatning");
                }
            } while (!correctVehicleNumber);

            switch (type)
            {
                case 1:
                    nameOfVehicle = "Car";
                    break;
                case 2:
                    nameOfVehicle = "Boat";
                    break;
                case 3:
                    nameOfVehicle = "Bus";
                    break;
                case 4:
                    nameOfVehicle = "Airplane";
                    break;
                case 5:
                    nameOfVehicle = "Motorcycle";
                    break;
                default:
                    break;
            }
          
            Console.WriteLine("Ange registreringsnummer (eller tryck Enter för att hoppa över):");
            regNmr = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Ange färg (eller tryck Enter för att hoppa över): "); //todo nått är fel här, får ogiltig inmatning trots korrekt siffra
            foreach (Color colour in Enum.GetValues(typeof(Color)))
            {
                Console.WriteLine($"{(int)colour} - {colour}");
            }

            bool correctValue = false;
            do
            {
                {
                    string input = Console.ReadLine() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        color = null;
                        correctValue = true;

                    }
                    else if (Enum.IsDefined(typeof(Color), input))
                    {
                        int colorChoice = int.Parse(Console.ReadLine());
                        color = (Color)colorChoice;
                        correctValue = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ogiltigt färgval. Försök igen.");
                        color = null;
                    }
                }
            } while (!correctValue);


            Console.WriteLine("Ange märke (eller tryck Enter för att hoppa över):");
            brand = Console.ReadLine() ?? string.Empty;
        }

        internal void PrintFilteredVehicles(IEnumerable<Vehicle> filteredVehicles)
        {
            if (!filteredVehicles.Any())
            {
                Console.WriteLine("Inga fordon finns i garaget.");
                return;
            }
            Console.WriteLine("Fordon som uppfyller dina sökkriterier:");
            foreach (var vehicle in filteredVehicles)
            {
                Console.WriteLine($"Fordonstyp: {vehicle.GetType().Name} Registreringsnummer: {vehicle.RegistrationNumber}, Färg: {vehicle.Color}, Märke: {vehicle.Brand}");
            }
        }

        internal void PrintNoGarageMessage()
        {
            Console.WriteLine("Du måste skapa ett garage innan du kan utföra denna åtgärd.");
        }
    }
}

