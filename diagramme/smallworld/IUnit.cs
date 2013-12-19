using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IUnit {

        int getLifePoints();

        void decreaseLifePoints();

        int getDefaultLifePoints();

        int getAttack();

        int getDefense();

        int getRemainingMovementPoints();

        void resetMovementPoints();

        void setPosition(Point pt, Dictionary<Point, ISquare> squares);

        void move(Point p);

        Boolean canMove(Point destination);

        IPlayer getOwner();

        int getPoint();

        Point getPosition();

        Boolean isAlive();
    }
}