using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class SquareFactory : ISquareFactory
    {
        private IMountain mountain;

        private ILowland lowland;

        private IForest forest;

        private ISea sea;

        private IDesert desert;

        private static SquareFactory INSTANCE = new SquareFactory();

        private SquareFactory()
        {
            mountain = null;
            lowland = null;
            forest = null;
            sea = null;
            desert = null;
        }

        public static ISquareFactory getInstance()
        {
            return INSTANCE;
        }

        public ISquare getSquare(int type)
        {
            switch (type)
            {
                case 0:
                    if (sea == null)
                        sea = new Sea();
                    return sea;
                case 1:
                    if (forest == null)
                        forest = new Forest();
                    return forest;

                case 2:
                    if (desert == null)
                        desert = new Desert();
                    return desert;

                case 3:
                    if (lowland == null)
                        lowland = new Lowland();
                    return lowland;

                case 4:
                    if (mountain == null)
                        mountain = new Mountain();
                    return mountain;
                default:
                    return null;
                    //throw Excetion ???

            }
        }
    }
}
