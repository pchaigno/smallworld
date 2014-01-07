using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IMap {

        List<IUnit> getUnits(IPoint coordinates);
        bool isEnemyPosition(IPoint position, IUnit unit);
        void placeUnit(IUnit unit, IPoint position);
        ISquare getSquare(IPoint position);
        ISquare[,] getSquares();
        List<IUnit>[,] getUnits();
        void moveUnit(IUnit unit, IPoint currentPosition, IPoint newPosition);
        void removeUnit(IUnit unit, IPoint position);
        int getSize();
        // TODO Should use an interface IPoint.
        Dictionary<IUnit, IPoint> getUnits(IPlayer player);
    }
}