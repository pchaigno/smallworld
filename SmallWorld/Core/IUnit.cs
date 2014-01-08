using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IUnit {

        int GetLifePoints();
        void DecreaseLifePoints();
        int GetDefaultLifePoints();
        int GetAttack();
        int GetDefense();
        int GetRemainingMovementPoints();
        void ResetMovementPoints();
        void Move(ISquare destination);
        bool CanMove(IPoint currentPosition, ISquare currentSquare, IPoint destination, ISquare square);
        IPlayer GetOwner();
        void SetOwner(IPlayer owner);
        int GetPoints(ISquare square, ISquare[] neighbours);
        bool IsAlive();
    }
}