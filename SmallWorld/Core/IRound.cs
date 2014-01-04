using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IRound {

        List<Point> getAdvisedDestinations(IUnit unit, Point position);
        void selectUnit(IUnit unit);
        void selectUnit(IUnit unit, Point position);
        void unselectUnit();
        bool setDestination(Point destination);
        List<IUnit> getUnits(Point position);
        Boolean isCurrentPlayerPosition(Point position);
        void executeMove();
        String getLastMoveInfo();
    }
}