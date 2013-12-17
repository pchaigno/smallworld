using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IGameBuilder {

        IGame buildGame(String name1, IUnitFactory factory1, String name2, IUnitFactory factory2);
    }
}