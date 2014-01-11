using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IGameBuilder {

        IGame BuildGame(string name1, IUnitFactory factory1, string name2, IUnitFactory factory2);
    }
}