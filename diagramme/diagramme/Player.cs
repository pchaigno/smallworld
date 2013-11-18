using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public class Player : IPlayer
    {
        public IUnitFactory factory;
    
        public List<IUnit> createUnits(int nbUnits)
        {
            throw new NotImplementedException();
        }

        public string getName()
        {
            throw new NotImplementedException();
        }
    }
}
