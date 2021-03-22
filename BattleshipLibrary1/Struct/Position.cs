using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    [Serializable]
    public struct Position
    {
        private int _oY;
        private int _oX;

        public Position(int coordY, int coordX)
        {
            _oY = coordY;
            _oX = coordX;
        }

        public int OY 
        {
            get
            {
                return _oY;
            } 
            set
            {
                if (value < 0 || value > 9)
                {
                    return;
                }

                _oY = value;
            }
        }

        public int OX
        {
            get
            {
                return _oX;
            }
            set
            {
                if (value < 0 || value > 9)
                {
                    return;
                }

                _oX = value;
            }
        }
    }
}
