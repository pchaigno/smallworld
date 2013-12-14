using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class Game : IGame
    {
        private IPlayer player2;
        private IPlayer player1;
        private IMap map;
        private int max_rounds;

        public Game(IPlayer player1, IPlayer player2, IMap map, int max_rounds)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.map = map;
            this.max_rounds = max_rounds;
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
            return player1;
        }

        public IPlayer getPlayer2()
        {
            return player2;
        }

        public IMap getMap()
        {
            return map;
        }
    }
}
