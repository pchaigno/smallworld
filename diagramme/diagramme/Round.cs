using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public class Round : IRound
    {
        public List<ICoordinates> getAdvisedDestinations(IUnit unit, ICoordinates position)
        {
            throw new NotImplementedException();
        }

        public void selectUnit(IUnit unit)
        {
            throw new NotImplementedException();
        }

        public boolean setDestination(ICoordinates destination)
        {
            throw new NotImplementedException();
        }

        public boolean unselectUnit(IUnit unit)
        {
            throw new NotImplementedException();
        }

        public List<IUnit> getUnits(ICoordinates position)
        {
            throw new NotImplementedException();
        }
    }
}
