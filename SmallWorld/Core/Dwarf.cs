﻿using System;
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
         * @param position The type of square the unit is currently on.
         * @returns The number of points won by the unit depending on the square she's on.
         */
        public override int getPoint(ISquare square) {
            if(square is IForest) {
                return 2;
            } else if(square is ISea || square is ILowland) {
                return 0;
            } else {
                return 1;
            }
        }

        /**
         * Checks if the unit can move during this round to a certain destination.
         * Dwarfs have the particularity that they can't move on mountains.
         * @param currentPosition The current position.
         * @param destination The destination to reach.
         * @param square The type of square the destination is.
         * @returns True if the unit can move to the destination.
         */
        public override Boolean canMove(Point currentPosition, Point destination, ISquare square) {
            return base.canMove(currentPosition, destination, square)
                && !(square is IMountain);
        }
    }
}