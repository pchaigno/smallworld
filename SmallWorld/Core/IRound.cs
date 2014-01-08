using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IRound {
        List<IPoint> getAdvisedDestinations(IUnit unit, IPoint position);
        void selectUnits(List<IUnit> units);
        void selectUnits(List<IUnit> units, IPoint position);
        void unselectUnit();
        bool setDestination(IPoint destination);
        List<IUnit> getUnits(IPoint position);
        bool isCurrentPlayerPosition(IPoint position);
        void executeMove();
        String getLastMoveInfo();
    }
}