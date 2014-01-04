using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class Gaulois: Unit, IGaulois {

        public Gaulois(IPlayer owner): base(owner) {

        }

        /**
         * Gaulois win twice the points if they are on lowland;
         * they don't win any if they are on a mountain or on the sea.
         * @param position The type of square the unit is currently on.
         * @param neighbours The neighbour squares (array of 4 squares or null if out bounds).
         * @returns The number of points won by the unit depending on the square she's on.
         */
        public override int getPoints(ISquare square, ISquare[] neighbours) {
            if(square is ILowland) {
                return 2;
            } else if(square is ISea || square is IMountain) {
                return 0;
            } else {
                return 1;
            }
        }

        /**
         * Move the unit to its destination point and update the number of remaining points.
         * Gaulois use one point less than others to move on lowland.
         * @param destination The type of square the destination is.
         */
        public override void move(ISquare destination) {
            base.move(destination);
            if(destination is ILowland) {
                this.remainingMovementPoints++;
            }
        }
    }
}