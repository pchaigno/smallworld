using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld
{
    public class Round : IRound
    {
        public List<Point> getAdvisedDestinations(IUnit unit, Point position)
        {
            throw new NotImplementedException();
        }

        public void selectUnit(IUnit unit)
        {
            throw new NotImplementedException();
        }

        public bool setDestination(Point destination)
        {
            throw new NotImplementedException();
        }

        public bool unselectUnit(IUnit unit)
        {
            throw new NotImplementedException();
        }

        public List<IUnit> getUnits(Point position)
        {
            throw new NotImplementedException();
        }
    }
}
