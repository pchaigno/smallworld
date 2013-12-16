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
        private IPlayer currentPlayer;
        private IRound round;

        public Game(IPlayer player1, IPlayer player2, IMap map, int maxRounds)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.map = map;
            this.maxRounds = maxRounds;
            this.currentRound = 1;
            this.currentPlayer = player1;
            this.round = new Round(this, currentPlayer);
        }

        public int getNbUnits(IPlayer player)
        {

            return map.getUnits(player).Count;
        }
    
        private bool isDefeated(IPlayer player)
        {
            return getNbUnits(player) == 0;
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

        public void endRound()
        {
            List<IUnit> units = map.getUnits(currentPlayer);
            foreach (IUnit unit in units)
            {
                currentPlayer.addPoints(unit.getPoint());
                unit.resetMovementPoints();
            }
            if (currentPlayer == player1)
            {
                currentPlayer = player2;
            }
            else
            {
                currentPlayer = player1;
                currentRound++;
            }
            round = new Round(this, currentPlayer);
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

        public IRound getRound() 
        {
            return round;
        }

        public int getCurrentRound()
        {
            return currentRound;
        }

        public IPlayer getCurrentPlayer()
        {
            return currentPlayer;
        }
    }
}
