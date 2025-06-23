using Garage_Ovning5.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5
{
    public class UI : IUI
    {

        //Huvudmeny
        public char ShowMainMenu()
        {
            string message = "Du kommer nu få ett antal alternativ:" +
                             "\n1) Parkera fordon i garaget" +
                             "\n2) Ta bort fordon från garaget"+
                             "\n3) Lista samtliga fordon i garagen " +
                             "\n4) Lista hur många av de olika fordonstyperna som står i garaget" +
                             "\n5) Sök efter fordon i garage" +
                             "\n0) Stäng applikationen" +
                             "\n\nAnge ett val (0-5):";

            char choice = ReturnChar(message);
            return choice;
        }

        public int GetNewGarageInfo()
        {
            string message = "Välkommen till denna garageapplikation. Du behöver skapa ett garage för att gå vidare. \nHur många parkeringsplatser ska garaget innehålla?";
            return ReturnInt(message);
        }

        // Metod för att returnera en sträng från användaren som inte är null/tom eller endast innehåller blanksteg
        public string ReturnString(string message)
        {
            Console.WriteLine(message);
            do
            {
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Tom inmatning. Var god försök igen");
                }
                else
                {
                    return input;
                }
            } while (true);
        }

        // Metod för att returnera en char från användaren, validerar att det endast är ett tecken 
        public char ReturnChar(string message)
        {
            do
            {
                string input = ReturnString(message);
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

        // Metod för att returnera ett heltal från användaren, validerar att det är ett positivt heltal
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
        public void GetVehicleInfo(out string regNumber, out string brand, out Color vehicleColor)
        {
            // Validerar att registrenringsnumret är i rätt format (AAA111)
            bool validRegNumber = true;
            do
            {
                regNumber = ReturnString("Ange Registreringsnummer, format: XXX111 (OBS måste vara unikt för att fordonet ska skapas) "); //TOdo visa vilka regNUmmer som redan finns
                if (regNumber.Length != 6)
                {
                    Console.WriteLine("Felaktigt antal tecken. Det måste vara 6 tecken");
                    validRegNumber = false;
                }
                else if (!char.IsLetter(regNumber[0]) || !char.IsLetter(regNumber[1]) || !char.IsLetter(regNumber[2]) || !char.IsDigit(regNumber[3]) || !char.IsDigit(regNumber[4]) || !char.IsDigit(regNumber[5]))
                {
                    Console.WriteLine("Ogiltigt registreringsnummer. Första tre tecken måste vara bokstäver och sista tre tecken måste vara siffror.");
                    validRegNumber = false;
                }
                else
                {
                    validRegNumber = true;
                }
            } while (!validRegNumber);

            brand = ReturnString("Märke: ");

            Console.WriteLine("Välj färg genom att ange rätt nummer: ");
            foreach (Color color in Enum.GetValues(typeof(Color)))
            {
                Console.WriteLine($"{(int)color + 1} - {color}");
            }

            while (true)
            {
                int colorChoice = ReturnInt("");
                colorChoice--;
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

        // Metod för att hämta information om bil
        public void GetCarInfo(out string regNumber, out string brand, out Color vehicleColor, out FuelType fuelType)
        {
            GetVehicleInfo(out regNumber, out brand, out vehicleColor);
            Console.WriteLine("Ange drivmedel genom att välja rätt nummer: ");

            foreach (FuelType fuels in Enum.GetValues(typeof(FuelType)))
            {
                Console.WriteLine($"{(int)fuels + 1} - {fuels}");
            }
            while (true)
            {
                int fuel = ReturnInt("");
                fuel--;
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

        // Metod för att hämta information om båt

        public void GetBoatInfo(out string regNumber, out string brand, out Color vehicleColor, out int length)
        {
            GetVehicleInfo(out regNumber, out brand, out vehicleColor);

            length = ReturnInt("Ange båtens längd: ");
        }

        // Metod för att hämta information om motorcykel
        public void GetMotorCycleInfo(out string regNumber, out string brand, out Color vehicleColor, out int cylinderVolume)
        {
            GetVehicleInfo(out regNumber, out brand, out vehicleColor);
            cylinderVolume = ReturnInt("Ange motorcykelns cylindervolym: ");
        }

        // Metod för att hämta information om buss
        public void GetBusInfo(out string regNumber, out string brand, out Color vehicleColor, out bool hasSeatBelts)
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

        // Metod för att hämta information om flygplan
        public void GetAirplaneInfo(out string regNumber, out string brand, out Color vehicleColor, out int numberOfSeats)
        {
            GetVehicleInfo(out regNumber, out brand, out vehicleColor);
            numberOfSeats = ReturnInt("Hur många säten har flygplanet? ");
        }

        // Metod för att hämta registreringsnummer på ett fordon som ska tas bort från garaget
        public string GetDeleteInfo()
        {
            string message = ("Du vill ta bort ett fordon från garaget. Var god mata in fordonets registreringsnummer: ");
            string regNumber = ReturnString(message);
            return regNumber;
        }

        // Metod för att skriva ut alla fordon i garaget
        public void PrintAllVehicles(IEnumerable<Vehicle> vehicles)
        {
            if (!vehicles.Any())
            {
                Console.WriteLine("Inga fordon finns i garaget.");
                return;
            }

            Console.WriteLine("Parkerade fordon i garaget: ");
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"Fordonstyp: {vehicle.GetType().Name} Registreringsnummer: {vehicle.RegistrationNumber}, Färg: {vehicle.Color}, Märke på fordonet: {vehicle.Brand}");

            }
        }

        // Metod för att hämta sökkriterier för att filtrera fordon i garaget
        public void SelectSearchFilters(out string nameOfVehicle, out string regNmr, out Color? color, out string brand)
        {
            int type;
            bool correctVehicleNumber = true;
            nameOfVehicle = string.Empty;
            do
            {
                string message = ("Ange fordonstyp (eller tryck Enter för att hoppa över:):\n1) Bil \n2) Båt \n3) Buss \n4) Flygplan \n5) Motorcyckel");
                Console.WriteLine(message);
                string input = Console.ReadLine() ?? string.Empty;
                bool isANumber = int.TryParse(input, out type);
                if (string.IsNullOrEmpty(input))
                {
                    nameOfVehicle = string.Empty;
                }
                else if (!isANumber || type > 5 || type < 1)
                {
                    Console.WriteLine("Ej giltig input, försök igen!");
                    correctVehicleNumber = false;
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

            Console.WriteLine("Ange färg (eller tryck Enter för att hoppa över): ");
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
                    else
                    {
                        if (int.TryParse(input, out int colorChoice) && Enum.IsDefined(typeof(Color), colorChoice))
                        {
                            color = (Color)colorChoice;
                            correctValue = true;
                        }
                        else
                        {
                            Console.WriteLine("Ogiltigt färgval. Försök igen.");
                            color = null;
                        }
                    }
                }
            } while (!correctValue);


            Console.WriteLine("Ange märke (eller tryck Enter för att hoppa över):");
            brand = Console.ReadLine() ?? string.Empty;
        }

        // Metod för att skriva ut filtrerade fordon baserat på sökkriterier
        public void PrintFilteredVehicles(IEnumerable<Vehicle> filteredVehicles)
        {
            if (!filteredVehicles.Any())
            {
                Console.WriteLine("Inga fordon uppfyller dina sökkriterier.");
                return;
            }

            Console.WriteLine("Fordon som uppfyller dina sökkriterier:");
            foreach (var vehicle in filteredVehicles)
            {
                Console.WriteLine($"Fordonstyp: {vehicle.GetType().Name} Registreringsnummer: {vehicle.RegistrationNumber}, Färg: {vehicle.Color}, Märke: {vehicle.Brand}");
            }
        }

        // Metod för att visa fordonstyper och antal i garaget
        public void ShowVehicleTypes(Dictionary<string, int> typesAndNumbers)
        {

            if (typesAndNumbers.Count == 0)
            {
                Console.WriteLine("Det finns inga fordon i garaget just nu!");
                return;
            }

            Console.WriteLine("Fordon i garaget: ");
            foreach (var key in typesAndNumbers)
            {
                Console.WriteLine($"Fordonstyp: {key.Key} - Antal: {key.Value}");
            }
        }


        // Metoder för att skriva ut olika meddelanden till användaren

        public void PrintNoGarageMessage()
        {
            Console.WriteLine("Du måste skapa ett garage innan du kan utföra denna åtgärd.");
        }

        public void PrintVehicleParkedMessage()
        {
            Console.WriteLine($"Fordonet är nu parkerad i garaget!");
        }

        public void PrintGarageFullMessage()
        {
            Console.WriteLine($"Ett nytt fordon kan inte parkeras just nu, garaget är fullt");
        }

        public void PrintGarageCreatedMessage(int garageSize, bool garageCreated)
        {
            if (garageCreated)
            {
                Console.WriteLine($"Ett nytt garage med {garageSize} parkeringsplatser har skapats!\n");
                return;
            }
            Console.WriteLine("Garaget kunde tyvärr inte skapas. Var god försök igen");
        }

        public void PrintVehicleRemovedMessage(bool vehicleRemoved, string regNumber)
        {
            if (vehicleRemoved)
            {
                Console.WriteLine($"Fordonet med registreringsnummer {regNumber} har tagits bort från garaget.");
            }
            else
            {
                Console.WriteLine($"Kunde inte ta bort fordonet med registreringsnummer {regNumber}. \nDet finns inget fordon med det registreringsnumret i garaget.");
            }
        }

        public void PrintNotUnicRegNmr()
        {
            Console.WriteLine("Fordonet kunde inte parkeras, det finns redan ett fordon med detta registreringsnummer i garaget");
        }

        public void InvalidInputMessage()
        {
            Console.WriteLine("Ogiltig inmatning. Tryck på Enter för att komma tillbaka till huvudmenyn");
        }

        public void ReturnToMainMenuMessage()
        {
            Console.WriteLine("\nTryck Enter för att komma tillbaka till huvudmenyn");
        }
    }
}

