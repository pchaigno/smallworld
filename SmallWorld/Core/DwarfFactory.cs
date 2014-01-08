using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmallWorld {

    public class DwarfFactory: IDwarfFactory {

        /// <summary>
        /// Creates a dwarf unit.
        /// </summary>
        /// <param name="player">The player owner of the unit.</param>
        /// <returns>The dwarf unit.</returns>
        public IUnit CreateUnit(IPlayer player) {
            return new Dwarf(player);
        }

        /// <summary>
        /// Used by the wrapper.
        /// </summary>
        /// <returns>The number corresponding to this factory/nation.</returns>
        public int GetNumber() {
            return 3;
        }
    }
}