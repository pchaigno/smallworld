using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class VikingFactory: IVikingFactory {

        /**
         * Creates a viking unit.
         * @param player The player owner of the unit.
         * @returns The viking unit.
         */
        public IUnit createUnit(IPlayer player) {
            return new Viking(player);
        }

        /**
         * Used by the wrapper.
         * @returns The number corresponding to this factory/nation.
         */
        public int getNumber() {
            return 1;
        }
    }
}