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
        public void ShowMainMenu()
        {
            string message = "Välkommen till garageapplikationen. Du kommer nu få ett antal alternativ: \n1) Skapa ett nytt garage " +
                "\n2) Parkera eller ta bort fordon från garage \n3) Lista samtliga fordon i garage \n4) Sök efter fordon i garage \n5) Stäng applikationen"; //{garageNamn}lägg till namn på garage?
                                                                                                                                                             // char input = ReturnChar(message); 
            char input = '1';


            switch (input)
            {

                case '1':
                    NewGarage();
                    break;

                case '2':
                    Undermeny();
                    break;
                case '3':
                    ShowAllVehicles();
                    break;
                case '4':
                    SearchForVehicle();
                    break;
                case '0':
                    Environment.Exit();
                    break;
                default:
                    Console.WriteLine("Felaktig inmatning");
                    break;
            }

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

            do
            {
                string input = ReturnString(message);
                bool success = int.TryParse(input, out int result);
                if (!success)
                {
                    Console.WriteLine("Endast siffror är tillåtna");
                }
                else
                {
                    return result;
                }
            } while (true);
        }
    }
}
