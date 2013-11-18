using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public interface IRound
    {

        List<ICoordinates> getAvailableDestinations();

        void selectUnit(IUnit unit);

        void setDestination(ICoordinates destination);

        boolean unselectUnit(IUnit unit);

        List<IUnit> getUnits(ICoordinates position);
    }
}
