using BattleshipLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    [Serializable]
    public class Sea
    {
        protected MapCondition[,] _mapCells;
        protected Ship[] _allShips;
        protected int _countAliveShips;
        protected int _sizeOfMap;
        protected int _targetCoordY;
        protected int _targetCoordX;
        protected GetDestroyedShip _destroyedShip;
        protected GetInjuredDeck _injuredShip;
        protected GetMissedCell _missedCell;

        public Sea(int sizeOfMap)
        {
            _mapCells = new MapCondition[sizeOfMap, sizeOfMap];
            _allShips = new Ship[sizeOfMap];
            _countAliveShips = 0;
            _sizeOfMap = sizeOfMap;
            _destroyedShip = null;
            _injuredShip = null;
            _missedCell = null;
        }

        public event GetDestroyedShip DestroyedShip
        {
            add
            {
                _destroyedShip += value;
            }
            remove
            {
                _destroyedShip -= value;
            }
        }

        public event GetInjuredDeck InjuredShip
        {
            add
            {
                _injuredShip += value;
            }
            remove
            {
                _injuredShip -= value;
            }
        }

        public event GetMissedCell MissedCell
        {
            add
            {
                _missedCell += value;
            }
            remove
            {
                _missedCell -= value;
            }
        }


        public int CountAliveShips
        {
            get
            {
                return _countAliveShips;
            }
            set
            {
                _countAliveShips = value;
            }
        }

        public MapCondition this[int indexOY, int indexOX ]
        {
            get 
            {
                return _mapCells[indexOY, indexOX];
            }
            set 
            {
                _mapCells[indexOY, indexOX] = value;

            }
        }

        public int TargetCoordY 
        {
            get
            {
                return _targetCoordY;
            }
            set
            {
                _targetCoordY = value;
            }
        }

        public int TargetCoordX
        {
            get
            {
                return _targetCoordX;
            }
            set
            {
                _targetCoordX = value;
            }
        }

        public int SizeOfMap
        {
            get
            {
                return _targetCoordX;
            }
            set
            {
                _targetCoordX = value;
            }
        }

        public int GetLengthMapCells(int index)
        {
            if (index < 0 || index > _mapCells.Rank)
            {
                throw new Exception();
            }

            return _mapCells.GetLength(index);
        }

        #region BuildSipsMethods(Методы построения кораблей)

        public virtual void BuildAllTypeOfShips()
        {
            TypeOfShips countOfDecker = TypeOfShips.FourDecker;

            for (int i = 1; i <= RandomCoords.COUNT_OF_SHIPS_TYPE; i++)
            {
                bool shipBuiltRigth = BuildOneTypeOfShips(countOfDecker, i);

                if (shipBuiltRigth)
                {
                    countOfDecker--;
                }
            }
        }

        private bool BuildOneTypeOfShips(TypeOfShips countOfDecker,
                int necessaryShipsCount)
        {
            Direction shipDirection;
            int shipCounter = 0;
            bool isPossibleSetting = true;

            while (shipCounter < necessaryShipsCount)
            {
                RandomCoords.GetRandomCoords(this);
                isPossibleSetting = IsPossibleSetting(countOfDecker, out shipDirection);

                if (isPossibleSetting)
                {
                    SetShips(countOfDecker, shipDirection);
                    shipCounter++;
                }
            }

            return isPossibleSetting;
        }

        protected void SetShips(TypeOfShips countOfDecker, Direction shipDirection)
        {
            Ship oneShip = new Ship((int)countOfDecker, true);

            for (int deckNumber = 0; deckNumber < (int)(countOfDecker); deckNumber++) 
            {
                _mapCells[_targetCoordY, _targetCoordX] = MapCondition.ShipSafe;
                oneShip.CountOfDeck = (int)(countOfDecker);
                oneShip[deckNumber] = new Position(_targetCoordY, _targetCoordX);

                switch (shipDirection)
                {
                    case Direction.Up:
                        _targetCoordY--;
                        break;
                    case Direction.Right:
                        _targetCoordX++;
                        break;
                    case Direction.Down:
                        _targetCoordY++;
                        break;
                    case Direction.Left:
                        _targetCoordX--;
                        break;
                    default:
                        break;
                }
            }

            _allShips[CountAliveShips] = oneShip;
            CountAliveShips++;
        }

        public bool IsPossibleSetting(TypeOfShips countOfDecker, out Direction shipDirection)
        {
            shipDirection = RandomCoords.GetRandomDirection();

            int startPositionX = _targetCoordX;
            int startPositionY = _targetCoordY;
            bool isPossibleSet = true;

            for (int i = 1; i <= RandomCoords.COUNT_OF_DIRECTION; i++)
            {
                for (int j = 0; j < (int)countOfDecker; j++)
                {
                    isPossibleSet = HasCollide();

                    if (!isPossibleSet)
                    {
                        break;
                    }

                    switch (shipDirection)  
                    {
                        case Direction.Up:
                            _targetCoordY--;
                            break;
                        case Direction.Right:
                            _targetCoordX++;
                            break;
                        case Direction.Down:
                            _targetCoordY++;
                            break;
                        case Direction.Left:
                            _targetCoordX--;
                            break;
                        default:
                            isPossibleSet = false;
                            break;
                    }
                }

                if (isPossibleSet)
                {
                    break;
                }
                else
                {
                    shipDirection++;

                    if (shipDirection > Direction.Left)
                    {
                        shipDirection -= Direction.Left;
                    }
                    
                }
            }

            _targetCoordX = startPositionX;
            _targetCoordY = startPositionY;

            return isPossibleSet;
        }

        protected bool HasCollide()
        {
            bool isPossibleSetting = true;

            if ((_targetCoordX < 0) || (_targetCoordX >= RandomCoords.MAP_SIZE)
                    || (_targetCoordY < 0)
                    || (_targetCoordY >= RandomCoords.MAP_SIZE))
            {
                isPossibleSetting = false;
            }
            else
            {
                for (int i = (_targetCoordY - 1); i <= (_targetCoordY + 1); i++)
                {
                    if (i < 0 || i >= RandomCoords.MAP_SIZE)
                    {
                        continue;
                    }

                    for (int j = (_targetCoordX - 1); j <= (_targetCoordX + 1); j++)
                    {
                        if (j < 0 || j >= RandomCoords.MAP_SIZE)
                        {
                            continue;
                        }

                        if (_mapCells[i, j] == MapCondition.ShipSafe)
                        {
                            isPossibleSetting = false;
                            break;
                        }
                    }

                    if (!isPossibleSetting)
                    {
                        break;
                    }
                }
            }

            return isPossibleSetting;
        }

        #endregion

        #region FinishOffShipMethods (Методы добивания корабля)

        public void CheckShipCondition(bool isTargetPlayer, bool isAlivePlayerAfterRigthShoot, Intelligence enemyMind)
        {
            if ((enemyMind.CounterSuccessfulShot >= RandomCoords.SECCESSFUL_SHOT) 
                    && !isTargetPlayer && isAlivePlayerAfterRigthShoot)
            {
                enemyMind.CounterSuccessfulShot = 1;
                int counter = (int)enemyMind.TargetDirection + RandomCoords.SECCESSFUL_SHOT;
                enemyMind.TargetDirection = (Direction)counter;

                if (enemyMind.TargetDirection > Direction.Left)
                {
                    enemyMind.TargetDirection -= Direction.Left;
                }
            }
            else
            {
                if (isAlivePlayerAfterRigthShoot && !isTargetPlayer)
                {
                    enemyMind.TargetDirection++;
                }
            }

            if (!isAlivePlayerAfterRigthShoot && isTargetPlayer)
            {
                enemyMind.CounterSuccessfulShot = 0;
                enemyMind.TargetDirection = Direction.Up;
                enemyMind.TargetDirection = RandomCoords.GetRandomDirection();//////этот метод для изи уровня как новый емени майнд
                MarkImpossibleTargets();
            }

            if (isAlivePlayerAfterRigthShoot && isTargetPlayer)
            {
                enemyMind.CounterSuccessfulShot++;
            }
        }

        #endregion

        #region MarkImpossiblePlaceForShotMehtods (Методы маркировки невозможных для выстрела координат)

        public void MarkImpossibleTargets()
        {
            int oldCoordX = _targetCoordX;
            int oldCoordY = _targetCoordY;

            Ship destroyedShip;

            bool isVertical = SearchDestroyedShip(out destroyedShip);

            if (isVertical)
            {
                MarkVerticalDirection(destroyedShip);
            }
            else
            {
                MarkHorizontalDirection(destroyedShip);
            }

            _targetCoordX = oldCoordX;
            _targetCoordY = oldCoordY;
        }

        public bool SearchDestroyedShip(out Ship destroyedShip)
        {
            int hitShipNumber = 0;
            bool isFoundShip = false;

            for (int shipIndex = 0; shipIndex < _countAliveShips; shipIndex++)
            {
                for (int deckNumb = 0; deckNumb < _allShips[shipIndex].CountOfDeck; deckNumb++)
                {

                    if (_allShips[shipIndex][deckNumb].OX == _targetCoordX               
                            && _allShips[shipIndex][deckNumb].OY == _targetCoordY)    
                    {
                        hitShipNumber = shipIndex;
                        isFoundShip = true;
                        break;
                    }
                }

                if (isFoundShip)
                {
                    break;
                }
            }

            int shipSize = _allShips[hitShipNumber].CountOfDeck;

            destroyedShip = new Ship(shipSize, false); 

            for (int deckNumb = 0; deckNumb < shipSize; deckNumb++)
            {
                destroyedShip[deckNumb] = new Position(_allShips[hitShipNumber][deckNumb].OY,
                        _allShips[hitShipNumber][deckNumb].OX);
            }

            bool isVertical = false;


            if (destroyedShip.CountOfDeck > 1)
            {
                if (destroyedShip[0].OX == destroyedShip[1].OX)
                {
                    isVertical = true;
                }
            }

            return isVertical;
        }

        public void MarkVerticalDirection(Ship destroyedShip)
        {
            int size = destroyedShip.CountOfDeck;

            int coordX = destroyedShip[0].OX;
            int coordY = destroyedShip[0].OY;

            if (coordY > destroyedShip[size - 1].OY)
            {
                coordY = destroyedShip[size - 1].OY;
            }

            for (int i = (coordY - 1); i <= (coordY + size); i++)
            {

                if ((i < 0) || (i >= _sizeOfMap)) 
                {
                    continue;
                }

                for (int j = (coordX - 1); j <= (coordX + 1); j++)
                {
                    if (j < 0 || j >= _sizeOfMap)
                    {
                        continue;
                    }

                    if ((j == coordX) && (i >= coordY)
                            && (i < (coordY + size)))
                    {
                        continue;
                    }

                    _targetCoordY = i;
                    _targetCoordX = j;

                    bool wasShot = WasShot();

                    if (wasShot)
                    {
                        continue;
                    }

                    _mapCells[i, j] = MapCondition.MissedShot;
                    Position missedCell = new Position(i, j);
                    _missedCell?.Invoke(this, new MissedShotEventArgs(missedCell));
                }
            }
        }

        public void MarkHorizontalDirection(Ship destroyedShip)
        {
            int size = destroyedShip.CountOfDeck;

            int coordX = destroyedShip[0].OX;
            int coordY = destroyedShip[0].OY;

            if (coordX > destroyedShip[size - 1].OX)
            {
                coordX = destroyedShip[size - 1].OX;
            }

            for (int i = (coordY - 1); i <= (coordY + 1); i++)
            {
                if (i < 0 || i >= RandomCoords.MAP_SIZE)
                {
                    continue;
                }

                for (int j = (coordX - 1); j <= (coordX + size); j++)
                {
                    if (j < 0 || j >= RandomCoords.MAP_SIZE)
                    {
                        continue;
                    }

                    if ((i == coordY) && (j >= coordX)
                            && (j < (coordX + size)))
                    {
                        continue;
                    }

                    _targetCoordY = i;
                    _targetCoordX = j;

                    bool wasShot = WasShot();

                    if (wasShot)
                    {
                        continue;
                    }

                    _mapCells[i, j] = MapCondition.MissedShot;
                    Position missedCell = new Position(i, j);
                    _missedCell?.Invoke(this, new MissedShotEventArgs(missedCell));
                }
            }
        }

        public bool WasShot()
        {
            bool wasShot = false;

            if ((_mapCells[_targetCoordY, _targetCoordX] == MapCondition.MissedShot)
                    || (_mapCells[_targetCoordY, _targetCoordX] == MapCondition.ShipInjured)
                    || (_mapCells[_targetCoordY, _targetCoordX] == MapCondition.ShipDestroyed))
            {
                wasShot = true;
            }

            return wasShot;
        }

        #endregion

        #region HitTargetMethods(Методы произведения выстрелов)

        public bool HitTarget(ref bool isAlivePlayerAfterRigthShoot)
        {
            bool isTarget;

            if (_mapCells[_targetCoordY, _targetCoordX] == MapCondition.ShipSafe)
            {
                isTarget = true;
                isAlivePlayerAfterRigthShoot = BreakDeck();
            }
            else
            {
                isTarget = false;
                MarkMissedShot();
            }

            return isTarget;
        }

        #endregion

        #region SetMapConditionMethods(Методы состояния карты)

        public bool BreakDeck()
        {
            int numberInjuredShip = 0;
            int numberInjuredDeck = 0;
            bool isFoundShip = false;

            for (int shipNumb = 0; shipNumb < _countAliveShips; shipNumb++)
            {
                if (!_allShips[shipNumb].IsAliveShip)
                {
                    continue;
                }

                for (int deckNumb = 0; deckNumb < _allShips[shipNumb].CountOfDeck; deckNumb++)
                {

                    if (_allShips[shipNumb][deckNumb].OX == _targetCoordX
                            && _allShips[shipNumb][deckNumb].OY == _targetCoordY)
                    {
                        _allShips[shipNumb].CountInjuredDeck++;
                        numberInjuredShip = shipNumb;
                        numberInjuredDeck = deckNumb;
                        isFoundShip = true;
                    }

                    if (_allShips[shipNumb].CountOfDeck == _allShips[shipNumb].CountInjuredDeck)
                    {
                        _allShips[shipNumb].IsAliveShip = false;
                    }
                }

                if (isFoundShip)
                {
                    break;
                }
            }

            if (_allShips[numberInjuredShip].IsAliveShip)
            {
                _mapCells[_targetCoordY, _targetCoordX] = MapCondition.ShipInjured;
                _injuredShip?.Invoke(this, new InjuredShipEventArgs(_allShips[numberInjuredShip][numberInjuredDeck]));
            }
            else
            {
                for (int deckNumb = 0; deckNumb < _allShips[numberInjuredShip].CountOfDeck; deckNumb++)
                {
                    _targetCoordX = _allShips[numberInjuredShip][deckNumb].OX;
                    _targetCoordY = _allShips[numberInjuredShip][deckNumb].OY;
                    _mapCells[_targetCoordY, _targetCoordX] = MapCondition.ShipDestroyed;

                    
                    _destroyedShip?.Invoke(this, new DestroyedShipEventArgs(_allShips[numberInjuredShip]));
                }

            }

            return _allShips[numberInjuredShip].IsAliveShip;
        }

        public void MarkMissedShot()
        {
            _mapCells[_targetCoordY, _targetCoordX] = MapCondition.MissedShot;
            Position missedCell = new Position(_targetCoordY, _targetCoordX);
            _missedCell?.Invoke(this, new MissedShotEventArgs(missedCell));
        }

        #endregion

        #region GetStatusGameMethods (Методы состояния игры)

        public bool SearchShips()
        {
            bool shipSearched = false;

            for (int i = 0; i < _mapCells.GetLength(0); i++)
            {
                for (int j = 0; j < _mapCells.GetLength(1); j++)
                {
                    if (_mapCells[i, j] == MapCondition.ShipSafe)
                    {
                        shipSearched = true;
                        break;
                    }
                }

                if (shipSearched)
                {
                    break;
                }
            }

            return shipSearched;
        }

        #endregion

    }
}
