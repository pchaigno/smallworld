using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {
    public class DwarfFactory: IDwarfFactory {
        public IUnit createUnit(IPlayer player) {
            return new Dwarf(player);
        }
    }
}
