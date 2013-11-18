using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public class Map : IMap
    {
        Dictionary<ICoordinates, IUnit> units;
        Dictionary<ICoordinates, ISquare> squares;

        public List<IUnit> getUnits(ICoordinates coordinates)
        {
            throw new NotImplementedException();
        }

        public bool isEnemyPosition(ICoordinates position, IUnit unit)
        {
            throw new NotImplementedException();
        }

        public void placeUnit(IUnit unit, ICoordinates position)
        {
            throw new NotImplementedException();
        }
    }
}
