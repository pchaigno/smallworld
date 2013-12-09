using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class Player : IPlayer
    {
        private IUnitFactory factory;
        private string name;

        public Player(string name, IUnitFactory factory)
        {
            this.name = name;
            this.factory = factory;
        }
    
        public List<IUnit> createUnits(int nbUnits)
        {
            throw new NotImplementedException();
        }

        public string getName()
        {
            return name;
        }
    }
}
