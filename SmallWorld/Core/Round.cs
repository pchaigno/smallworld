using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mWrapper;

namespace SmallWorld {

    public class Round: IRound {
        private IGame game;
        private IPlayer player;
        private List<IUnit> selectedUnit;
        private IPoint selectedPosition;
        private IPoint destination;
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
            selectedUnit = new List<IUnit>();
        }

        /**
         * @returns The information about the last move.
         */
        public String GetLastMoveInfo() {
            return this.lastMoveInfo;
        }

        /**
         * Retrieves advised destinations from the C++ library.
         * @param unit The unit for which we want advise.
         * @param pos The unit's position.
         * @returns The list of advised desinations.
         */
        public List<IPoint> GetAdvisedDestinations(IUnit unit, IPoint pos) {
            IMap map = this.game.GetMap();
            ISquare[,] squares = map.GetSquares();
            int[][] mapBis = new int[map.GetSize()][];
            int[][] units = new int[map.GetSize()][];
            for(int i=0; i<map.GetSize(); i++) {
                mapBis[i] = new int[map.GetSize()];
                units[i] = new int[map.GetSize()];
                for(int j=0; j<map.GetSize(); j++) {
                    mapBis[i][j] = squares[i, j].GetNumber();
                    List<IUnit> unitsAtPos = map.GetUnits(new Point(i, j));
                    if(unitsAtPos.Count > 0) {
                        units[i][j] = unitsAtPos[0].GetOwner().GetNumber();
                    }
                }
            }
            
            int nationPlayer1 = this.game.GetPlayer1().GetNationNumber();
            int nationPlayer2 = this.game.GetPlayer2().GetNationNumber();
            int[][] result = Wrapper.getAdvice(mapBis, map.GetSize(), nationPlayer1, nationPlayer2, pos.X, pos.Y, units, this.player.GetNumber());
            List<IPoint> advice = new List<IPoint>();
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
        public void SelectUnits(List<IUnit> units) {
            this.selectedUnit = units;
        }

        /**
         * Set the new selected unit.
         * The selected position is also updated.
         * @param unit The unit to select.
         * @param position The unit's position.
         */
        public void SelectUnits(List<IUnit> units, IPoint position) {
            this.selectedUnit = units;
            this.selectedPosition = position;
        }

        /**
         * Unselect the last selected unit.
         * The last selected position isn't unselected.
         */
        public void UnselectUnit() {
            this.selectedUnit.Clear();
        }

        /**
         * Retrieve all units from a position.
         * @param position The position
         * @returns The units on this position.
         */
        public List<IUnit> GetUnits(IPoint position) {
            return this.game.GetMap().GetUnits(position);
        }

        /**
         * Checks if a position is under the control of the current player.
         * @param position The position.
         * @returns True if the position is under the control of the current player.
         */
        public bool IsCurrentPlayerPosition(IPoint position) {
            List<IUnit> units = this.game.GetMap().GetUnits(position);
            return units.Count > 0 
                && units[0].GetOwner() == this.player;
        }

        /**
         * Checks if the unit currently selected can move to a desination.
         * Save the destination if the move is possible.
         * @param destination The desination.
         * @param True if the current unit can move to the destination.
         */
        public bool SetDestination(IPoint destination) {
            if(this.selectedUnit.Count == 0) {
                this.lastMoveInfo = "You have to select a unit first.";
                return false;
            }

            bool result = true;
            foreach(IUnit unit in this.selectedUnit) {
                if(!unit.CanMove(this.selectedPosition, game.GetMap().GetSquare(this.selectedPosition), destination, game.GetMap().GetSquare(destination))) {
                    result = false;
                    break;
                }
            }

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
        public void ExecuteMove() {
            // TODO improve message

            if(this.game.GetMap().IsEnemyPosition(this.destination, this.selectedUnit[0])) {
                for(int i = 0; i < this.selectedUnit.Count; i++) {
                    IUnit unit = this.selectedUnit[i];
                    unit.Move(game.GetMap().GetSquare(destination));
                    if(Combat(unit)) {
                        if(this.game.GetMap().GetUnits(this.destination).Count == 0) {
                            this.game.GetMap().MoveUnit(unit, this.selectedPosition, this.destination);
                        }
                    }
                }
            } else {
                for(int i = 0; i < this.selectedUnit.Count; i++) {
                    IUnit unit = this.selectedUnit[i];
                    unit.Move(game.GetMap().GetSquare(destination));
                    this.game.GetMap().MoveUnit(unit, this.selectedPosition, this.destination);
                    this.lastMoveInfo = this.player.GetName() + " moved an unit.";
                }
            }
            this.selectedUnit.Clear();
        }

        /**
         * Compute a fight between the currently selected unit
         * and the best one from the selected destination.
         * @returns True if the selected unit won the fight.
         */
        private bool Combat(IUnit unit) {
            IUnit enemy = GetBestUnit();

            Random randCombat = new Random();
            Random rand = new Random();

            int nbRound = 3 + randCombat.Next((Math.Max(unit.GetLifePoints(), enemy.GetLifePoints())) + 2);
            int n = 0;

            while(nbRound > n && unit.IsAlive() && enemy.IsAlive()) {
                double ratioLife = (double)unit.GetLifePoints() / (double)unit.GetDefaultLifePoints();
                double ratioLifeDef = (double)enemy.GetLifePoints() / (double)enemy.GetDefaultLifePoints();
                double attaUnit = (double)unit.GetAttack() * (double)ratioLife;
                double defUnitdef = (double)enemy.GetDefense() * (double)ratioLifeDef;
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
                    enemy.DecreaseLifePoints();
                } else {
                    unit.DecreaseLifePoints();
                }
                n++;
            }

            if(!unit.IsAlive()) {
                this.game.GetMap().RemoveUnit(unit, this.selectedPosition);
                this.lastMoveInfo = this.player.GetName() + " lost the fight.";
                return false;
            } else if(!enemy.IsAlive()) {
                this.game.GetMap().RemoveUnit(enemy, this.destination);
                this.lastMoveInfo = this.player.GetName() + " won the fight.";
                return true;
            } else {
                this.lastMoveInfo = "The fight ended with a draw";
                return false;
            }

        }

        /**
         * Retrieves the best unit from the square currently selected.
         * The best unit is the one with the most life points.
         * @returns The best unit on the square currently selected.
         */
        private IUnit GetBestUnit() {
            IUnit result = null;
            List<IUnit> units = this.game.GetMap().GetUnits(this.destination);
            if(units.Count > 0) {
                result = units[0];
                for(int i=1; i<units.Count; i++) {
                    if(result.GetLifePoints() < units[i].GetLifePoints()) {
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