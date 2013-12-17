using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {
    public class VikingFactory: IVikingFactory {
        public IUnit createUnit(IPlayer player) {
            return new Viking(player);
        }
    }
}
