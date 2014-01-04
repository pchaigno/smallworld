using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SmallWorld {

    public class Viking: Unit, IViking {

        public Viking(IPlayer owner): base(owner) {

        }

        /**
         * Vikings win twice the points when they are on lowland;
         * they don't win any if they are on the sea or in the desert.
         * @param position The type of square the unit is currently on.
         * @returns The number of points won by the unit depending on the square she's on.
         */
        // TODO See Unit: Is 'override' needed?
        public override int getPoint(ISquare square) {
            if(square is ILowland) {
                // TODO Near sea instead of lowlands.
                return 2;
            } else if(square is ISea || square is IDesert) {
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
         * @param currentPosition The current position.
         * @param destination The destination to reach.
         * @param square The type of square the destination is.
         * @returns True if the unit can move to the destination.
         */
        public override bool canMove(Point currentPosition, Point destination, ISquare square) {
            return destination.isNext(currentPosition) 
                && remainingMovementPoints>0;
        }
    }
}