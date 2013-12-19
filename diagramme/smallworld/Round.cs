using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld {

    public class Round: IRound {
        private IGame game;
        private IPlayer player;
        private IUnit selectedUnit;
        private Point destination;
        private String lastMoveInfo;

        /**
         * Constructor
         * @param game The game the round is part of.
         * @param player The player for this round.
         */
        public Round(IGame game, IPlayer player) {
            this.game = game;
            this.player = player;
            lastMoveInfo = "";
        }

        /**
         * @returns The information about the last move.
         */
        public String getLastMoveInfo() {
            return lastMoveInfo;
        }

        /**
         * Retrieves advised destinations from the C++ library.
         * @param unit The unit for which we want advise.
         * @param position The unit's position.
         * @returns The list of advised desinations.
         */
        public List<Point> getAdvisedDestinations(IUnit unit, Point position) {
            // TODO Retrieve from wrapper.
            return new List<Point>();
        }

        /**
         * Set the new selected unit.
         * @param unit The unit to select.
         */
        public void selectUnit(IUnit unit) {
            this.selectedUnit = unit;
        }

        /**
         * Retrieve all units from a position.
         * @param position The position
         * @returns The units on this position.
         */
        public List<IUnit> getUnits(Point position) {
            return game.getMap().getUnits(position);
        }

        /**
         * Checks if a position is under the control of the current player.
         * @param position The position.
         * @returns True if the position is under the control of the current player.
         */
        public Boolean isCurrentPlayerPosition(Point position) {
            List<IUnit> units = game.getMap().getUnits(position);
            return units.Count>0 && units[0].getOwner()==player;
        }

        /**
         * Checks if the unit currently selected can move to a desination.
         * Save the destination if the move is possible.
         * @param destination The desination.
         * @param True if the current unit can move to the destination.
         */
        public bool setDestination(Point destination) {
            if(this.selectedUnit == null) {
                lastMoveInfo = "You have to select a unit first.";
                return false;
            }

            Boolean result = selectedUnit.canMove(destination);
            if(result) {
                this.destination = destination;
            } else {
                lastMoveInfo = "You cannot move here.";
            }

            return result;
        }

        /**
         * Execute the move of the current unit to the destination selected previously.
         * Unselect the unit.
         * @see setDestination
         */
        public void executeMove() {
            if(game.getMap().isEnemyPosition(destination, selectedUnit)) {
                if(combat()) {
                    Console.WriteLine(game.getMap().getUnits(destination).Count);
                    if(game.getMap().getUnits(destination).Count == 0) {
                        game.getMap().moveUnit(selectedUnit, destination);
                    }
                }
            } else {
                game.getMap().moveUnit(selectedUnit, destination);
                lastMoveInfo = player.getName() + " moved a unit.";
            }

            selectedUnit = null;
        }

        /**
         * Compute a fight between the currently selected unit
         * and the best one from the selected destination.
         * @returns True if the selected unit won the fight.
         */
        private Boolean combat() {
            IUnit enemy = getBestUnit();

            Random randCombat = new Random();
            Random rand = new Random();

            int nbRound = 3 + randCombat.Next((Math.Max(selectedUnit.getLifePoints(), enemy.getLifePoints())) + 2);
            int n = 0;

            while(nbRound>n && selectedUnit.isAlive() && enemy.isAlive()) {
                double ratioLife = (double)selectedUnit.getLifePoints() / (double)selectedUnit.getDefaultLifePoints();
                double ratioLifeDef = (double)enemy.getLifePoints() / (double)enemy.getDefaultLifePoints();
                double attaUnit = (double)selectedUnit.getAttack() * (double)ratioLife;
                double defUnitdef = (double)enemy.getDefense() * (double)ratioLifeDef;
                double ratioAttDef = (double)(attaUnit / defUnitdef);
                double ratioChanceDef = 0;
                if(ratioAttDef > 1) {
                    // Advantage for the attacker.
                    ratioChanceDef = (1 / ratioAttDef) / 2;
                    ratioChanceDef = (0.5 - ratioChanceDef) + 0.5;
                } else if(ratioAttDef == 1) {
                    // Draw: 50% chances to win.
                    ratioChanceDef = 0.5;
                } else {
                    // Advantage for the defender.
                    ratioChanceDef = ratioAttDef / 2;
                }
                double ratioCombat = (double)((double)rand.Next(100) / 100);

                if(ratioCombat <= ratioChanceDef) {
                    enemy.decreaseLifePoints();
                } else {
                    selectedUnit.decreaseLifePoints();
                }
                n++;
            }

            if(!selectedUnit.isAlive()) {
                game.getMap().removeUnit(selectedUnit, selectedUnit.getPosition());
                lastMoveInfo = player.getName() + " lost the fight.";
                return false;
            } else if(!enemy.isAlive()) {
                game.getMap().removeUnit(enemy, destination);
                lastMoveInfo = player.getName() + " won the fight.";
                return true;
            } else {
                lastMoveInfo = "The fight ended with a draw (Grammar ??)";
                return false;
            }

        }

        /**
         * Retrieves the best unit from the square currently selected.
         * The best unit is the one with the most life points.
         * @returns The best unit on the square currently selected.
         */
        private IUnit getBestUnit() {
            IUnit result = null;
            List<IUnit> units = game.getMap().getUnits(destination);
            if(units.Count > 0) {
                result = units[0];
                for(int i=1; i<units.Count; i++) {
                    if(result.getLifePoints() < units[i].getLifePoints()) {
                        result = units[i];
                    }
                }
            } else {
                // TODO Throw exception
            }
            return result;
        }
    }
}