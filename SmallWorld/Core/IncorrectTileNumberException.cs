using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld {

    class IncorrectTileNumberException: Exception {

        /// <summary>
        /// Empty constructor
        /// </summary>
        public IncorrectTileNumberException() {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="number">The incorrect number given.</param>
        public IncorrectTileNumberException(int number)
            : base(number+" is not a number representing a tile.") {

        }
    }
}