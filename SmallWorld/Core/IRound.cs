using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IRound {

        List<IPoint> GetAdvisedDestinations(IUnit unit, IPoint position);
        void SelectUnits(List<IUnit> units);
        void SelectUnits(List<IUnit> units, IPoint position);
        void UnselectUnit();
        bool SetDestination(IPoint destination);
        List<IUnit> GetUnits(IPoint position);
        bool IsCurrentPlayerPosition(IPoint position);
        void ExecuteMove();
        String GetLastMoveInfo();
    }
}