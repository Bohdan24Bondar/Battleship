
using __Battleship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BattleshipLibrary;

namespace __Battleship
{
    class UserInterface
    {
        #region Constants

        public const int MAP_SIZE = 10;
        public const int TOP_BOARDER = 3;
        public const int DOWN_BOARDER = 13;
        public const int LEFT_BOARDER = 3;
        public const int RIGTH_BOARDER = 13;
        public const int MIN_COORDS_LENGTH = 2;
        public const int MAX_COORDS_LENGTH = 3;
        public const int LENGTH_OF_HORIZONTAL_LINE = 19;
        public const int LENGTH_OF_VERTICAL_LINE = 11;
        public const int DISTANCE_BETWEEN_MAP = 50;
        public const int START_LEFT_CURSOR = 4;
        public const int START_TOP_CURSOR = 3;
        public const int BORDER_MAP_CLEAR = 50;
        public const int MESSAGE_START_POSITION = 14;
        public const int MESSAGE_FINISH_POSITION = 16;
        public const int DIFERENCE_HORISONTAL_SYMBOL = 2;
        public const int START_CURSOR_FOR_MESSAGE = 15;
        public const int NICKNAME_WINDOW_WIDTH = 90;
        public const int NICKNAME_WINDOW_HEIGTH = 25;
        public const int GAME_LOGO_WINDOW_WIDTH = 105;
        public const int GAME_LOGO_WINDOW_HEIGTH = 25;
        public const int WIDTH_LOGO_TABLE = 100;
        public const int HEIGTH_LOGO_TABLE = 20;
        public const int LEFT_LOGO_CURSOR = 2;
        public const string TARGET_MESSAGE = "target";
        public const string SHIP_POSITION_MESSAGE = "ship";

        #endregion

        public static Position AreRigthCoords(string message = TARGET_MESSAGE, int startCursorTop = START_CURSOR_FOR_MESSAGE)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey keyReal = Console.ReadKey().Key;

                if (keyReal == ConsoleKey.Escape)
                {
                    return new Position(-1, -1);
                }
            }


            bool isNumber = false;
            bool isLetter = false;
            char letter;
            bool isRightNumber = false;
            bool isRightLetter = false;

            int coordX = -1;
            int coordY = -1;

            do
            {
                ClearBottom(MESSAGE_START_POSITION + 1);

                Console.SetCursorPosition(Console.CursorLeft = 0, 
                        Console.CursorTop = startCursorTop);
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine($"Input coordinates of {message}:");

                string coords = Console.ReadLine();

                string numberCoord = string.Empty;
                string lettersCoord = string.Empty;

                if (coords.Length < MIN_COORDS_LENGTH 
                        || coords.Length > MAX_COORDS_LENGTH)
                {
                    continue; 
                }

                if (coords.Length == MAX_COORDS_LENGTH)
                {
                    numberCoord = coords[0].ToString();
                    numberCoord += coords[1].ToString();
                    lettersCoord = coords[MAX_COORDS_LENGTH - 1].ToString();
                }
                else
                {
                    numberCoord = coords[0].ToString();
                    lettersCoord = coords[1].ToString();
                }
                
                lettersCoord = lettersCoord.ToUpper(); 
                isNumber = int.TryParse(numberCoord, out coordY );
                coordY--;

                if ((coordY >= 0) && (coordY < MAP_SIZE)) 
                {
                    isRightNumber = true;
                }

                isLetter = char.TryParse(lettersCoord, out letter);

                if (isLetter)
                {
                    coordX = CompereLetters(letter); 

                    if ((coordX >= 0) && (coordX < MAP_SIZE))
                    {
                        isRightLetter = true;
                    }
                }

            } while (!isNumber ||!isLetter ||!isRightLetter || !isRightNumber);

