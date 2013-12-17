using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace SmallWorld {

    public class Gaulois: Unit, IGaulois {

        public Gaulois(IPlayer owner)
            : base(owner) {
        }

        public override int getPoint() {
            if(squares[position] is ILowland) {
                return 2;
            } else if(squares[position] is ISea || squares[position] is IMountain) {
                return 0;
            } else {
                return 1;
            }
        }

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