﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class Lowland: ILowland {

        /**
         * @returns The number corresponding to this square for the C++ library.
         */
        public int getNumber() {
            return 4;
        }
    }
}