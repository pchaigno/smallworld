﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public interface IRound
    {

        List<ICoordinates> getAdvisedDestinations(IUnit unit, ICoordinates position);

        void selectUnit(IUnit unit);

        boolean setDestination(ICoordinates destination);

        boolean unselectUnit(IUnit unit);

        List<IUnit> getUnits(ICoordinates position);
    }
}
