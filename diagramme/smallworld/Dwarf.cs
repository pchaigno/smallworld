using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace SmallWorld
{
    public class Dwarf : Unit
    {
        public Dwarf(IPlayer owner) : base(owner)
        {
        }

        public override int getPoint()
        {
            if (squares[position] is IForest)
                return 2;
            else if (squares[position] is ISea || squares[position] is ILowland)
                return 0;
            else
                return 1;
        }

        public override Boolean canMove(Point destination, ISquare destinationSquare)
        {
            return (remainingMovementPoints > 0) 
                && ((isNext(destination, position) && !(destinationSquare is ISea)) || (destinationSquare is IMountain));
        }

    }
}
