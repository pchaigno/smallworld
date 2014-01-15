using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class TileFactory: ITileFactory {
        private IMountain mountain;
        private ILowland lowland;
        private IForest forest;
        private ISea sea;
        private IDesert desert;
        private static ITileFactory instance = new TileFactory();
        public static ITileFactory Instance {
            get {
                return instance;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// It is private because the class is a singleton.
        /// </remarks>
        private TileFactory() {
            this.mountain = new Mountain();
            this.lowland = new Lowland();
            this.forest = new Forest();
            this.sea = new Sea();
            this.desert = new Desert();
        }

        /// <summary>
        /// Gets the tile corresponding to the number from the C++ library.
        /// </summary>
        /// <param name="type">The number from the C++ library.</param>
        /// <returns>The tile.</returns>
        public ITile GetTile(int type) {
            switch(type) {
                case 1:
                    return this.sea;
                case 2:
                    return this.forest;
                case 3:
                    return this.desert;
                case 4:
                    return this.lowland;
                case 5:
                    return this.mountain;
            }
            throw new IncorrectTileNumberException(type);
        }

        /// <summary>
        /// Converts a matrix of tiles to a matrix of integers with the corresponding numbers.
        /// </summary>
        /// <param name="tiles">The map as a matrix of tiles.</param>
        /// <returns>The map as a matrix of integers.</returns>
        public static int[][] GetNumbers(ITile[,] tiles) {
            int[][] map = new int[tiles.GetLength(0)][];
            for(int i = 0; i < tiles.GetLength(0); i++) {
                map[i] = new int[tiles.GetLength(1)];
                for(int j = 0; j < tiles.GetLength(1); j++) {
                    map[i][j] = tiles[i, j].Number;
                }
            }
            return map;
        }
    }
}