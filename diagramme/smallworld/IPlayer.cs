using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface IPlayer
    {

        List<IUnit> createUnits(int nbUnits);

        String getName();

        int getNbUnits();

        int getPoints();

        void endRound();

        void terminateUnit(IUnit unit);
    }
}
