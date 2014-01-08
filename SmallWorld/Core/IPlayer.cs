using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IPlayer {

        List<IUnit> CreateUnits(int nbUnits);
        String GetName();
        int GetPoints();
        void AddPoints(int n);
        int GetNumber();
        int GetNationNumber();
    }
}