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
        bool canMove(IPoint currentPosition, ISquare currentSquare, IPoint destination, ISquare square);
        IPlayer getOwner();
        void setOwner(IPlayer owner);
        int getPoints(ISquare square, ISquare[] neighbours);
        bool isAlive();
    }
}