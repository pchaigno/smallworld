using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mWrapper;
using System.Drawing;

namespace SmallWorld {

    public class MapBuilder: IMapBuilder {

        public IMap buildMap(int size) {
            int[][] squares = Wrapper.generateMapList(size);
            Dictionary<Point, ISquare> squaresDictionnary = new Dictionary<Point, ISquare>();

            for(int i=0; i<size; i++) {
                for(int j=0; j<size; j++) {
                    squaresDictionnary.Add(new Point(i, j), SquareFactory.INSTANCE.getSquare(squares[i][j]));
                }
            }
            return new Map(squaresDictionnary);
        }
    }
}