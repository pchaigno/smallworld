using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IUnitFactory {
        
        IUnit createUnit(IPlayer player);
        int getNumber();
    }
}