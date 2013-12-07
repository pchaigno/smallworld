using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class Player : IPlayer
    {
        public IUnitFactory factory;

        public Player(string name)
        {
            throw new System.NotImplementedException();
        }
    
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
