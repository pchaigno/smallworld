using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public class MapImpl : Map
    {
        private Map<Coordinates, Square> squares;

        private Map<diagramme.Coordinates, IEnumerable<Unit>> units;
    }
}
