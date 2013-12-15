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
        private int maxRounds;
        private int currentRound;

        public Game(IPlayer player1, IPlayer player2, IMap map, int max_rounds)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.map = map;
            this.maxRounds = max_rounds;
            this.currentRound = 1;
        }
    
        private bool isDefeated(IPlayer player)
        {
            return player.getNbUnits() == 0;
        }

        public bool isEndOfGame()
        {
            return isDefeated(player1) || isDefeated(player2) || currentRound > maxRounds ;
        }

        public IPlayer getWinner()
        {
            if (isDefeated(player1))
            {
                return player2;
            }
            else if (isDefeated(player2))
            {
                return player1;
            }
            else
            {
                if (player1.getPoints() < player2.getPoints())
                {
                    return player2;
                }
                else
                {
                    return player1; ;
                }
            }
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
