using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    public delegate bool BuildShip(TypeOfShips deckCount, Position startShipPosition, Direction shipDirection);
}
