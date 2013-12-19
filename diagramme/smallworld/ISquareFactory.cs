﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    // TODO Why is there no public/private in interfaces?
    public interface ISquareFactory {

        SquareFactory getInstance();

        ISquare getSquare(int i);
    }
}
