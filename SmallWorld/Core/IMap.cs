using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IMap {
        ISquare[,] Squares {
            get;
        }
        List<IUnit>[,] Units {
            get;
        }
        int Size {
            get;
        }

        List<IUnit> GetUnits(IPoint coordinates);
        bool IsEnemyPosition(IPoint position, IUnit unit);
        void PlaceUnit(IUnit unit, IPoint position);
        ISquare GetSquare(IPoint position);
        void MoveUnit(IUnit unit, IPoint currentPosition, IPoint newPosition);
        void RemoveUnit(IUnit unit, IPoint position);
        Dictionary<IUnit, IPoint> GetUnits(IPlayer player);
    }
}