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
        private List<IUnit> units;

        public Player(string name, IUnitFactory factory)
        {
            this.name = name;
            this.factory = factory;
        }
    
        public List<IUnit> createUnits(int nbUnits)
        {
            List<IUnit> units = new List<IUnit>();
            for (int i = 0; i < nbUnits; i++)
            {
                units.Add(factory.createUnit(this));
            }
            this.units = units;
            return units;
        }

        public string getName()
        {
            return name;
        }

        public int getNbUnits()
        {
            return units.Count;
        }

        public int getPoints()
        {
            return points;
        }

        public void computePoints()
        {
            foreach (IUnit unit in units)
            {
                points += unit.getPoint();
            }
        }

        public void terminateUnit(IUnit unit)
        {
            units.Remove(unit);
        }
    }
}
