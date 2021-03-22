using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipLibrary;

namespace __Battleship
{
    [Serializable]
    class ConsoleGame
    {
        #region Constant

        public const string PATH_TO_FOLDER = "..\\LastSaving";
        public const string PATH_TO_FILE = PATH_TO_FOLDER + "\\Battleship.bin";

        #endregion

        #region Private

        private UserActions _numberUserAction;
        private Sea _playerMap;
        private Sea _enemyMap;
        private bool _isNewGame;
        private bool _isPlayerWinner;
        private bool _isAlivePlayerAfterRigthShoot;
        private string _enemyTurn = "ENEMY'S TURN!";
        private string _yourTurn = "YOUR TURN!";
        private bool _isEasyLevel;
        private Intelligence _enemysMind;
        private bool _isTargetPlayer;
        private bool _isTargetEnemy;
        private MethodShipsBuild _introducedMethod;

        #endregion

        public   ConsoleGame(UserActions numberUserAction, MethodShipsBuild introducedMethod, 
                Intelligence enemysMind, bool isEasyLevel)
        {
            _numberUserAction = numberUserAction;
            _introducedMethod = introducedMethod;
            _enemysMind = enemysMind;
            _isEasyLevel = isEasyLevel;
            _playerMap = null;
            _enemyMap = null;
            _isPlayerWinner = false;
            _isAlivePlayerAfterRigthShoot = false;
            _enemyTurn = "ENEMY'S TURN!";
            _yourTurn = "YOUR TURN!";
            _isEasyLevel = false;
            _isTargetPlayer = false;
            _isTargetEnemy = false;
            _isNewGame = true; 
        }

        public void Run()
        {
            bool shipSearched = true;

            while (_numberUserAction == UserActions.StartGame && shipSearched)
            {
                Console.Clear();

                if (_isNewGame)
                {
                    _enemyMap = new Sea(RandomCoords.MAP_SIZE);
                    _enemyMap.BuildAllTypeOfShips();
                    GetPlayerMap();
                }

                Console.Clear();
                UserInterface.PrintStartPosition(_enemyMap, _playerMap);

                do
                {
                    shipSearched = _enemyMap.SearchShips();
                    if (!shipSearched)
                    {
                        break;
                    }

                    _numberUserAction = ShotPlayer();

                    if (_numberUserAction == UserActions.GameOver)
                    {
                        SaveGameCondition();
                        _isNewGame = false;
                        Console.Clear();
                        Console.WriteLine("GAME SAVED!\nPLease, press any key!");
                        break;
                    }

                    shipSearched = _enemyMap.SearchShips();
                    if (!shipSearched)
                    {
                        _isPlayerWinner = true;
                        break;
                    }

                    _numberUserAction = ShotEnemy();

                    if (_numberUserAction == UserActions.GameOver)
                    {
                        SaveGameCondition();
                        _isNewGame = false;
                        Console.Clear();
                        Console.WriteLine("GAME SAVED!\nPLease, press any key!");
                        break;
                    }

                } while (shipSearched);

                if (_numberUserAction == UserActions.StartGame)
                {
                    UserInterface.PrinPlayerWin(_isPlayerWinner);
                }
            }
        }

        public bool IsNewGame 
        {
            get
            {
                return _isNewGame;
            }
            set
            {
                _isNewGame = value;
            }
        }

        public UserActions NumberUserAction 
        {
            get
            {
                return _numberUserAction;
            }
            set
            {
                _numberUserAction = value;
            }
        }

        private void GetPlayerMap()
        {
            PlayerSea playerMapManualInput = null;

            switch (_introducedMethod)
            {
                case MethodShipsBuild.AutoRandom:
                    _playerMap = new Sea(10);
                    _playerMap.BuildAllTypeOfShips();
                    break;
                case MethodShipsBuild.Manual:
                    playerMapManualInput = new PlayerSea(10);

                    bool isPossibleSetting;
                    int shipCount = 1;
                    TypeOfShips deckCount = TypeOfShips.FourDecker;

                    Console.Clear();

                    for (int i = 0; i < RandomCoords.COUNT_OF_SHIPS_TYPE; i++)
                    {
                        for (int j = 0; j < shipCount; j++)
                        {
                            UserInterface.PrintManualInputShips(playerMapManualInput);
                            Console.SetCursorPosition(0, 0);
                            Direction shipDirection;
                            Position shipPosition = UserInterface.InputShipPosition(deckCount, out shipDirection);
                            isPossibleSetting = playerMapManualInput.BuildOneTypeOfShips(deckCount, 
                                    shipPosition, shipDirection);

                            if (!isPossibleSetting)
                            {
                                UserInterface.ClearBottom(0, 45, 7);
                                UserInterface.ShowMessage(Constants.IMPOSSIBLE_SETTING, 0, 0);
                                System.Threading.Thread.Sleep(3000);
                                j--;
                                continue;
                            }

                            UserInterface.PrintManualInputShips(playerMapManualInput);


                        }

                        deckCount--;
                        shipCount++;
                    }

                    break;
                default:
                    break;
            }

            if (_playerMap == null)
            {
                _playerMap = playerMapManualInput;
            }
        }