            return new Position(coordY, coordX);
        }

        public static void ClearBottom(int startCursorTop = MESSAGE_START_POSITION, 
                int startCursorLeft = BORDER_MAP_CLEAR, int maxTopPosition = MESSAGE_FINISH_POSITION)
        {
            for (int i = startCursorTop; i <= maxTopPosition; i++)
            {
                for (int j = 0; j < startCursorLeft; j++)
                {
                    Console.SetCursorPosition(Console.CursorLeft = j, 
                            Console.CursorTop = i);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                }
            }
        }

        public static string InputNickName() 
        {
            ConsoleColor color = ConsoleColor.Green;
            Console.WindowWidth = NICKNAME_WINDOW_WIDTH;
            Console.WindowHeight = NICKNAME_WINDOW_HEIGTH;

            string message = "PLEASE INPUT YOUR NICKNAME:";
            int cursorLeft = 0;
            int cursorTop = 0;

            ClearBottom();
            ShowMessage(message, cursorLeft, cursorTop, color);

            string nickname = Console.ReadLine();
            nickname = nickname.ToUpper();

            Console.Clear();

            return nickname;
        }

        public static int CompereLetters(char letter)
        {
            int coordX;

            switch (letter)
            {
                case 'A':
                    coordX = (int)Letters.A;
                    break;
                case 'B':
                    coordX = (int)Letters.B;
                    break;
                case 'C':
                    coordX = (int)Letters.C;
                    break;
                case 'D':
                    coordX = (int)Letters.D;
                    break;
                case 'E':
                    coordX = (int)Letters.E;
                    break;
                case 'F':
                    coordX = (int)Letters.F;
                    break;
                case 'G':
                    coordX = (int)Letters.G;
                    break;
                case 'H':
                    coordX = (int)Letters.H;
                    break;
                case 'I':
                    coordX = (int)Letters.I;
                    break;
                case 'J':
                    coordX = (int)Letters.J;
                    break;
                default:
                    coordX = -1;
                    break;
            }

            return coordX;
        }

        public static void PrintMap(Sea map, int cursorLeft, int cursorTop)
        {
            for (int i = 0; i < map.GetLengthMapCells(0); i++)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop);

                for (int j = 0; j < map.GetLengthMapCells(1); j++)
                {
                    switch (map[i, j])
                    {
                        case MapCondition.MissedShot:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write($"{(char)Symbol.MissedShot} ");
                            break;
                        case MapCondition.NoneShot:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("- ");
                            break;
                        case MapCondition.ShipSafe:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"{(char)Symbol.ShipSafe}");
                            break;
                        case MapCondition.ShipInjured:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write($"{(char)Symbol.ShipInjured} ");
                            break;
                        case MapCondition.ShipDestroyed:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"{(char)Symbol.DestroyedShip} ");
                            break;
                        default:
                            break;
                    }
                }
                cursorTop++;
                Console.WriteLine();
            }

            Console.CursorTop = 0;
        }

        public static void PrintShipPlayer(Sea map, int cursorLeft, int cursorTop) 
        {
            for (int i = 0; i < map.GetLengthMapCells(0); i++)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop);

                for (int j = 0; j < map.GetLengthMapCells(1); j++)
                {
                    switch (map[i,j])
                    {
                        case MapCondition.MissedShot:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write($"{(char)Symbol.MissedShot} ");
                            break;
                        case MapCondition.NoneShot:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"- ");
                            break;
                        case MapCondition.ShipSafe:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"{(char)Symbol.ShipSafe}");
                            break;
                        case MapCondition.ShipInjured:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write($"{(char)Symbol.ShipInjured} ");
                            break;
                        case MapCondition.ShipDestroyed:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"{(char)Symbol.DestroyedShip} ");
                            break;
                        default:
                            break;
                    }
                }
                cursorTop++;
            }
        }

        public static void PrintShipEnemy(Sea map, int cursorLeft, int cursorTop)
        {
            for (int i = 0; i < map.GetLengthMapCells(0); i++)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop);

                for (int j = 0; j < map.GetLengthMapCells(1); j++)
                {
                    switch (map[i, j])
                    {
                        case MapCondition.MissedShot:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write($"{(char)Symbol.MissedShot} ");
                            break;
                        case MapCondition.NoneShot:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"- ");
                            break;
                        case MapCondition.ShipSafe:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"- ");
                            break;
                        case MapCondition.ShipInjured:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write($"{(char)Symbol.ShipInjured} ");
                            break;
                        case MapCondition.ShipDestroyed:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"{(char)Symbol.DestroyedShip} ");
                            break;
                        default:
                            break;
                    }
                }
                cursorTop++;
            }
        }

        public static void AreSimilarCoords(bool wasShot)
        {
            if (wasShot)
            {
                ClearBottom();
                Console.SetCursorPosition(START_LEFT_CURSOR, MESSAGE_START_POSITION);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You shot this coordinates!" +
                    "Try again!");
            }
        }

        public static void ShowMessage(string message, int cursorLeft = MAP_SIZE, 
                int cursorTop = MESSAGE_START_POSITION, ConsoleColor color = ConsoleColor.Yellow)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(Console.CursorLeft = cursorLeft, Console.CursorTop = cursorTop);
            Console.WriteLine(message);
        }

        public static void PrintMapBorders(int cursorLeft, int cursorTop, 
                bool enemyTitle, string nickname)
        {
            Console.SetCursorPosition(cursorLeft + MAP_SIZE - START_LEFT_CURSOR , cursorTop);

            if (enemyTitle)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("ENEMY");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(nickname);
            }

            Console.ResetColor();
            Console.SetCursorPosition(cursorLeft, cursorTop += 1); 

            for (int i = 0; i < MAP_SIZE; i++)
            {
                Console.Write($"{(Letters)i} ");
            }
            cursorLeft -= LEFT_BOARDER; 
            cursorTop += (TOP_BOARDER - 1); 

            for (int i = 1; i <= MAP_SIZE; i++)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop++);
                Console.Write(i);
            }
        }

        public static void PrintTableEnemy(int leftCursor, int topCursor)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(leftCursor, topCursor);
            Console.Write((char)Borders.LeftTop); 
            Console.Write(new string((char)Borders.Horizontal, 
                    LENGTH_OF_HORIZONTAL_LINE + 1)); 
            Console.Write((char)Borders.RightTop);

            topCursor = (Console.CursorTop + 1);
            leftCursor = (Console.CursorLeft - 1);

            for (int i = 0; i < LENGTH_OF_VERTICAL_LINE - 1; i++) 
            {
                Console.SetCursorPosition(leftCursor, topCursor);
                Console.Write((char)Borders.Vertical);
                topCursor++;
            }

            topCursor = Console.CursorTop + 1;
            leftCursor = Console.CursorLeft - 1;

            Console.SetCursorPosition(leftCursor, topCursor);
            Console.Write((char)Borders.RightDown);

            topCursor = Console.CursorTop;
            leftCursor = LEFT_BOARDER; 

            Console.SetCursorPosition(leftCursor, topCursor);
            Console.Write(new string((char)Borders.Horizontal, 
                    LENGTH_OF_HORIZONTAL_LINE + DIFERENCE_HORISONTAL_SYMBOL));

            topCursor = Console.CursorTop;
            leftCursor = LEFT_BOARDER;

            Console.SetCursorPosition(leftCursor, topCursor);
            Console.Write((char)Borders.LeftDown);

            topCursor = (Console.CursorTop - 1);
            leftCursor = LEFT_BOARDER;

            for (int i = 0; i < LENGTH_OF_VERTICAL_LINE - 1; i++) 
            {
                Console.SetCursorPosition(leftCursor, topCursor);
                Console.Write((char)Borders.Vertical);
                topCursor--;
            }
        }

        public static void PrintTablePlayer(int leftCursor, int topCursor)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(leftCursor, topCursor);
            Console.Write((char)Borders.LeftTop);
            Console.Write(new string((char)Borders.Horizontal, 
                    LENGTH_OF_HORIZONTAL_LINE + 1)); 
            Console.Write((char)Borders.RightTop);

            topCursor = (Console.CursorTop + 1);
            leftCursor = (Console.CursorLeft - 1);

            for (int i = 0; i < LENGTH_OF_VERTICAL_LINE - 1; i++) 
            {
                Console.SetCursorPosition(leftCursor, topCursor);
                Console.Write((char)Borders.Vertical);
                topCursor++;
            }

            topCursor = Console.CursorTop + 1;
            leftCursor = Console.CursorLeft - 1;

            Console.SetCursorPosition(leftCursor, topCursor);
            Console.Write((char)Borders.RightDown);

            topCursor = Console.CursorTop;
            leftCursor = DISTANCE_BETWEEN_MAP - 1; 

            Console.SetCursorPosition(leftCursor, topCursor);
            Console.Write(new string((char)Borders.Horizontal, 
                    LENGTH_OF_HORIZONTAL_LINE + (TOP_BOARDER - 1)));

            topCursor = Console.CursorTop;
            leftCursor = DISTANCE_BETWEEN_MAP - 1 ; 

            Console.SetCursorPosition(leftCursor, topCursor);
            Console.Write((char)Borders.LeftDown);

            topCursor = (Console.CursorTop - 1);
            leftCursor = DISTANCE_BETWEEN_MAP - 1;

            for (int i = 0; i < LENGTH_OF_VERTICAL_LINE - 1; i++) 
            {
                Console.SetCursorPosition(leftCursor, topCursor);
                Console.Write((char)Borders.Vertical);
                topCursor--;
            }
        }

        public static void PrintStartPosition(Sea enemyMap, Sea playerMap)
        {
            int cursorLeft = START_LEFT_CURSOR; 
            int cursorTop = 0; 
            bool enemyTitle = true;
            string nickname = UserInterface.InputNickName(); 

            PrintMapBorders(cursorLeft, cursorTop, enemyTitle, nickname);

            cursorLeft = START_LEFT_CURSOR - 1;
            cursorTop = START_TOP_CURSOR - 1; ;  

            PrintTableEnemy(cursorLeft, cursorTop);

            cursorLeft = START_LEFT_CURSOR; 
            cursorTop = START_TOP_CURSOR;

            PrintShipEnemy(enemyMap, cursorLeft, cursorTop);

            enemyTitle = false;
            cursorTop = 0;
            cursorLeft = DISTANCE_BETWEEN_MAP;

            PrintMapBorders(cursorLeft, cursorTop, enemyTitle, nickname);

            cursorLeft = DISTANCE_BETWEEN_MAP - 1;
            cursorTop = START_TOP_CURSOR - 1; 

            PrintTablePlayer(cursorLeft, cursorTop);

            cursorLeft = DISTANCE_BETWEEN_MAP; 
            cursorTop = START_TOP_CURSOR;

            PrintMap(playerMap, cursorLeft, cursorTop);
        }

        public static void PrintManualInputShips(Sea playerMap)
        {
            int cursorLeft = DISTANCE_BETWEEN_MAP;
            int cursorTop = 0;
            bool enemyTitle = false;

            PrintMapBorders(cursorLeft, cursorTop, enemyTitle, "");

            cursorLeft = DISTANCE_BETWEEN_MAP - 1;
            cursorTop = START_TOP_CURSOR - 1;

            PrintTablePlayer(cursorLeft, cursorTop);

            cursorLeft = DISTANCE_BETWEEN_MAP;
            cursorTop = START_TOP_CURSOR;

            PrintMap(playerMap, cursorLeft, cursorTop);
        }

        public static void PrinPlayerWin(bool isPlayerWinner)
        {
            Console.Clear();
            Console.SetCursorPosition(Console.CursorLeft = 0, Console.CursorTop = 0);
            if (isPlayerWinner)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("YOU WIN!!!!!!!!!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("YOU LOSE!!!!!!!!!");
            }
        }

        public static int ShowUserActions()
        {
            PrintStartMenu();

            int numberAction;
            bool isRigthAction;

            do
            {
                PrintLogoOfGame();

                string userAction = Console.ReadLine();
                isRigthAction = int.TryParse(userAction, out numberAction);

            } while (numberAction < 0 || numberAction > 1 || !isRigthAction);

            Console.Clear();

            return numberAction;
        }

        public static void PrintStartMenu()
        {
            int leftCursor = 0;
            int topCursor = 0;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(leftCursor, topCursor);
            Console.Write((char)Borders.LeftTop);
            Console.Write(new string((char)Borders.Horizontal, WIDTH_LOGO_TABLE));
            Console.Write((char)Borders.RightTop);
            topCursor = (Console.CursorTop + 1);
            leftCursor = (Console.CursorLeft - 1);

            for (int i = 0; i < HEIGTH_LOGO_TABLE; i++)
            {
                Console.SetCursorPosition(leftCursor, topCursor);
                Console.Write((char)Borders.Vertical);
                topCursor++;
            }

            topCursor = Console.CursorTop + 1;
            leftCursor = Console.CursorLeft - 1;

            Console.SetCursorPosition(leftCursor, topCursor);
            Console.Write((char)Borders.RightDown);

            topCursor = Console.CursorTop;
            leftCursor = 1;

            Console.SetCursorPosition(leftCursor, topCursor);
            Console.Write(new string((char)Borders.Horizontal, WIDTH_LOGO_TABLE)); 

            topCursor = Console.CursorTop;
            leftCursor = 0;

            Console.SetCursorPosition(leftCursor, topCursor);
            Console.Write((char)Borders.LeftDown);

            topCursor = (Console.CursorTop - 1);
            leftCursor = 0;

            for (int i = 0; i < HEIGTH_LOGO_TABLE; i++) 
            {
                Console.SetCursorPosition(leftCursor, topCursor);
                Console.Write((char)Borders.Vertical);
                topCursor--;
            }
        }

        public static void PrintLogoOfGame()
        {

            Console.WindowWidth = GAME_LOGO_WINDOW_WIDTH;
            Console.WindowHeight = GAME_LOGO_WINDOW_HEIGTH;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" ********                                                                                         ");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" *********                                                                                         ");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" **     ***                                                                                        ");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" **      **                                                                                        ");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" **     ***                                                                                        ");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" **    ***       **    ********* ********* **        ********   ********   **     **  ***  ******* ");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" ********       *  *   ********* ********* **        ********  ******* *   **     **  ***  ********");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" ********      **  **      **       **     **        **        *****       **     **       **    **");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" **     ***   **    **     **       **     **        **          *****     **     **  ***  **    **");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" **       **  **    **     **       **     **        *******      *****    *********  ***  **    **");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" **        ** **    **     **       **     **        *******       *****   *********  ***  ********");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" **        ** ********     **       **     **        **             *****  **     **  ***  ******* ");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" **        ** ********     **       **     **        **              ****  **     **  ***  **      ");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" **      ***  **    **     **       **     **        **        *    ****   **     **  ***  **      ");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" **********   **    **     **       **     ********* ********* *******     **     **  ***  **      ");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine(" ********     **    **     **       **     ********* ********* ******      **     **  ***  **      ");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine("                                                                                                  ");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                                           - Press 1 to START                                     ");
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.WriteLine("                                           - Press 0 to EXIT                                      ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(Console.CursorLeft = LEFT_LOGO_CURSOR, Console.CursorTop);
            Console.Write("                                        Please, make your choice:");
        }

        public static void ShowResultOfShot(bool isAlive, bool isTarget)
        {
            string condition = "PAST!";
            ConsoleColor color = ConsoleColor.Cyan;
            int cursorLeft = MAP_SIZE;
            int cursorTop = MESSAGE_START_POSITION;

            if (isTarget)
            {
                if (isAlive)
                {
                    condition = "INJURED!";
                    color = ConsoleColor.DarkYellow;
                }
                else
                {
                    condition = "DESTROYED!";
                    color = ConsoleColor.DarkRed;
                }
            }

            ClearBottom();
            ShowMessage(condition, cursorLeft, cursorTop, color);
        }

        public static Level InputDifficultyLevel()
        {
            Console.Clear();

            Console.WriteLine("Please input difficulty level:");

            for (Level i = Level.Easy; i <= Level.Hard; i++)
            {
                Console.WriteLine($"{i} - {(int)i}");
            }

            bool isNumber;
            int levelNumber;

            do
            {
                isNumber = int.TryParse(Console.ReadLine(), out levelNumber);

            } while (!isNumber || (levelNumber > 3) || (levelNumber < 1));

            return (Level)levelNumber;
        }

        public static void PrintCellChanges(object sender, InjuredShipEventArgs e)
        {

        }

        public static Position InputShipPosition(TypeOfShips deckCount, out Direction shipDirection, 
                ConsoleColor color = ConsoleColor.Yellow)
        {
            int direction;
            bool isRigthDirection;
            Console.ForegroundColor = color;

            do
            {
                ClearBottom(0, 47, 7);

                Console.SetCursorPosition(0, 0);

                Console.WriteLine("DIRECTION:");

                for (Direction i = Direction.Up; i <= Direction.Left; i++)
                {
                    Console.WriteLine("{0} - {1}", i, ((int)i));
                }

                Console.Write("Please, input direction for {0} ship:", deckCount);

                isRigthDirection = int.TryParse(Console.ReadLine(), out direction);

                shipDirection = (Direction)direction;

            } while (!isRigthDirection || shipDirection < Direction.Up 
                    || shipDirection > Direction.Left);

            return AreRigthCoords(SHIP_POSITION_MESSAGE, 6);
        }

        public static MethodShipsBuild EnterShipsBuildingMethod()
        {
            Console.Clear();

            Console.WriteLine("Please input method of ships building:");

            for (MethodShipsBuild i = MethodShipsBuild.AutoRandom; i <= MethodShipsBuild.Manual; i++)
            {
                Console.WriteLine($"{i} - {(int)i}");
            }

            bool isNumber;
            int methodNumber;

            do
            {
                isNumber = int.TryParse(Console.ReadLine(), out methodNumber);

            } while (!isNumber || (methodNumber > 2) || (methodNumber < 1));

            return (MethodShipsBuild)methodNumber;
        }

        public static bool IsNewGame()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            bool isNumber;
            int number;

            do
            {
                Console.Clear();
                Console.WriteLine("Please input number\nNew game - 1\nContinue game - 2");
                isNumber = int.TryParse(Console.ReadLine(), out number);


            } while (!isNumber || (number < 1) || (number > 2));

            return number == 1;
        }

        public static void PrintExitSymbol()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, 18);
            Console.WriteLine("Exit - [Esc]");
        }
    }
}
