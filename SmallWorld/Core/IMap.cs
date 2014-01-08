using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IMap {

        List<IUnit> GetUnits(IPoint coordinates);
        bool IsEnemyPosition(IPoint position, IUnit unit);
        void PlaceUnit(IUnit unit, IPoint position);
        ISquare GetSquare(IPoint position);
        ISquare[,] GetSquares();
        List<IUnit>[,] GetUnits();
        void MoveUnit(IUnit unit, IPoint currentPosition, IPoint newPosition);
        void RemoveUnit(IUnit unit, IPoint position);
        int GetSize();
        Dictionary<IUnit, IPoint> GetUnits(IPlayer player);
    }
}