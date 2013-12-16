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

        void endRound();

        IRound getRound();

        int getCurrentRound();

        IPlayer getCurrentPlayer();

        int getNbUnits(IPlayer p);
    }
}
