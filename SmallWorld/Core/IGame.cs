using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public interface IGame {
        IPlayer Player1 {
            get;
        }
        IPlayer Player2 {
            get;
        }
        IMap Map {
            get;
        }
        IRound Round {
            get;
        }
        int CurrentRound {
            get;
        }
        int MaxNbRound {
            get;
        }
        IPlayer CurrentPlayer {
            get;
        }

        bool IsEndOfGame();
        IPlayer GetWinner();
        void EndRound();
        int GetNbUnits(IPlayer p);
    }
}