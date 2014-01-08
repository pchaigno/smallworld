using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class Game: IGame {
        private IPlayer player2;
        private IPlayer player1;
        private IMap map;
        private int maxRounds;
        private int currentRound;
        private IPlayer currentPlayer;
        private IRound round;

        /**
         * Constructor
         * @param player1 The first player.
         * @param player2 The second player.
         * @param map The map for this game.
         * @param maxRounds The maximum number of rounds that can be played.
         */
        public Game(IPlayer player1, IPlayer player2, IMap map, int maxRounds) {
            this.player1 = player1;
            this.player2 = player2;
            this.map = map;
            this.maxRounds = maxRounds;
            this.currentRound = 1;
            this.currentPlayer = player1;
            this.round = new Round(this, currentPlayer);
        }

        /**
         * @param player A player.
         * @returns The number of units owned by the player.
         */
        public int GetNbUnits(IPlayer player) {
            return this.map.GetUnits(player).Count;
        }

        /**
         * Checks if a player lost.
         * A player lose if he doesn't have any more units.
         * @param player The player.
         * @returns True if the player lost.
         */
        private bool IsDefeated(IPlayer player) {
            return this.GetNbUnits(player) == 0;
        }

        /**
         * Checks if the game is ended.
         * A game is ended if a player won or if they reached the maximum number of rounds.
         * @returns True if the game is ended.
         */
        public bool IsEndOfGame() {
            return this.IsDefeated(this.player1) 
                || this.IsDefeated(this.player2) 
                || this.currentRound > this.maxRounds;
        }

        /**
         * @returns The winner, null if there is none.
         */
        public IPlayer GetWinner() {
            if(this.IsDefeated(this.player1)) {
                return this.player2;
            } else if(this.IsDefeated(this.player2)) {
                return this.player1;
            } else {
                if(this.player1.GetPoints() < this.player2.GetPoints()) {
                    return this.player2;
                } else if(this.player1.GetPoints() > this.player2.GetPoints()) {
                    return this.player1;
                }
            }
            return null;
        }

        /**
         * End the current round and start the next one.
         */
        public void EndRound() {
            Dictionary<IUnit, IPoint> units = map.GetUnits(this.currentPlayer);
            foreach(IUnit unit in units.Keys) {
                ISquare square = this.map.GetSquare(units[unit]);
                ISquare[] neighbours = this.GetNeighbours(units[unit]);
                this.currentPlayer.AddPoints(unit.GetPoints(square, neighbours));
                unit.ResetMovementPoints();
            }
            if(this.currentPlayer == this.player1) {
                this.currentPlayer = this.player2;
            } else {
                this.currentPlayer = player1;
                this.currentRound++;
            }
            this.round = new Round(this, this.currentPlayer);
        }

        /**
         * Retrieve the neighbours' types of a specific position.
         * @param pos The position.
         * @returns An array of neighbours' types, or null if a neighbour was out bounds.
         */
        private ISquare[] GetNeighbours(IPoint pos) {
            int[] xOffset = {0, -1, 0, 1};
            int[] yOffset = {0, -1, 0, 1};
            ISquare[] neighbours = new ISquare[4];
            for(int i=0; i<4; i++) {
                int x = pos.X + xOffset[i];
                int y = pos.Y + yOffset[i];
                if(x >= 0 && y >= 0 && x < this.map.GetSize() && y < this.map.GetSize()) {
                    neighbours[i] = this.map.GetSquare(new Point(x, y));
                } else {
                    neighbours[i] = null;
                }
            }
            return neighbours;
        }

        /**
         * @returns The first player.
         */
        public IPlayer GetPlayer1() {
            return this.player1;
        }

        /**
         * @returns The second player.
         */
        public IPlayer GetPlayer2() {
            return this.player2;
        }

        /**
         * @returns The map for this game.
         */
        public IMap GetMap() {
            return this.map;
        }

        /**
         * @returns The manager for the current round.
         */
        public IRound GetRound() {
            return this.round;
        }

        /**
         * @returns The number of the current round.
         */
        public int GetCurrentRound() {
            return this.currentRound;
        }

        /**
         * @returns The maximum number of rounds allowed.
         */
        public int GetMaxNbRound() {
            return this.maxRounds;
        }

        /**
         * @returns The player currently playing.
         */
        public IPlayer GetCurrentPlayer() {
            return this.currentPlayer;
        }
    }
}