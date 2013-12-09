using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface IGameBuilder
    {
        IGame buildGame(IPlayer p1, IPlayer p2);
    }
}
