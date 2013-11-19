using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public interface IGame
    {

        boolean isDefeated(IPlayer player);

        boolean isEndOfGame();

        IPlayer getWinner();

        int computePoints();

        IPlayer getPlayer1();

        IPlayer getPlayer2();

        IMap getMap();
    }
}
