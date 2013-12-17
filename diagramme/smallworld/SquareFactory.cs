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

        public static SquareFactory INSTANCE = new SquareFactory();

        private SquareFactory() {
            mountain = new Mountain();
            lowland = new Lowland();
            forest = new Forest();
            sea = new Sea();
            desert = new Desert();
        }

        public ISquare getSquare(int type) {
            switch(type) {
                case 1:
                    return sea;
                case 2:
                    return forest;
                case 3:
                    return desert;
                case 4:
                    return lowland;
                case 5:
                    return mountain;
                default:
                    return null;
                // TODO throw Excetion ???
            }
        }
    }
}
