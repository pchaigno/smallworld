using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmallWorld {

    public class GauloisFactory: IGauloisFactory {

        /**
         * Creates a gaulois unit.
         * @param player The player owner of the unit.
         * @returns The gaulois unit.
         */
        public IUnit CreateUnit(IPlayer player) {
            return new Gaulois(player);
        }

        /**
         * Used by the wrapper.
         * @returns The number corresponding to this factory/nation.
         */
        public int GetNumber() {
            return 2;
        }
    }
}