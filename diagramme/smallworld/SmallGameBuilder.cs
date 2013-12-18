using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class SmallGameBuilder: GameBuilder, ISmallGameBuilder {

        /**
         * Constructor
         * Defines the default parameters for the game:
         * Number of rounds, size of the map and number of units.
         */
        // TODO Shouldn't default parameters by defined as constants?
        public SmallGameBuilder() {
            max_rounds = 20;
            nb_units = 6;
            map_size = 10;
        }
    }
}