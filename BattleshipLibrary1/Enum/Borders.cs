using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    [Serializable]
    public enum Borders
    {
        RightTop = 0x2557,
        Vertical = 0x2551,
        RightDown = 0x255D,
        Horizontal = 0x2550,
        LeftDown = 0x255A,
        LeftTop = 0x2554
    }
}
