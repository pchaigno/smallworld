using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IUnitFactory {
        int Number {
            get;
        }

        IUnit CreateUnit(IPlayer player);
    }
}