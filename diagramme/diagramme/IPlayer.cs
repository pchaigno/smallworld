using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public interface IPlayer
    {
        IUnitFactory factory;

        List<IUnit> createUnits(int nbUnits);

        void addPoints(int points);

        String getName();
    }
}
