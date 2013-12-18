using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld {

    public class Viking: Unit, IViking {

        public Viking(IPlayer owner): base(owner) {

        }

        /**
         * Vikings win twice the points when they are on lowland;
         * they don't win any if they are on the sea or in the desert.
         * @returns The number of points won by the unit depending on the square she's on.
         */
        public override int getPoint() {
            if(squares[position] is ILowland) {
                // TODO: only near sea
                return 2;
            } else if(squares[position] is ISea || squares[position] is IDesert) {
                return 0;
            } else {
                return 1;
            }
        }

        /**
         * Checks if the unit can move during this round to a certain destination.
         * The destination must be next to the current position,
         * the unit must have some movement points left.
         * Contrary to must units, viking can move on the sea.
         * @param destination The destination to reach.
         * @returns True if the unit can move to the destination.
         */
        public override bool canMove(Point destination) {
            return isNext(destination, position) 
                && remainingMovementPoints>0;
        }
    }
}