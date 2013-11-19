using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public interface IUnit
    {

        int getDefense();

        int getLifePoints();

        int getAttack();

        int getRemainingMovementPoints();

        void resetMovementPoints();

        IPlayer getOwner();
    }
}
