using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    [Serializable]
    public class Ship
    {
        private int _countOfDeck;
        private int _countInjuredDeck;
        private Position[] _deckCoords;
        private bool _isAliveShip;

        public Ship(int countOfDeck, bool isAliveShip)
        {
            _countOfDeck = countOfDeck;
            _countInjuredDeck = 0;
            _deckCoords = new Position[countOfDeck];
            _isAliveShip = isAliveShip;
        }

        public Ship(Ship copyShip)
        {
            _countOfDeck = copyShip.CountOfDeck;
            _countInjuredDeck = copyShip.CountInjuredDeck;
            _deckCoords = new Position[copyShip.CountOfDeck];

            for (int i = 0; i < copyShip.CountOfDeck; i++)
            {
                _deckCoords[i] = new Position();
                _deckCoords[i] = copyShip[i];
            }

            _isAliveShip = copyShip.IsAliveShip;
        }

        public int CountOfDeck 
        {
            get
            {
                return _countOfDeck;
            } 
            set
            {
                _countOfDeck = value;
            }
        }

        public int CountInjuredDeck 
        {
            get
            {
                return _countInjuredDeck;
            }
            set
            {
                _countInjuredDeck = value;
            }
        }

        public bool IsAliveShip 
        { 
            get
            {
                return _isAliveShip;
            }
            set
            {
                _isAliveShip = value;
            }
        }

        public Position this[int index]
        {
            get 
            {
                return _deckCoords[index];
            }
            set 
            {
                _deckCoords[index] = value;
            }
        }

        public Ship GetShipCopy()
        {
            return (Ship)MemberwiseClone();
        }
    }
}
