using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmallWorld {

    public class GauloisFactory: IGauloisFactory {
        public int Number {
            get {
                return 2;   
            }
        }

        /// <summary>
        /// Creates a gaulois unit.
        /// </summary>
        /// <param name="player">The player owner of the unit.</param>
        /// <returns>The gaulois unit.</returns>
        public IUnit CreateUnit(IPlayer player) {
            return new Gaulois(player);
        }
    }
}