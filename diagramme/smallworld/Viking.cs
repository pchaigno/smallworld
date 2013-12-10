using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace SmallWorld
{
    public class Viking : Unit, IViking
    {
        public int getPoint()
        {
            if (square is ILowland) // TODO: only near sea
                return 2;
            else if (square is ISea || square is IDesert)
                return 0;
            else
                return 1;
        }

        public Boolean canMove(Point destination, ISquare destinationSquare)
        {
            return isNext(destination, position) && remainingMovementPoints > 0;
        }
    }
}
