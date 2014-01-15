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
        public IPlayer Player1 {
            get {
                return this.player1;
            }
        }
        public IPlayer Player2 {
            get {
                return this.player2;
            }
        }
        public IMap Map {
            get {
                return this.map;
            }
        }
        public IRound Round {
            get {
                return this.round;
            }
        }
        public int CurrentRound {
            get {
                return this.currentRound;
            }
        }
        public int MaxNbRound {
            get {
                return this.maxRounds;
            }
        }
        public IPlayer CurrentPlayer {
            get {
                return this.currentPlayer;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="player1">The first player.</param>
        /// <param name="player2">The second player.</param>
        /// <param name="map">The map for this game.</param>
        /// <param name="maxRounds">The maximum number of rounds that can be played.</param>
        public Game(IPlayer player1, IPlayer player2, IMap map, int maxRounds) {
            this.player1 = player1;
            this.player2 = player2;
            this.map = map;
            this.maxRounds = maxRounds;
            this.currentRound = 1;
            this.currentPlayer = player1;
            this.round = new Round(this, currentPlayer);
        }

        /// <summary>
        /// Returns the number of units owner by the player.
        /// </summary>
        /// <param name="player">A player.</param>
        /// <returns>The number of units owned by the player.</returns>
        public int GetNbUnits(IPlayer player) {
            return this.map.GetUnits(player).Count;
        }

        /// <summary>
        /// Checks if a player lost.
        /// A player loses if he doesn't have any more units.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>True if the player lost.</returns>
        private bool IsDefeated(IPlayer player) {
            return this.GetNbUnits(player) == 0;
        }

        /// <summary>
        /// Checks if the game is ended.
        /// A game is ended if a player won or if they reached the maximum number of rounds.
        /// </summary>
        /// <returns>True if the game is ended.</returns>
        public bool IsEndOfGame() {
            return this.IsDefeated(this.player1) 
                || this.IsDefeated(this.player2) 
                || this.currentRound > this.maxRounds;
        }

        /// <summary>
        /// Returns the winner, null if there is none.
        /// </summary>
        /// <returns>The winner, null if there is none.</returns>
        public IPlayer GetWinner() {
            if(this.IsDefeated(this.player1)) {
                return this.player2;
            }
            if(this.IsDefeated(this.player2)) {
                return this.player1;
            }
            if(this.player1.Points < this.player2.Points) {
                return this.player2;
            }
            if(this.player1.Points > this.player2.Points) {
                return this.player1;
            }
            return null;
        }

        /// <summary>
        /// Ends the current round and start the next one.
        /// </summary>
        public void EndRound() {
            Dictionary<IUnit, IPoint> units = map.GetUnits(this.currentPlayer);
            foreach(IUnit unit in units.Keys) {
                ITile tile = this.map.GetTile(units[unit]);
                ITile[] neighbours = this.GetNeighbours(units[unit]);
                this.currentPlayer.AddPoints(unit.GetPoints(tile, neighbours));
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

        /// <summary>
        /// Retrieves the neighbours' types of a specific position.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <returns>An array of neighbours' types, or null if a neighbour was out bounds.</returns>
        private ITile[] GetNeighbours(IPoint pos) {
            int[] xOffset = {0, -1, 0, 1};
            int[] yOffset = {0, -1, 0, 1};
            ITile[] neighbours = new ITile[4];
            for(int i=0; i<4; i++) {
                int x = pos.X + xOffset[i];
                int y = pos.Y + yOffset[i];
                if(x >= 0 && y >= 0 && x < this.map.Size && y < this.map.Size) {
                    neighbours[i] = this.map.GetTile(new Point(x, y));
                } else {
                    neighbours[i] = null;
                }
            }
            return neighbours;
        }
    }
}