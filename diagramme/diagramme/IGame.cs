using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public interface IGame
    {
        IPlayer player1;
        IPlayer player2;

        boolean isDefeated(IPlayer player);

        boolean isEndOfGame();

        IPlayer getWinner();

        int computePoints();
    }
}
