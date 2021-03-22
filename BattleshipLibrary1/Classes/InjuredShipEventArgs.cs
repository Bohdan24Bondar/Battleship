using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    [Serializable]
    public class InjuredShipEventArgs
    {
        private Position _injuredCell;

        public InjuredShipEventArgs(Position injuredCell)
        {
            _injuredCell = injuredCell;
        }

        public Position InjuredCell 
        {
            get
            {
                return _injuredCell;
            }
            set
            {
                _injuredCell = value;
            }
        }
    }
}
