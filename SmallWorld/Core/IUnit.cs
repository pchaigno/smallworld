using System;
using System.Collections.Generic;
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
        Boolean canMove(IPoint currentPosition, IPoint destination, ISquare square);
        IPlayer getOwner();
        int getPoints(ISquare square, ISquare[] neighbours);
        Boolean isAlive();
    }
}