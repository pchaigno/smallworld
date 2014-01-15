using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mWrapper;
using System.Runtime.Serialization;

namespace SmallWorld {

    [Serializable()]
    public class Round: IRound {
        private IGame game;
        private IPlayer player;
        private List<IUnit> selectedUnits;
        private IPoint selectedPosition;
        private IPoint destination;
        // TODO We should use a code and code/messages correspondances for move information.
        private string lastMoveInfo;
        public string LastMoveInfo {
            get {
                return this.lastMoveInfo;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">The game the round is part of.</param>
        /// <param name="player">The player for this round.</param>
        public Round(IGame game, IPlayer player) {
            this.game = game;
            this.player = player;
            this.lastMoveInfo = "";
            selectedUnits = new List<IUnit>();
        }

        /// <summary>
        /// Retrieves advised destinations from the C++ library.
        /// </summary>
        /// <param name="unit">The unit for which we want advise.</param>
        /// <param name="pos">The unit's position.</param>
        /// <returns>The list of advised desinations.</returns>
        public List<IPoint> GetAdvisedDestinations(IUnit unit, IPoint pos) {
            IMap map = this.game.Map;
            ITile[,] tiles = map.Tiles;
            int[][] mapBis = new int[map.Size][];
            int[][] units = new int[map.Size][];
            for(int i=0; i<map.Size; i++) {
                mapBis[i] = new int[map.Size];
                units[i] = new int[map.Size];
                for(int j=0; j<map.Size; j++) {
                    mapBis[i][j] = tiles[i, j].Number;
                    List<IUnit> unitsAtPos = map.GetUnits(new Point(i, j));
                    if(unitsAtPos.Count > 0) {
                        units[i][j] = unitsAtPos[0].Owner.Number;
                    }
                }
            }
            
            int nationPlayer1 = this.game.Player1.NationNumber;
            int nationPlayer2 = this.game.Player2.NationNumber;
            int[][] result = Wrapper.getAdvice(mapBis, map.Size, nationPlayer1, nationPlayer2, pos.X, pos.Y, units, this.player.Number);
            List<IPoint> advice = new List<IPoint>();
            for(int i=0; i<3; i++) {
                if(result[i][0]!=-1 && result[i][1]!=-1) {
                    advice.Add(new Point(result[i][0], result[i][1]));
                }
            }
            return advice;
        }

        /// <summary>
        /// Sets the new selected unit.
        /// The selected position stay the same.
        /// </summary>
        /// <param name="units">The unit to select.</param>
        public void SelectUnits(List<IUnit> units) {
            this.selectedUnits = units;
        }

        /// <summary>
        /// Sets the new selected unit.
        /// The selected position is also updated.
        /// </summary>
        /// <param name="units">The unit to select.</param>
        /// <param name="position">The unit's position.</param>
        public void SelectUnits(List<IUnit> units, IPoint position) {
            this.selectedUnits = units;
            this.selectedPosition = position;
        }

        /// <summary>
        /// Unselects the last selected unit.
        /// </summary>
        /// <remarks>
        /// The last selected position isn't unselected.
        /// </remarks>
        public void UnselectUnit() {
            this.selectedUnits.Clear();
        }

        /// <summary>
        /// Retrieves all units from a position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>The units on this position.</returns>
        public List<IUnit> GetUnits(IPoint position) {
            return this.game.Map.GetUnits(position);
        }

        /// <summary>
        /// Checks if a position is under the control of the current player.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>True if the position is under the control of the current player.</returns>
        public bool IsCurrentPlayerPosition(IPoint position) {
            List<IUnit> units = this.game.Map.GetUnits(position);
            return units.Count > 0 
                && units[0].Owner.Equals(this.player);
        }

        /// <summary>
        /// Checks if the unit currently selected can move to a desination.
        /// Saves the destination if the move is possible.
        /// </summary>
        /// <param name="destination">The desination.</param>
        /// <returns>True if the current unit can move to the destination.</returns>
        public bool SetDestination(IPoint destination) {
            if(this.selectedUnits.Count == 0) {
                this.lastMoveInfo = "You have to select a unit first.";
                return false;
            }

            bool result = true;
            foreach(IUnit unit in this.selectedUnits) {
                bool occupied = game.Map.IsEnemyPosition(destination, unit);
                if(!unit.CanMove(this.selectedPosition, game.Map.GetTile(this.selectedPosition), destination, game.Map.GetTile(destination), occupied)) {
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

        /// <summary>
        /// Executes the move of the current unit to the destination selected previously.
        /// Unselects the unit.
        /// </summary>
        /// <see cref="Round.SetDestination"/>
        /// <exception cref="">If the unit couldn't be moved to the destination point.</exception>
        public void ExecuteMove() {
            // TODO improve message

            if(this.game.Map.IsEnemyPosition(this.destination, this.selectedUnits[0])) {
                for(int i = 0; i < this.selectedUnits.Count; i++) {
                    IUnit unit = this.selectedUnits[i];
                    if(!unit.Move(game.Map.GetTile(destination))) {
                        // TODO Need to define a better exception.
                        throw new Exception("The unit " + unit + " couldn't be moved to " + destination + ".");
                    }
                    if(Combat(unit)) {
                        if(this.game.Map.GetUnits(this.destination).Count == 0) {
                            this.game.Map.MoveUnit(unit, this.selectedPosition, this.destination);
                        }
                    }
                }
            } else {
                for(int i = 0; i < this.selectedUnits.Count; i++) {
                    IUnit unit = this.selectedUnits[i];
                    if(!unit.Move(game.Map.GetTile(destination))) {
                        throw new Exception("The unit " + unit + " couldn't be moved to " + destination + ".");
                    }
                    this.game.Map.MoveUnit(unit, this.selectedPosition, this.destination);
                    this.lastMoveInfo = this.player.Name + " moved an unit.";
                }
            }
            this.selectedUnits.Clear();
        }

        /// <summary>
        /// Computes a fight between the currently selected unit
        /// and the best one from the selected destination.
        /// </summary>
        /// <param name="unit">The unit who fight.</param>
        /// <returns>True if the selected unit won the fight.</returns>
        private bool Combat(IUnit unit) {
            IUnit enemy = GetBestUnit();

            Random randCombat = new Random();
            Random rand = new Random();

            int nbRound = 3 + randCombat.Next((Math.Max(unit.LifePoints, enemy.LifePoints)) + 2);
            int n = 0;

            while(nbRound > n && unit.IsAlive() && enemy.IsAlive()) {
                double ratioLife = (double)unit.LifePoints / (double)unit.DefaultLifePoints;
                double ratioLifeDef = (double)enemy.LifePoints / (double)enemy.DefaultLifePoints;
                double attaUnit = (double)unit.Attack * (double)ratioLife;
                double defUnitdef = (double)enemy.Defense * (double)ratioLifeDef;
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
                this.game.Map.RemoveUnit(unit, this.selectedPosition);
                this.lastMoveInfo = this.player.Name + " lost the fight.";
                return false;
            } else if(!enemy.IsAlive()) {
                this.game.Map.RemoveUnit(enemy, this.destination);
                this.lastMoveInfo = this.player.Name + " won the fight.";
                return true;
            } else {
                this.lastMoveInfo = "The fight ended with a draw";
                return false;
            }
        }

        /// <summary>
        /// Retrieves the best unit from the tile currently selected.
        /// The best unit is the one with the most life points.
        /// </summary>
        /// <returns>The best unit on the tile currently selected.</returns>
        private IUnit GetBestUnit() {
            IUnit result = null;
            List<IUnit> units = this.game.Map.GetUnits(this.destination);
            if(units.Count > 0) {
                result = units[0];
                for(int i=1; i<units.Count; i++) {
                    if(result.LifePoints < units[i].LifePoints) {
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