        private UserActions ShotPlayer()
        {
            UserActions chosenAction = UserActions.StartGame;

            do
            {
                if (CheckExit())
                {
                    chosenAction = UserActions.GameOver;
                    break;
                }

                bool shipSearched = _enemyMap.SearchShips();

                if (!shipSearched)
                {
                    break;
                }

                UserInterface.ShowMessage(_yourTurn);
                bool wasShot;

                do
                {
                    Position coords = UserInterface.AreRigthCoords();

                    if ((coords.OX == -1) || (coords.OY == -1))
                    {
                        _numberUserAction = UserActions.GameOver;
                        break;
                    }

                    _enemyMap.TargetCoordY = coords.OY;
                    _enemyMap.TargetCoordX = coords.OX;
                    wasShot = _enemyMap.WasShot();
                    UserInterface.AreSimilarCoords(wasShot);

                } while (wasShot);

                bool isFinishedOfShipEnemy = false;
                _isTargetEnemy = _enemyMap.HitTarget(ref isFinishedOfShipEnemy);

                int cursorLeft = UserInterface.START_LEFT_CURSOR;
                int cursorTop = UserInterface.START_TOP_CURSOR;

                if (_isTargetEnemy && !isFinishedOfShipEnemy)
                {
                    _enemyMap.MarkImpossibleTargets();
                }

                UserInterface.PrintShipEnemy(_enemyMap, cursorLeft, cursorTop);
                UserInterface.ShowResultOfShot(isFinishedOfShipEnemy, _isTargetEnemy);
                UserInterface.PrintExitSymbol();/////////////////

                System.Threading.Thread.Sleep(1500);

            } while (_isTargetEnemy);

            return chosenAction;
        }

        private UserActions ShotEnemy()
        {
            UserActions chosenAction = UserActions.StartGame;

            if (_isEasyLevel)
            {
                ShotWithoutIntellegence(ref chosenAction);
            }
            else
            {
                ShotWithEnemyIntellegence(ref chosenAction);
            }

            return chosenAction;
        }

        private void SaveGameCondition()
        {
            PlayerMapSerializer.CreateDirectory(PATH_TO_FOLDER);

            using (PlayerMapSerializer saver = new PlayerMapSerializer(PATH_TO_FOLDER, PATH_TO_FILE))
            {
                saver.WriteMapsCondition(this);
            }
        }

        private bool CheckExit()
        {
            bool isExit = false;

            if (Console.KeyAvailable)
            {
                ConsoleKey keyReal = Console.ReadKey().Key;

                if (keyReal == ConsoleKey.Escape)
                {
                    isExit = true;
                }
            }

            return isExit;
        }

        private void ShotWithEnemyIntellegence(ref UserActions chosenAction)
        {
            do
            {
                UserInterface.ShowMessage(_enemyTurn);
                System.Threading.Thread.Sleep(1000);

                if (CheckExit())
                {
                    chosenAction = UserActions.GameOver;
                    break;
                }

                _enemysMind.MakeTheShot(ref _isAlivePlayerAfterRigthShoot, _playerMap);
                _isTargetPlayer = _enemysMind.IsTargetPlayer;

                _playerMap.CheckShipCondition(_isTargetPlayer, _isAlivePlayerAfterRigthShoot, _enemysMind);

                int cursorLeft = UserInterface.DISTANCE_BETWEEN_MAP;
                int cursorTop = UserInterface.START_TOP_CURSOR;
                UserInterface.PrintShipPlayer(_playerMap, cursorLeft, cursorTop);
                UserInterface.ShowResultOfShot(_isAlivePlayerAfterRigthShoot, _isTargetPlayer);
                UserInterface.PrintExitSymbol();/////////////////

                System.Threading.Thread.Sleep(1500);
                bool shipSearched = _playerMap.SearchShips();

                if (!shipSearched)
                {
                    break;
                }

            } while (_isTargetPlayer);
        }

        private void ShotWithoutIntellegence(ref UserActions chosenAction)
        {
            do
            {
                UserInterface.ShowMessage(_enemyTurn);
                System.Threading.Thread.Sleep(1000);

                if (CheckExit())
                {
                    chosenAction = UserActions.GameOver;
                    break;
                }

                RandomCoords.SearchRandomCoords(_playerMap);
                _isTargetPlayer = _playerMap.HitTarget(ref _isAlivePlayerAfterRigthShoot);

                int cursorLeft = UserInterface.DISTANCE_BETWEEN_MAP;
                int cursorTop = UserInterface.START_TOP_CURSOR;
                UserInterface.PrintShipPlayer(_playerMap, cursorLeft, cursorTop);
                UserInterface.ShowResultOfShot(_isAlivePlayerAfterRigthShoot, _isTargetPlayer);
                UserInterface.PrintExitSymbol();

                System.Threading.Thread.Sleep(1500);
                bool shipSearched = _playerMap.SearchShips();

                if (!shipSearched)
                {
                    break;
                }

            } while (_isTargetPlayer);
        }
    }
}
