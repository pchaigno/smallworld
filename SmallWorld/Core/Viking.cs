using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace SmallWorld {

    [Serializable()]
    public class Viking: Unit, IViking {

        public Viking(IPlayer owner): base(owner) {

        }

        public Viking(SerializationInfo info, StreamingContext context): base(info, context) {

        }

        /**
         * Vikings win one more point when they are next to the sea;
         * they don't win any if they are on the sea or in the desert.
         * @param square The type of square the unit is currently on.
         * @param neighbours The neighbour squares (array of 4 squares or null if out bounds).
         * @returns The number of points won by the unit depending on the square she's on.
         */
        // TODO See Unit: Is 'override' needed?
        public override int getPoints(ISquare square, ISquare[] neighbours) {
            int points = 1;
            if(square is ILowland) {
                points = 2;
            } else if(square is ISea || square is IDesert) {
                points = 0;
            }
            foreach(ISquare neighbour in neighbours) {
                if(neighbour is ISea) {
                    points++;
                    break;
                }
            }
            return points;
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
        public override bool canMove(IPoint currentPosition, ISquare currentSquare, IPoint destination, ISquare square) {
            return destination.isNext(currentPosition) 
                && remainingMovementPoints>0;
        }
    }
}