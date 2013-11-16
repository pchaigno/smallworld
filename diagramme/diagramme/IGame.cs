using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public interface IGame
    {
        IPlayer player1;
        IPlayer player2;
    
        List<IUnit> getUnits(ICoordinates coordinates);

        void setCurrentUnit(IUnit unit);

        List<ICoordinates> getAvailableDestinations();

        void setDestination(ICoordinates destination);
    }
}
