using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IMap {
        ITile[,] Tiles {
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
        ITile GetTile(IPoint position);
        void MoveUnit(IUnit unit, IPoint currentPosition, IPoint newPosition);
        bool RemoveUnit(IUnit unit, IPoint position);
        Dictionary<IUnit, IPoint> GetUnits(IPlayer player);
        Tuple<IUnit, IPoint> GetIdleUnit(IPlayer player);
    }
}