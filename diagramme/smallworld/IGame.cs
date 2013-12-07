using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface IGame
    {

        bool isDefeated(IPlayer player);

        bool isEndOfGame();

        IPlayer getWinner();

        int computePoints();

        IPlayer getPlayer1();

        IPlayer getPlayer2();

        IMap getMap();
    }
}
