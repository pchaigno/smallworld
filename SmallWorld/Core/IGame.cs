using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IGame {

        bool IsEndOfGame();
        IPlayer GetWinner();
        IPlayer GetPlayer1();
        IPlayer GetPlayer2();
        IMap GetMap();
        void EndRound();
        IRound GetRound();
        int GetCurrentRound();
        int GetMaxNbRound();
        IPlayer GetCurrentPlayer();
        int GetNbUnits(IPlayer p);
    }
}