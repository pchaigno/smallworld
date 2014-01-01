using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using mWrapper;

namespace SmallWorld {

    public class Round: IRound {
        private IGame game;
        private IPlayer player;
        private IUnit selectedUnit;
        private Point selectedPosition;
        private Point destination;
        // TODO We should use a code and code/messages correspondances for move information.
        private String lastMoveInfo;

        /**
         * Constructor
         * @param game The game the round is part of.
         * @param player The player for this round.
         */
        public Round(IGame game, IPlayer player) {
            this.game = game;
            this.player = player;
            this.lastMoveInfo = "";
        }

        /**
         * @returns The information about the last move.
         */
        public String getLastMoveInfo() {
            return this.lastMoveInfo;
        }

        /**
         * Retrieves advised destinations from the C++ library.
         * @param unit The unit for which we want advise.
         * @param pos The unit's position.
         * @returns The list of advised desinations.
         */
        public List<Point> getAdvisedDestinations(IUnit unit, Point pos) {
            // TODO Retrieve from wrapper.
            IMap map = this.game.getMap();
            ISquare[,] squares = map.getSquares();
            int[][] mapBis = new int[map.getSize()][];
            int[][] units = new int[map.getSize()][];
            for(int i=0; i<map.getSize(); i++) {
                mapBis[i] = new int[map.getSize()];
                units[i] = new int[map.getSize()];
                for(int j=0; j<map.getSize(); j++) {
                    mapBis[i][j] = squares[i, j].getNumber();
                    List<IUnit> unitsAtPos = map.getUnits(new Point(i, j));
                    if(unitsAtPos.Count > 0) {
                        units[i][j] = unitsAtPos[0].getOwner().getNumber();
                    }
                }
            }
            
            int nationPlayer1 = this.game.getPlayer1().getNationNumber();
            int nationPlayer2 = this.game.getPlayer2().getNationNumber();
            int[][] result = Wrapper.getAdvice(mapBis, map.getSize(), nationPlayer1, nationPlayer2, pos.X, pos.Y, units, this.player.getNumber());
            List<Point> advice = new List<Point>();
            for(int i=0; i<3; i++) {
                if(result[i][0]!=-1 && result[i][1]!=-1) {
                    advice.Add(new Point(result[i][0], result[i][1]));
                }
            }
            return advice;
        }

        /**
         * Set the new selected unit.
         * The selected position stay the same.
         * @param unit The unit to select.
         */
        public void selectUnit(IUnit unit) {
            this.selectedUnit = unit;
        }

        /**
         * Set the new selected unit.
         * The selected position is also updated.
         * @param unit The unit to select.
         * @param position The unit's position.
         */
        public void selectUnit(IUnit unit, Point position) {
            this.selectedUnit = unit;
            this.selectedPosition = position;
        }

        /**
         * Unselect the last selected unit.
         * The last selected position isn't unselected.
         */
        public void unselectUnit() {
            this.selectedUnit = null;
        }

        /**
         * Retrieve all units from a position.
         * @param position The position
         * @returns The units on this position.
         */
        public List<IUnit> getUnits(Point position) {
            return this.game.getMap().getUnits(position);
        }

        /**
         * Checks if a position is under the control of the current player.
         * @param position The position.
         * @returns True if the position is under the control of the current player.
         */
        public Boolean isCurrentPlayerPosition(Point position) {
            List<IUnit> units = this.game.getMap().getUnits(position);
            return units.Count > 0 
                && units[0].getOwner() == this.player;
        }

        /**
         * Checks if the unit currently selected can move to a desination.
         * Save the destination if the move is possible.
         * @param destination The desination.
         * @param True if the current unit can move to the destination.
         */
        public bool setDestination(Point destination) {
            if(this.selectedUnit == null) {
                this.lastMoveInfo = "You have to select a unit first.";
                return false;
            }

            Boolean result = selectedUnit.canMove(this.selectedPosition, destination, game.getMap().getSquare(destination));
            if(result) {
                this.destination = destination;
            } else {
                this.lastMoveInfo = "You cannot move here.";
            }

            return result;
        }

        /**
         * Execute the move of the current unit to the destination selected previously.
         * Unselect the unit.
         * @see setDestination
         */
        public void executeMove() {
            if(this.game.getMap().isEnemyPosition(this.destination, this.selectedUnit)) {
                if(combat()) {
                    if(this.game.getMap().getUnits(this.destination).Count == 0) {
                        this.game.getMap().moveUnit(this.selectedUnit, this.selectedPosition, this.destination);
                    }
                }
            } else {
                this.game.getMap().moveUnit(this.selectedUnit, this.selectedPosition, this.destination);
                this.lastMoveInfo = this.player.getName() + " moved an unit.";
            }

            this.selectedUnit = null;
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

            int nbRound = 3 + randCombat.Next((Math.Max(this.selectedUnit.getLifePoints(), enemy.getLifePoints())) + 2);
            int n = 0;

            while(nbRound > n && this.selectedUnit.isAlive() && enemy.isAlive()) {
                double ratioLife = (double)this.selectedUnit.getLifePoints() / (double)this.selectedUnit.getDefaultLifePoints();
                double ratioLifeDef = (double)enemy.getLifePoints() / (double)enemy.getDefaultLifePoints();
                double attaUnit = (double)this.selectedUnit.getAttack() * (double)ratioLife;
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
                    this.selectedUnit.decreaseLifePoints();
                }
                n++;
            }

            if(!this.selectedUnit.isAlive()) {
                this.game.getMap().removeUnit(this.selectedUnit, this.selectedPosition);
                this.lastMoveInfo = this.player.getName() + " lost the fight.";
                return false;
            } else if(!enemy.isAlive()) {
                this.game.getMap().removeUnit(enemy, this.destination);
                this.lastMoveInfo = this.player.getName() + " won the fight.";
                return true;
            } else {
                this.lastMoveInfo = "The fight ended with a draw (Grammar ??)";
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
            List<IUnit> units = this.game.getMap().getUnits(this.destination);
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