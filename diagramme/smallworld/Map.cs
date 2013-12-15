using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Drawing;


namespace SmallWorld
{
    public class Map : IMap
    {

        private Dictionary<Point, List<IUnit>> units;
        private Dictionary<Point, ISquare> squares;
        private int size;

        public Map(Dictionary<Point, ISquare> squares)
        {
            this.squares = squares;
            foreach (Point key in squares.Keys)
            {
                units.Add(key, new List<IUnit>());
            }

        }
        
        public int getSize()
        {
            return size;
        }

        public Dictionary<Point, ISquare> getSquares()
        {
            return squares;
        }

        public void setSize(int i)
        {
            this.size = i;
        }

        public List<IUnit> getUnits(Point position)
        {
            return units[position];
        }

        public bool isEnemyPosition(Point position, IUnit unit)
        {
            if (units[position].Count == 0)
            {
                return false;
            }
            else
            {
                return !units[position][0].getOwner().Equals(unit.getOwner()); 
            }
        }

        public void placeUnit(IUnit unit, Point position)
        {
            units[position].Add(unit);
        }


        public ISquare getSquare(Point position)
        {
            return squares[position];
        }


        public void moveUnit(IUnit unit, Point oldPosition, Point newPosition)
        {
            units[oldPosition].Remove(unit);
            if (this.isEnemyPosition(newPosition, unit))
                throw new Exception("Erreur dans le deplacement");
            units[newPosition].Add(unit);
            unit.move(newPosition);
        }
    }
}
