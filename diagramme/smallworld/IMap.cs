using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace SmallWorld
{
    public interface IMap
    {
        List<IUnit> getUnits(Point coordinates);

        bool isEnemyPosition(Point position, IUnit unit);

        void placeUnit(IUnit unit, Point position);

        ISquare getSquare(Point position);

        Dictionary<Point, ISquare> getSquares();

        void moveUnit(IUnit unit, Point oldPosition, Point newPosition);

        int getSize();

        void setSize(int i);
    }
}
