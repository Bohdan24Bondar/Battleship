using BattleshipLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipApp
{
    public class DirectionButton : Button
    {
        public Direction CurrentDirection { get; set; }

        public DirectionButton(Direction currentDirection)
        {
            CurrentDirection = currentDirection;
        }
    }
}
