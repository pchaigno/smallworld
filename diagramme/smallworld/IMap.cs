using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace SmallWorld {
    public interface IMap {
        List<IUnit> getUnits(Point coordinates);

        bool isEnemyPosition(Point position, IUnit unit);

        void placeUnit(IUnit unit, Point position);

        ISquare getSquare(Point position);

        Dictionary<Point, ISquare> getSquares();

        Dictionary<Point, List<IUnit>> getUnits();

        void moveUnit(IUnit unit, Point newPosition);

        void removeUnit(IUnit unit, Point position);

        int getSize();

        List<IUnit> getUnits(IPlayer player);
    }
}
