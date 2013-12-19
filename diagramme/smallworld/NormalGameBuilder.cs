using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class NormalGameBuilder: GameBuilder, INormalGameBuilder {
        private const int MAXIMUM_ROUNDS = 30;
        private const int NUMBER_UNITS = 8;
        private const int MAP_SIZE = 15;

        /**
         * Constructor
         * Defines the default parameters for the game:
         * Number of rounds, size of the map and number of units.
         */
        public NormalGameBuilder() {
            this.maxRounds = MAXIMUM_ROUNDS;
            this.nbUnits = NUMBER_UNITS;
            this.mapSize = MAP_SIZE;
        }
    }
}