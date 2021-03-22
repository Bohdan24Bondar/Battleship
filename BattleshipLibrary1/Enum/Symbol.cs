using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    [Serializable]
    public enum Symbol
    {
        MissedShot = 0x2022,
        NoneShot = 0xFE4C,
        ShipSafe = 0x26F5,
        ShipInjured = 0x2716,
        DestroyedShip = 0x2620
    }
}
