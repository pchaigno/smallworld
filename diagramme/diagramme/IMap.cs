using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public interface IMap
    {
        List<IUnit> getUnits(ICoordinates coordinates);

        bool isEnemyPosition(ICoordinates position, IUnit unit);

        void placeUnit(IUnit unit, ICoordinates position);

        ISquare getSquare(ICoordinates position);

        void moveUnit(IUnit unit, ICoordinates oldPosition, ICoordinates newPosition);
    }
}
