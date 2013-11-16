﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public interface IMap
    {
        Dictionary<ICoordinates, IUnit> units;
        Dictionary<ICoordinates, ISquare> squares;
    
        List<IUnit> getUnits(ICoordinates coordinates);

        bool isEnemyPosition(ICoordinates position, IUnit unit);

        void placeUnit(IUnit unit, ICoordinates position);
    }
}
