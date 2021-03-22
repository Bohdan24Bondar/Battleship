
using __Battleship;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipLibrary;

namespace __Battleship
{
    class Program
    {
        public const string PATH_TO_FOLDER = "..\\LastSaving";
        public const string PATH_TO_FILE = PATH_TO_FOLDER + "\\Battleship.bin";

        static void Main(string[] args)
        {
            Console.Title = "BATTLESHIP";
            Console.OutputEncoding = Encoding.Unicode;
            bool isEasyLevel = false;
            Intelligence enemysMind = null;
            ConsoleGame currentGame;

            UserActions numberUserAction = (UserActions)UserInterface.ShowUserActions();
            bool isNewGame = UserInterface.IsNewGame();

            if (isNewGame)
            {
                Level levelNumber = UserInterface.InputDifficultyLevel();

                switch (levelNumber)
                {
                    case Level.Easy:
                        isEasyLevel = true;
                        break;
                    case Level.Meduim:
                        enemysMind = new Intelligence(-1, -1);
                        break;
                    case Level.Hard:
                        enemysMind = new AdvancedIntelligence(-1, -1);
                        break;
                    default:
                        break;
                }

                MethodShipsBuild introducedMethod = UserInterface.EnterShipsBuildingMethod();

                currentGame = new ConsoleGame(numberUserAction, introducedMethod, enemysMind, isEasyLevel);
            }
            else
            {
                using (PlayerMapSerializer creator = new PlayerMapSerializer(PATH_TO_FOLDER, PATH_TO_FILE))
                {
                    currentGame = creator.ReadPlayerMap() as ConsoleGame;

                    if (currentGame != null)
                    {
                        currentGame.NumberUserAction = numberUserAction;
                        currentGame.IsNewGame = false;
                    }
                }
            }

            currentGame.Run();
            Console.ReadKey();
        }
    }
}
