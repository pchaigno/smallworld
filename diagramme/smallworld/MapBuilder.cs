using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallWorld;
using System.Drawing;

namespace SmallWorld
{
    public class MapBuilder : IMapBuilder
    {
        public IMap buildMap(int size)
        {
            
            int[,] squares = new int[size,size];

            // Must be replaced by a call to wrapper
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    squares[i, j] = j;
                }
            }

            Dictionary<Point, ISquare> squaresDictionnary = new Dictionary<Point, ISquare>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    squaresDictionnary.Add(new Point(i, j), SquareFactory.INSTANCE.getSquare(squares[i,j]));
                }
            }


            IMap map = new Map(squaresDictionnary);
            map.setSize(size);


            return map;
        }
    }
}
