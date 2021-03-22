using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    [Serializable]
    public enum MapCondition
    {
        MissedShot = -1,
        NoneShot = 0,
        ShipSafe = 1,
        ShipInjured = 2,
        ShipDestroyed = 3
    }
}
