using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class Map : IMap
    {
        Dictionary<ICoordinates, IUnit> units;
        Dictionary<ICoordinates, ISquare> squares;

        public Map(Dictionary<ICoordinates, ISquare> squares)
        {
            throw new System.NotImplementedException();
        }

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


        public ISquare getSquare(ICoordinates position)
        {
            throw new NotImplementedException();
        }


        public void moveUnit(IUnit unit, ICoordinates oldPosition, ICoordinates newPosition)
        {
            throw new NotImplementedException();
        }
    }
}
