using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IPlayer {
        string Name {
            get;
        }
        int Number {
            get;
        }
        int Points {
            get;
        }
        int NationNumber {
            get;
        }

        List<IUnit> CreateUnits(int nbUnits);
        void AddPoints(int n);
    }
}