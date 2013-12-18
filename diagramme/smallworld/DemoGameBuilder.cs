using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class DemoGameBuilder: GameBuilder, IDemoGameBuilder {

        /**
         * Constructor
         * Defines the default parameters for the game:
         * Number of rounds, size of the map and number of units.
         */
        // TODO Shouldn't default parameters by defined as constants?
        public DemoGameBuilder() {
            max_rounds = 10;
            nb_units = 4;
            map_size = 5;
        }
    }
}
