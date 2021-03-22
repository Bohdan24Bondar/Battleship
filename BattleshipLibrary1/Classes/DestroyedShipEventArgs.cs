using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    [Serializable]
    public class DestroyedShipEventArgs
    {
        private Ship _destroyedShip;

        public DestroyedShipEventArgs(Ship destroyedShip)
        {
            _destroyedShip = destroyedShip.GetShipCopy();
        }

        public Ship DestroyedShip
        {
            get
            {
                return _destroyedShip;
            }
        }
    }
}
