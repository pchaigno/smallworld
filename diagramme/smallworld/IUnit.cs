using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface IUnit
    {

        int attack { get; set; }
        int defense { get; set; }
        int lifePoints { get; set; }
        int maxLifePoints { get; set; }

        int getRemainingMovementPoints();

        void resetMovementPoints();

        void setPosition(Point pt, Dictionary<Point, ISquare> squares);

        void move(Point p);

        Boolean canMove(Point destination);

        IPlayer getOwner();

        int getPoint();

        void terminate();

        Point getPosition();

        Boolean isAlive();
    }
}
