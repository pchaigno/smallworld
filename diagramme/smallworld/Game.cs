using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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
        public int getNbUnits(IPlayer player) {
            return this.map.getUnits(player).Count;
        }

        /**
         * Checks if a player lost.
         * A player lose if he doesn't have any more units.
         * @param player The player.
         * @returns True if the player lost.
         */
        private bool isDefeated(IPlayer player) {
            return this.getNbUnits(player) == 0;
        }

        /**
         * Checks if the game is ended.
         * A game is ended if a player won or if they reached the maximum number of rounds.
         * @returns True if the game is ended.
         */
        public bool isEndOfGame() {
            return this.isDefeated(this.player1) 
                || this.isDefeated(this.player2) 
                || this.currentRound > this.maxRounds;
        }

        /**
         * @returns The winner, null if there is none.
         */
        public IPlayer getWinner() {
            if(this.isDefeated(this.player1)) {
                return this.player2;
            } else if(this.isDefeated(this.player2)) {
                return this.player1;
            } else {
                if(this.player1.getPoints() < this.player2.getPoints()) {
                    return this.player2;
                } else if(this.player1.getPoints() > this.player2.getPoints()) {
                    return this.player1;
                }
            }
            return null;
        }

        /**
         * End the current round and start the next one.
         */
        public void endRound() {
            Dictionary<IUnit, ISquare> units = map.getUnits(this.currentPlayer);
            foreach(IUnit unit in units.Keys) {
                this.currentPlayer.addPoints(unit.getPoint(units[unit]));
                unit.resetMovementPoints();
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
         * @returns The first player.
         */
        public IPlayer getPlayer1() {
            return this.player1;
        }

        /**
         * @returns The second player.
         */
        public IPlayer getPlayer2() {
            return this.player2;
        }

        /**
         * @returns The map for this game.
         */
        public IMap getMap() {
            return this.map;
        }

        /**
         * @returns The manager for the current round.
         */
        public IRound getRound() {
            return this.round;
        }

        /**
         * @returns The number of the current round.
         */
        public int getCurrentRound() {
            return this.currentRound;
        }

        /**
         * @returns The player currently playing.
         */
        public IPlayer getCurrentPlayer() {
            return this.currentPlayer;
        }
    }
}