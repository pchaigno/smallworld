using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {
    public class NormalGameBuilder: GameBuilder, INormalGameBuilder {
        public NormalGameBuilder() {
            max_rounds = 30;
            nb_units = 8;
            map_size = 15;
        }
    }
}
