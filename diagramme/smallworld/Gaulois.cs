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
         * @returns The number of points won by the unit depending on the square she's on.
         */
        public override int getPoint() {
            if(squares[position] is ILowland) {
                return 2;
            } else if(squares[position] is ISea || squares[position] is IMountain) {
                return 0;
            } else {
                return 1;
            }
        }

        /**
         * Move the unit to its destination point and update the number of remaining points.
         * @param destination The destination for the unit.
         */
        // TODO Can't we use the super-method?
        public override void move(Point destination) {
            this.position = destination;
            if(squares[position] is ILowland) {
                remainingMovementPoints -= 1;
            } else {
                remainingMovementPoints -= 2;
            }
        }
    }
}