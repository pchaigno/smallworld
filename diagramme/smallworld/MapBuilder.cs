using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld
{
    public class MapBuilder : IMapBuilder
    {
        public IMap buildMap(int size)
        {
            /*
            int[][] squares = Wrapper.generateMapList(size);
            Console.Write("it works!");
            */
            Dictionary<Point, ISquare> squaresDictionnary = new Dictionary<Point, ISquare>();

            Random rnd = new Random();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    squaresDictionnary.Add(new Point(i, j), SquareFactory.INSTANCE.getSquare(rnd.Next(1, 5)));
                }
            }

            IMap map = new Map(squaresDictionnary);
            map.setSize(size);

            return map;
        }
    }
}
