using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    [Serializable]
    public class PlayerSea : Sea
    {
        public PlayerSea(int sizeOfMap)
            : base(sizeOfMap)
        {
            
        }

        public bool BuildOneTypeOfShips(TypeOfShips deckCount, Position startShipPosition, Direction shipDirection)
        {
            bool isPossibleSetteing = true;

            _targetCoordY = startShipPosition.OY;
            _targetCoordX = startShipPosition.OX;

            for (TypeOfShips i = TypeOfShips.OneDecker; i <= deckCount; i++)
            {
                isPossibleSetteing = HasCollide();

                if (!isPossibleSetteing)
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
                        break;
                }
            }

            if (isPossibleSetteing)
            {
                _targetCoordY = startShipPosition.OY;
                _targetCoordX = startShipPosition.OX;

                SetShips(deckCount, shipDirection);
            }

            return isPossibleSetteing;///если false - возвращаем переменную и выводим пользователю сообщение что нужна новая позиция корабля
        }

    }
}
