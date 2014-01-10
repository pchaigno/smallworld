using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmallWorld {

    public class VikingFactory: IVikingFactory {
        public int Number {
            get {
                return 1;
            }
        }

        /// <summary>
        /// Creates a viking unit.
        /// </summary>
        /// <param name="player">The player owner of the unit.</param>
        /// <returns>The viking unit.</returns>
        public IUnit CreateUnit(IPlayer player) {
            return new Viking(player);
        }
    }
}