using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public class Game : IGame
    {
        public IPlayer player2;

        public IPlayer player1;
    
        public bool isDefeated(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public bool isEndOfGame()
        {
            throw new NotImplementedException();
        }

        public IPlayer getWinner()
        {
            throw new NotImplementedException();
        }

        public int computePoints()
        {
            throw new NotImplementedException();
        }
    }
}
