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
        private int nbUnits;
        private int points;

        public Player(string name, IUnitFactory factory)
        {
            this.name = name;
            this.factory = factory;
        }
    
        public List<IUnit> createUnits(int nbUnits)
        {
            this.nbUnits = nbUnits;
            List<IUnit> units = new List<IUnit>();
            for (int i = 0; i < nbUnits; i++)
            {
                units.Add(factory.createUnit(this));
            }
            return units;
        }

        public string getName()
        {
            return name;
        }

        public int getNbUnits()
        {
            return nbUnits;
        }

        public int getPoints()
        {
            return points;
        }

        public void addPoints(int n)
        {
            this.addPoints += n;
        }
    }
}
