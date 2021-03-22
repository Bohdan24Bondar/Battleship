using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipLibrary;

namespace BattleshipApp
{
    [Serializable]
    public class FormGame
    {

        #region Constants

        const int HEIGTH = 10;
        const int WIDTH = 10;
        const int COUNT_THREE_DECK_SHIP = 2;
        const int COUNT_TWO_DECK_SHIP = 3;
        const int COUNT_ONE_DECK_SHIP = 4;
        const int COUNT_ALL_SHIPS = 10;

        #endregion

        #region Private

        private Sea _playerMap;
        private Sea _enemyMap;
        private bool _isTargetEnemy;
        private bool _isTargetPlayer;
        private bool _isAliveEnemyAfterRigthShot;
        private bool _isAlivePlayerAfterRigthShoot;
        private bool _isEasyLevel;
        private Intelligence _enemysMind;
        private string _name;
        private PlayerSea _playerMapManualInput;
        private Position _startShipPosition;

        #endregion

        public FormGame()
        {
            _playerMap = null;
            _enemyMap = null;
            _isTargetEnemy = false;
            _isTargetPlayer = false;
            _isAliveEnemyAfterRigthShot = false;
            _isAlivePlayerAfterRigthShoot = false;
            _isEasyLevel = false;
            _enemysMind = null;
            _name = null;
            _playerMapManualInput = new PlayerSea(COUNT_ALL_SHIPS);
            _startShipPosition = new Position(-1, -1) ;
        }

        public Sea PlayerMap 
        {
            get
            {
                return _playerMap;
            }
            set
            {
                _playerMap = value;
            }
        }

        public Sea EnemyMap
        {
            get
            {
                return _enemyMap;
            }
            set
            {
                _enemyMap = value;
            }
        }

        public bool IsTargetEnemy 
        {
            get
            {
                return _isTargetEnemy;
            }
            set
            {
                _isTargetEnemy = value;
            }
        }

        public bool IsTargetPlayer
        {
            get
            {
                return _isTargetPlayer;
            }
            set
            {
                _isTargetPlayer = value;
            }
        }

        public bool IsEasyLevel
        {
            get
            {
                return _isEasyLevel;
            }
            set
            {
                _isEasyLevel = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public PlayerSea PlayerMapManualInput
        {
            get
            {
                return _playerMapManualInput;
            }
            set
            {
                _playerMapManualInput = value;
            }
        }

        public MapCondition GetMapManualCondition(int OY, int OX)
        {
            return _playerMapManualInput[OY, OX];
        }

        public Position StartShipPosition
        {
            get
            {
                return _startShipPosition;
            }
            set
            {
                _startShipPosition = value;
            }
        }

        public void SetShipsAuto()
        {
            _playerMap = new Sea(10);
            _playerMap.BuildAllTypeOfShips();
        }

        public void SetGameLevel(Level gameLavel)
        {
            switch (gameLavel)
            {
                case Level.Easy:
                    break;
                case Level.Meduim:
                    _enemysMind = new Intelligence(-1, -1);
                    break;
                case Level.Hard:
                    _enemysMind = new AdvancedIntelligence(-1, -1);
                    break;
                default:
                    break;
            }
        }

        public void SetPlayerMapCondition()
        {
            for (int OY = 0; OY < HEIGTH; OY++)
            {
                for (int OX = 0; OX < WIDTH; OX++)
                {
                    _playerMapManualInput[OY, OX] = MapCondition.NoneShot;
                }
            }
        }

        public void SetOneShip( Direction currentDirection)
        {
            TypeOfShips deckCount = 0;

            if (_playerMapManualInput.CountAliveShips == 0)
            {
                deckCount = TypeOfShips.FourDecker;
            }
            else
            {
                if (_playerMapManualInput.CountAliveShips == 1 || _playerMapManualInput.CountAliveShips == COUNT_THREE_DECK_SHIP)
                {
                    deckCount = TypeOfShips.ThreeDecker;
                }
                else
                {
                    if (_playerMapManualInput.CountAliveShips >= COUNT_TWO_DECK_SHIP 
                        && _playerMapManualInput.CountAliveShips < (COUNT_ALL_SHIPS - COUNT_ONE_DECK_SHIP))
                    {
                        deckCount = TypeOfShips.TwoDecker;
                    }
                    else
                    {
                        if (_playerMapManualInput.CountAliveShips >= (COUNT_ALL_SHIPS - COUNT_ONE_DECK_SHIP) 
                            && _playerMapManualInput.CountAliveShips < COUNT_ALL_SHIPS)
                        {
                            deckCount = TypeOfShips.OneDecker;
                        }
                    }
                }
            }

            _playerMapManualInput.BuildOneTypeOfShips(deckCount, _startShipPosition, currentDirection);
        }

        public void IsPlayerTurn(out string message, out bool isEnemyTurn, out bool isPlayerWin)
        {
            isPlayerWin = false;
            isEnemyTurn = false;

            if (_enemyMap.SearchShips())
            {
                _isTargetEnemy = _enemyMap.HitTarget(ref _isAliveEnemyAfterRigthShot);

                if (_isTargetEnemy && !_isAliveEnemyAfterRigthShot)
                {
                    _enemyMap.MarkImpossibleTargets();
                }

                if (_isTargetEnemy)
                {
                    message = (_enemyMap.TargetCoordY + 1)
                           + ((Letters)_enemyMap.TargetCoordX).ToString()
                           + "\nHit the target!"
                           + "\n Your turn!";
                }
                else
                {
                    message = (_enemyMap.TargetCoordY + 1)
                           + ((Letters)_enemyMap.TargetCoordX).ToString()
                           + "\nPast!"
                           + "\nEnemy's turn!";
                    isEnemyTurn = true;
                }
            }
            else
            {
                message = "YOU WON!!!";
                isPlayerWin = true;
            }
        }


        public void IsEnemyTurn(out string message, out bool isEnemyWin)
        {
            isEnemyWin = false;

            if (_isEasyLevel)
            {
                RandomCoords.SearchRandomCoords(_playerMap);
                _isTargetPlayer = _playerMap.HitTarget(ref _isAlivePlayerAfterRigthShoot);
            }
            else
            {
                _enemysMind.MakeTheShot(ref _isAlivePlayerAfterRigthShoot, _playerMap);
                _isTargetPlayer = _enemysMind.IsTargetPlayer;
                _playerMap.CheckShipCondition(_isTargetPlayer, _isAlivePlayerAfterRigthShoot, _enemysMind);
            }


            if (_isTargetPlayer)
            {
                message = (_playerMap.TargetCoordY + 1)
                   + ((Letters)_playerMap.TargetCoordX).ToString()
                   + "\nHit the target!"
                   + "\n Enemy's turn!";
            }
            else
            {
                message = (_playerMap.TargetCoordY + 1)
                   + ((Letters)_playerMap.TargetCoordX).ToString()
                   + "\nPast!!"
                   + "\nYour turn!";
            }

            if (!_playerMap.SearchShips())
            {
                isEnemyWin = true;
                message = "ENEMY WON!!!";
            }
        }

        public bool HasAllShipsPlayer()
        {
            bool areSettedAllShips = _playerMapManualInput.CountAliveShips == 10;

            return areSettedAllShips;
        }

        public void SetEnemyMap()
        {
            _enemyMap.BuildAllTypeOfShips();
        }
    }
}
