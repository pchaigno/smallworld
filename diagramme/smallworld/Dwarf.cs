using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace SmallWorld {
    public class Dwarf: Unit, IDwarf {
        public Dwarf(IPlayer owner)
            : base(owner) {
        }

        public override int getPoint() {
            if(squares[position] is IForest)
                return 2;
            else if(squares[position] is ISea || squares[position] is ILowland)
                return 0;
            else
                return 1;
        }

        public override Boolean canMove(Point destination) {
            return (remainingMovementPoints > 0)
                && ((isNext(destination, position) && !(squares[destination] is ISea)) || (squares[destination] is IMountain));
        }

    }
}
