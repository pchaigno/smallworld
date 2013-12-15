using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface IGame
    {

        bool isEndOfGame();

        IPlayer getWinner();

        IPlayer getPlayer1();

        IPlayer getPlayer2();

        IMap getMap();
    }
}
