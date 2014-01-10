using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class SquareFactory: ISquareFactory {
        private IMountain mountain;
        private ILowland lowland;
        private IForest forest;
        private ISea sea;
        private IDesert desert;
        private static SquareFactory instance = new SquareFactory();
        public static SquareFactory Instance {
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
        private SquareFactory() {
            this.mountain = new Mountain();
            this.lowland = new Lowland();
            this.forest = new Forest();
            this.sea = new Sea();
            this.desert = new Desert();
        }

        /// <summary>
        /// Gets the square corresponding to the number from the C++ library.
        /// </summary>
        /// <param name="type">The number from the C++ library.</param>
        /// <returns>The square.</returns>
        public ISquare GetSquare(int type) {
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
                default:
                    return null;
                    // TODO Throw exception.
            }
        }

        /// <summary>
        /// Convert a matrix of squares to a matrix of integers with the corresponding numbers.
        /// </summary>
        /// <param name="squares">The map as a matrix of squares.</param>
        /// <returns>The map as a matrix of integers.</returns>
        public static int[][] GetNumbers(ISquare[,] squares) {
            int[][] map = new int[squares.GetLength(0)][];
            for(int i=0; i<squares.GetLength(0); i++) {
                map[i] = new int[squares.GetLength(1)];
                for(int j=0; j<squares.GetLength(1); j++) {
                    map[i][j] = squares[i, j].Number;
                }
            }
            return map;
        }
    }
}