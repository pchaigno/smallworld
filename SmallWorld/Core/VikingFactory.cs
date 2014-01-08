using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmallWorld {

    public class VikingFactory: IVikingFactory {

        /// <summary>
        /// Creates a viking unit.
        /// </summary>
        /// <param name="player">The player owner of the unit.</param>
        /// <returns>The viking unit.</returns>
        public IUnit CreateUnit(IPlayer player) {
            return new Viking(player);
        }

        /// <summary>
        /// Used by the wrapper.
        /// </summary>
        /// <returns>The number corresponding to this factory/nation.</returns>
        public int GetNumber() {
            return 1;
        }
    }
}