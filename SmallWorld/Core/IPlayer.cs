using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IPlayer {

        List<IUnit> createUnits(int nbUnits);
        String getName();
        int getPoints();
        void addPoints(int n);
        int getNumber();
        int getNationNumber();
    }
}