using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using BattleshipLibrary;

namespace BattleshipApp
{
    public class ButtonMap : Button
    {
        private Position _coord;
        private MapCondition _cellCondition;

        public ButtonMap(int oX, int oY)
        {
            _coord.OX = oX;
            _coord.OY = oY;
        }

        public Position Coord
        {
            get
            {
                return _coord;
            }
            set
            {
                _coord = value;
            }
        }

        public MapCondition CellCondition 
        { 
            get
            {
                return _cellCondition;
            }
            set
            {
                _cellCondition = value;
            }
        }
    }
}
