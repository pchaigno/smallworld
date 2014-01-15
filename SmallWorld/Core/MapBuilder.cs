﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mWrapper;

namespace SmallWorld {

    public class MapBuilder: IMapBuilder {
        // TODO Should be a singleton.

        /// <summary>
        /// Builds a map.
        /// </summary>
        /// <remarks>
        /// The composition of the map is computed by in the C++ library.
        /// </remarks>
        /// <param name="size">The size of the map to build.</param>
        /// <returns>The map.</returns>
        public IMap BuildMap(int size) {
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