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
        }
        int RemainingMovementPoints {
            get;
        }

        bool DecreaseLifePoints();
        void ResetMovementPoints();
        bool Move(ITile destination);
        bool CanMove(IPoint currentPosition, ITile currentTile, IPoint destination, ITile tile, bool occupied);
        int GetPoints(ITile tile, ITile[] neighbours);
        bool IsAlive();
    }
}