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

        private Dictionary<Point, IUnit> units;
        private Dictionary<Point, ISquare> squares;

        private int size;
        
        public int getSize()
        {
            return size;
        }

        public void setSize(int i)
        {
            this.size = i;
        }


        public Map(Dictionary<Point, ISquare> squares)
        {
            this.squares = squares;
        }

        public List<IUnit> getUnits(Point coordinates)
        {
            throw new NotImplementedException();
        }

        public bool isEnemyPosition(Point position, IUnit unit)
        {
            throw new NotImplementedException();
        }

        public void placeUnit(IUnit unit, Point position)
        {
            throw new NotImplementedException();
        }


        public ISquare getSquare(Point position)
        {
            return squares[position];
        }


        public void moveUnit(IUnit unit, Point oldPosition, Point newPosition)
        {
            throw new NotImplementedException();
        }
    }
}
