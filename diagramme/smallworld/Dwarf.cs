using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class Dwarf : IDwarf
    {
        private int attack;
        private int defense;
        private int lifePoints;
        private IPlayer owner;
        private int remainingMovementPoints;
        private int movementPoints;

        public Dwarf(int defense, int attack, int lifePoints)
        {
            throw new System.NotImplementedException();
        }
    
        public int getDefense()
        {
            throw new NotImplementedException();
        }

        public int getLifePoints()
        {
            throw new NotImplementedException();
        }

        public int getAttack()
        {
            throw new NotImplementedException();
        }


        public int getRemainingMovementPoints()
        {
            throw new NotImplementedException();
        }

        public void resetMovementPoints()
        {
            throw new NotImplementedException();
        }


        public IPlayer getOwner()
        {
            throw new NotImplementedException();
        }
    }
}
