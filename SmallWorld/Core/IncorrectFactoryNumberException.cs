using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld {

    class IncorrectFactoryNumberException: Exception {

        /// <summary>
        /// Empty constructor
        /// </summary>
        public IncorrectFactoryNumberException() {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="number">The incorrect number given.</param>
        public IncorrectFactoryNumberException(int number)
            : base(number+" is not a number representing a factory.") {

        }
    }
}