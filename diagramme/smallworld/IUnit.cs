using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface IUnit
    {

        int getDefense();

        int getLifePoints();

        int getAttack();

        int getRemainingMovementPoints();

        void resetMovementPoints();

        void setPosition(Point pt, Dictionary<Point, ISquare> squares);

        void move(Point p);

        IPlayer getOwner();
    }
}
