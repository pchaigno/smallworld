using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public interface ISquareFactory
    {
        ISea sea;
        IDesert desert;
        IMoutain mountain;
        ILowland lowland;
        IForest forest;
    }
}
