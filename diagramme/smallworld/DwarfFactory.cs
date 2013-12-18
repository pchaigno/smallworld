﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class DwarfFactory: IDwarfFactory {

        /**
         * Creates a dwarf unit.
         * @param player The player owner of the unit.
         * @returns The dwarf unit.
         */
        public IUnit createUnit(IPlayer player) {
            return new Dwarf(player);
        }
    }
}