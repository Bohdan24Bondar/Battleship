using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    [Serializable]
    public class Intelligence
    {
        protected Direction _targetDirection;
        protected int _counterSuccessfulShot;
        protected Position _cleanShotPosition;
        protected bool _isTargetPlayer;

        public Intelligence(int coordY, int coordX)
        {
            _targetDirection = RandomCoords.GetRandomDirection();
            _counterSuccessfulShot = 0;
            _cleanShotPosition = new Position(coordY, coordX);
            _isTargetPlayer = false;
        }    

        public Direction TargetDirection 
        {
            get
            {
                return _targetDirection;
            }
            set
            {
                _targetDirection = value;
            } 
        }

        public int CounterSuccessfulShot
        {
            get
            {
                return _counterSuccessfulShot;
            }
            set
            {
                _counterSuccessfulShot = value;
            }
        }

        public bool IsTargetPlayer
        {
            get
            {
                return _isTargetPlayer;
            }
            private set
            {
                _isTargetPlayer = value;
            }
        }

        public void SaveCoordsSuccessfulTarget(Sea playerMap)
        {
            _cleanShotPosition.OY = playerMap.TargetCoordY;
            _cleanShotPosition.OX = playerMap.TargetCoordX;
        }

        public virtual void MakeTheShot(ref bool isAlivePlayerAfterShoot, Sea playerMap)
        {
            if (!isAlivePlayerAfterShoot)
            {
                RandomCoords.SearchRandomCoords(playerMap);
                _isTargetPlayer = playerMap.HitTarget(ref isAlivePlayerAfterShoot);

                if (_isTargetPlayer)
                {
                    SaveCoordsSuccessfulTarget(playerMap);
                }
            }
            else
            {
                GetTargetCoords(playerMap);
                _isTargetPlayer = playerMap.HitTarget(ref isAlivePlayerAfterShoot);
            }
        }

        public void GetTargetCoords(Sea playerMap)
        {
            bool wasShot = true;
            playerMap.TargetCoordY = _cleanShotPosition.OY;
            playerMap.TargetCoordX = _cleanShotPosition.OX;

            do
            {
                if (_targetDirection > Direction.Left)
                {
                    _targetDirection = Direction.Up;
                }

                switch (_targetDirection)
                {
                    case Direction.Up:
                        playerMap.TargetCoordY -= _counterSuccessfulShot;
                        break;
                    case Direction.Right:
                        playerMap.TargetCoordX += _counterSuccessfulShot;
                        break;
                    case Direction.Down:
                        playerMap.TargetCoordY += _counterSuccessfulShot;
                        break;
                    case Direction.Left:
                        playerMap.TargetCoordX -= _counterSuccessfulShot;
                        break;
                    default:
                        break;
                }

                if ((playerMap.TargetCoordX < 0) || (playerMap.TargetCoordX >= RandomCoords.MAP_SIZE)
                        || (playerMap.TargetCoordY < 0) 
                        || (playerMap.TargetCoordY >= RandomCoords.MAP_SIZE))
                {
                    if (_counterSuccessfulShot >= RandomCoords.COUNT_OF_COORDS)
                    {
                        playerMap.TargetCoordY = _cleanShotPosition.OY;
                        playerMap.TargetCoordX = _cleanShotPosition.OX;
                        _targetDirection += RandomCoords.COUNT_OF_COORDS;
                        continue;

                    }
                    else
                    {
                        playerMap.TargetCoordY = _cleanShotPosition.OY;
                        playerMap.TargetCoordX = _cleanShotPosition.OX;
                        _counterSuccessfulShot = 1;
                        _targetDirection++;
                        continue;
                    }
                }

                wasShot = playerMap.WasShot();

                if ((_counterSuccessfulShot < RandomCoords.SECCESSFUL_SHOT) && wasShot)
                {
                    _targetDirection++;
                    _counterSuccessfulShot = 1;
                    playerMap.TargetCoordY = _cleanShotPosition.OY;
                    playerMap.TargetCoordX = _cleanShotPosition.OX;
                }
                else
                {
                    if (wasShot && _counterSuccessfulShot >= RandomCoords.SECCESSFUL_SHOT)
                    {
                        int counter = (int)_targetDirection + RandomCoords.SECCESSFUL_SHOT;
                        _targetDirection = (Direction)counter;
                        _counterSuccessfulShot = 1;
                        playerMap.TargetCoordY = _cleanShotPosition.OY;
                        playerMap.TargetCoordX = _cleanShotPosition.OX;
                    }
                }

            } while (wasShot);
        }
    }
}
