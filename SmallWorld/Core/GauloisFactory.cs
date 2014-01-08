﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmallWorld {

    public class GauloisFactory: IGauloisFactory {

        /// <summary>
        /// Creates a gaulois unit.
        /// </summary>
        /// <param name="player">The player owner of the unit.</param>
        /// <returns>The gaulois unit.</returns>
        public IUnit CreateUnit(IPlayer player) {
            return new Gaulois(player);
        }

        /// <summary>
        /// Used by the wrapper.
        /// </summary>
        /// <returns>The number corresponding to this factory/nation.</returns>
        public int GetNumber() {
            return 2;
        }
    }
}