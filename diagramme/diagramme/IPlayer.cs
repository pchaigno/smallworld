using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public interface IPlayer
    {

        List<IUnit> createUnits(int nbUnits);

        String getName();
    }
}
