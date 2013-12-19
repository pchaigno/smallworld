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

        void setPosition(Point position);

        void move(Point p, ISquare square);

        Boolean canMove(Point destination, ISquare square);

        IPlayer getOwner();

        int getPoint(ISquare square);

        Point getPosition();

        Boolean isAlive();
    }
}