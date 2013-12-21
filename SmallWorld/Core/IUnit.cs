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

        void move(ISquare destination);

        Boolean canMove(Point currentPosition, Point destination, ISquare square);

        IPlayer getOwner();

        int getPoint(ISquare square);

        Boolean isAlive();
    }
}