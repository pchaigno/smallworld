using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {
    public class GauloisFactory: IGauloisFactory {
        public IUnit createUnit(IPlayer player) {
            return new Gaulois(player);
        }
    }
}
