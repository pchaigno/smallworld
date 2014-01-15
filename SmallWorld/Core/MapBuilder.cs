using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mWrapper;

namespace SmallWorld {

    public class MapBuilder: IMapBuilder {
        private static IMapBuilder instance = new MapBuilder();
        public static IMapBuilder Instance {
            get {
                return instance;
            }
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        private MapBuilder() {

        }

        /// <summary>
        /// Builds a map.
        /// </summary>
        /// <remarks>
        /// The composition of the map is computed by in the C++ library.
        /// </remarks>
        /// <param name="size">The size of the map to build.</param>
        /// <returns>The map.</returns>
        public IMap BuildMap(int size) {
            // Retrieves the composition of the map from the wrapper
            // and converts it to a 2D array of tiles: 
            int[][] composition = Wrapper.generateMapList(size);
            ITile[,] tiles = new ITile[size, size];

            for(int x=0; x<size; x++) {
                for(int y=0; y<size; y++) {
                    tiles[x, y] = TileFactory.Instance.GetTile(composition[x][y]);
                }
            }
            return new Map(tiles);
        }
    }
}