using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mWrapper;
using System.Drawing;

namespace SmallWorld {

    public class MapBuilder: IMapBuilder {

        /**
         * Builds a map.
         * The composition of the map is computed by in the C++ library.
         * @param size The size of the map to build.
         * @returns The map.
         */
        public IMap buildMap(int size) {
            int[][] squares = Wrapper.generateMapList(size);
            Dictionary<Point, ISquare> squaresDictionnary = new Dictionary<Point, ISquare>();

            for(int i=0; i<size; i++) {
                for(int j=0; j<size; j++) {
                    squaresDictionnary.Add(new Point(i, j), SquareFactory.getInstance().getSquare(squares[i][j]));
                }
            }
            return new Map(squaresDictionnary);
        }
    }
}