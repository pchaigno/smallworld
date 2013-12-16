using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld
{
    public interface IRound
    {

        List<Point> getAdvisedDestinations(IUnit unit, Point position);

        void selectUnit(IUnit unit);

        bool setDestination(Point destination);

        List<IUnit> getUnits(Point position);

        Boolean isCurrentPlayerPosition(Point position);

        void executeMove();

        String getLastMoveInfo();
    }
}
