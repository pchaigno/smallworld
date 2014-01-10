using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmallWorld {

    public class DwarfFactory: IDwarfFactory {
        public int Number {
            get {
                return 3;
            }
        }

        /// <summary>
        /// Creates a dwarf unit.
        /// </summary>
        /// <param name="player">The player owner of the unit.</param>
        /// <returns>The dwarf unit.</returns>
        public IUnit CreateUnit(IPlayer player) {
            return new Dwarf(player);
        }
    }
}