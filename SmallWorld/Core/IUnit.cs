using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IUnit {
        int LifePoints {
            get;
        }
        int DefaultLifePoints {
            get;
        }
        int Attack {
            get;
        }
        int Defense {
            get;
        }
        IPlayer Owner {
            get;
            set;
        }
        int RemainingMovementPoints {
            get;
        }

        void DecreaseLifePoints();
        void ResetMovementPoints();
        void Move(ISquare destination);
        bool CanMove(IPoint currentPosition, ISquare currentSquare, IPoint destination, ISquare square);
        int GetPoints(ISquare square, ISquare[] neighbours);
        bool IsAlive();
    }
}