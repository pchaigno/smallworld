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
        private static SquareFactory INSTANCE = new SquareFactory();

        /**
         * Constructor
         * It is private because the class is a singleton.
         */
        private SquareFactory() {
            this.mountain = new Mountain();
            this.lowland = new Lowland();
            this.forest = new Forest();
            this.sea = new Sea();
            this.desert = new Desert();
        }

        /**
         * @returns The instance of the square factory.
         */
        public static SquareFactory getInstance() {
            return INSTANCE;
        }

        /**
         * Gets the square corresponding to the number from the C++ library.
         * @param type The number from the C++ library.
         * @returns The square.
         */
        public ISquare getSquare(int type) {
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
                    // TODO throw Excetion?
            }
        }

        /**
         * Convert a matrix of squares to a matrix of integers with the corresponding numbers.
         * @param squares The map as a matrix of squares.
         * @returns The map as a matrix of integers.
         */
        public static int[][] getNumbers(ISquare[,] squares) {
            int[][] map = new int[squares.GetLength(0)][];
            for(int i=0; i<squares.GetLength(0); i++) {
                map[i] = new int[squares.GetLength(1)];
                for(int j=0; j<squares.GetLength(1); j++) {
                    map[i][j] = squares[i, j].getNumber();
                }
            }
            return map;
        }
    }
}