﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public interface CarteStrategie
    {
    }

    public interface Map
    {
        void attackSquare(Unit unit, Square square);
    }
}
