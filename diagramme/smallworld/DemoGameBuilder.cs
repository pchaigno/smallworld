using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {
    public class DemoGameBuilder: GameBuilder, IDemoGameBuilder {
        public DemoGameBuilder() {
            max_rounds = 10;
            nb_units = 4;
            map_size = 5;
        }
    }
}
