using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld {

    public class Dwarf: Unit, IDwarf {

        public Dwarf(IPlayer owner): base(owner) {

        }

        /**
         * Dwarfs win twice the points when they are on a forest;
         * they don't win any if they are on a sea or on a lowland square.
         * @returns The number of points won by the unit depending on the square she's on.
         */
        public override int getPoint() {
            if(this.squares[position] is IForest) {
                return 2;
            } else if(this.squares[position] is ISea || this.squares[position] is ILowland) {
                return 0;
            } else {
                return 1;
            }
        }

        /**
         * Checks if the unit can move during this round to a certain destination.
         * Dwarfs have the particularity that they can't move on mountains.
         * @param destination The destination to reach.
         * @returns True if the unit can move to the destination.
         */
        public override Boolean canMove(Point destination) {
            return base.canMove(destination)
                && !(this.squares[destination] is IMountain);
        }
    }
}