using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class NormalGameBuilder: GameBuilder, INormalGameBuilder {

        /**
         * Constructor
         * Defines the default parameters for the game:
         * Number of rounds, size of the map and number of units.
         */
        // TODO Shouldn't default parameters by defined as constants?
        public NormalGameBuilder() {
            max_rounds = 30;
            nb_units = 8;
            map_size = 15;
        }
    }
}