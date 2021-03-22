using BattleshipLibrary;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    [Serializable]
    public class AdvancedIntelligence : Intelligence
    {
        private BitShipType _playerShips;
        private Queue<Position> _plentyShots;

        public AdvancedIntelligence(int coordY, int coordX)
           : base(coordY, coordX)
        {
            _playerShips = (BitShipType)Constants.COUNT_SHIPS_TYPE;
            _plentyShots = RandomCoords.BuildShootsLine();
        }

        public BitShipType PlayerShips 
        {
            get
            {
                return _playerShips;
            }
            set
            {
                _playerShips = value;
            } 
        }

        public override void MakeTheShot(ref bool isAlivePlayerAfterShoot, Sea playerMap)
        {
            bool wasShot = false;

            if (!isAlivePlayerAfterShoot)
            {
                do
                {
                    if (_plentyShots.Count <= 0)
                    {
                        RandomCoords.SearchRandomCoords(playerMap);
                        //break;//
                    }
                    else
                    {
                        Position currentPos = _plentyShots.Dequeue();

                        playerMap.TargetCoordX = currentPos.OX;
                        playerMap.TargetCoordY = currentPos.OY;

                        wasShot = playerMap.WasShot();
                    }

                    
                } while (wasShot);


                _isTargetPlayer = playerMap.HitTarget(ref isAlivePlayerAfterShoot);

                if (_isTargetPlayer)
                {
                    SaveCoordsSuccessfulTarget(playerMap);
                }
            }
            else
            {
                GetTargetCoords(playerMap);

                _isTargetPlayer = playerMap.HitTarget(ref isAlivePlayerAfterShoot);
            }

        }

    }
}
