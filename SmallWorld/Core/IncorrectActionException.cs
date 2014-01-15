using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld {

    class IncorrectActionException: Exception {

        /// <summary>
        /// Empty constructor
        /// </summary>
        public IncorrectActionException() {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg">Error message.</param>
        public IncorrectActionException(string msg):base(msg) {

        }
    }
}