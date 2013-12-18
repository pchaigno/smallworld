using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class SmallGameBuilder: GameBuilder, ISmallGameBuilder {
        private const int MAXIMUM_ROUNDS = 20;
        private const int NUMBER_UNITS = 6;
        private const int MAP_SIZE = 10;

        /**
         * Constructor
         * Defines the default parameters for the game:
         * Number of rounds, size of the map and number of units.
         */
        public SmallGameBuilder() {
            maxRounds = MAXIMUM_ROUNDS;
            nbUnits = NUMBER_UNITS;
            mapSize = MAP_SIZE;
        }
    }
}