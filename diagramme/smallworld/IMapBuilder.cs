using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallWorld;

namespace diagramme
{
    public interface IMapBuilder
    {
        IMap buildMap(int size);
    }
}
