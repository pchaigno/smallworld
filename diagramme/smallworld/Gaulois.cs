using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace SmallWorld
{
    public class Gaulois : Unit, IGaulois
    {
        public Gaulois(IPlayer owner) : base(owner)
        {
        }
        
        public override int getPoint()
        {
            if (square is ILowland)
                return 2;
            else if (square is ISea || square is IMountain)
                return 0;
            else
                return 1;
        }

        public override void move(ISquare destinationSquare, Point destination)
        {
            this.square = destinationSquare;
            this.position = destination;
            if(destinationSquare is ILowland)
                remainingMovementPoints -= 1;
            else
                remainingMovementPoints -= 2;
        }

        
    }
}
