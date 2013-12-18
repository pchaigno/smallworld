using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class DemoGameBuilder: GameBuilder, IDemoGameBuilder {
        private const int MAXIMUM_ROUNDS = 10;
        private const int NUMBER_UNITS = 4;
        private const int MAP_SIZE = 5;

        /**
         * Constructor
         * Defines the default parameters for the game:
         * Number of rounds, size of the map and number of units.
         */
        public DemoGameBuilder() {
            maxRounds = MAXIMUM_ROUNDS;
            nbUnits = NUMBER_UNITS;
            mapSize = MAP_SIZE;
        }
    }
}
