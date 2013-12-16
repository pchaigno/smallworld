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
        private int points;

        public Player(string name, IUnitFactory factory)
        {
            this.name = name;
            this.factory = factory;
        }

        public string getName()
        {
            return name;
        }

        public int getPoints()
        {
            return points;
        }
    
        public List<IUnit> createUnits(int nbUnits)
        {
            List<IUnit> units = new List<IUnit>();
            for (int i = 0; i < nbUnits; i++)
            {
                units.Add(factory.createUnit(this));
            }
            return units;
        }

        public void addPoints(int n)
        {
            points += n;
        }
    }
}
