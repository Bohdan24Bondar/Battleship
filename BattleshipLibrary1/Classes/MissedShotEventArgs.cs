using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    [Serializable]
    public class MissedShotEventArgs
    {
        private Position _missedPosition;

        public MissedShotEventArgs(Position missedPosition )
        {
            _missedPosition = missedPosition;
        }

        public Position MissedPosition 
        {
            get
            {
                return _missedPosition;
            } 
        }

    }
}
