using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mWrapper;

namespace SmallWorld {

    public class MapBuilder: IMapBuilder {
        // TODO Should be a singleton.

        /**
         * Builds a map.
         * The composition of the map is computed by in the C++ library.
         * @param size The size of the map to build.
         * @returns The map.
         */
        public IMap BuildMap(int size) {
            int[][] composition = Wrapper.generateMapList(size);
            ISquare[,] squares = new ISquare[size, size];

            for(int x=0; x<size; x++) {
                for(int y=0; y<size; y++) {
                    squares[x, y] = SquareFactory.GetInstance().GetSquare(composition[x][y]);
                }
            }
            return new Map(squares);
        }
    }
}