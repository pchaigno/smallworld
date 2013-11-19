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

        public Game(IPlayer player1, IPlayer player2)
        {
            throw new System.NotImplementedException();
        }
    
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

        public IPlayer getPlayer1()
        {
            throw new NotImplementedException();
        }

        public IPlayer getPlayer2()
        {
            throw new NotImplementedException();
        }
    }
}
