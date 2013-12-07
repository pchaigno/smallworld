using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
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

        public bool setDestination(ICoordinates destination)
        {
            throw new NotImplementedException();
        }

        public bool unselectUnit(IUnit unit)
        {
            throw new NotImplementedException();
        }

        public List<IUnit> getUnits(ICoordinates position)
        {
            throw new NotImplementedException();
        }
    }
}
