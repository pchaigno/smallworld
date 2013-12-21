using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class GauloisFactory: IGauloisFactory {

        /**
         * Creates a gaulois unit.
         * @param player The player owner of the unit.
         * @returns The gaulois unit.
         */
        public IUnit createUnit(IPlayer player) {
            return new Gaulois(player);
        }
    }
}