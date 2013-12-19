using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace SmallWorld {

    public class Gaulois: Unit, IGaulois {

        public Gaulois(IPlayer owner): base(owner) {

        }

        /**
         * Gaulois win twice the points if they are on lowland;
         * they don't win any if they are on a mountain or on the sea.
         * @param position The type of square the unit is currently on.
         * @returns The number of points won by the unit depending on the square she's on.
         */
        public override int getPoint(ISquare square) {
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
         * @param destination The destination for the unit.
         * @param square The type of square the destination is.
         */
        public override void move(Point destination, ISquare square) {
            base.move(destination, square);
            if(square is ILowland) {
                this.remainingMovementPoints++;
            }
        }
    }
}