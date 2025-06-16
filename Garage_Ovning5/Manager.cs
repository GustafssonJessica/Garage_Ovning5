using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Ovning5
{
    //detta är som Game.CS
    internal class Manager
    {
        private UI _ui; //gör om dessa till interface
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
               char choice = _ui.ShowMainMenu(); //kör huvudmenyn

                switch (choice)
                {
                    case '1':
                        int GarageSize = _ui.GetNewGarageInfo();
                        _garageHandler.CreateGarage(GarageSize);
                        break;
                    case '2':
                        break;
                    case '3':
                        break; 
                    case '4':
                        break; 
                    case '0':
                        break; 
                    default:
                        break; 
                }
            } while (true);
        }

        //Frågar UI om input/skickar nått till UI som ska skrivas ut

        //Har ej tillgång till garaget. Kommunicerar via Handler
    }
}
