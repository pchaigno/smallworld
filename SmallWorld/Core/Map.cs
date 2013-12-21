using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld {

    public class Map: IMap {
        private List<IUnit>[,] units;
        private ISquare[,] squares;
        private int size;

        /**
         * Constructor
         * @param squares A matrix with the type of square for each position.
         */
        public Map(ISquare[,] squares) {
            this.squares = squares;
            this.size = this.squares.GetLength(0);

            // Initialize the matrix of units:
            units = new List<IUnit>[size, size];
            for(int x=0; x<this.size; x++) {
                for(int y=0; y<this.size; y++) {
                    units[x, y] = new List<IUnit>();
                }
            }
        }

        /**
         * @returns The size of the map.
         */
        public int getSize() {
            return this.size;
        }

        /**
         * @returns The composition of the map as a matrix of squares by coordinates.
         */
        public ISquare[,] getSquares() {
            return this.squares;
        }

        /**
         * @returns The position of the units on the map as a matrix of units by coordinates.
         */
        public List<IUnit>[,] getUnits() {
            return this.units;
        }

        /**
         * @param position A position.
         * @returns The units at the position given.
         */
        public List<IUnit> getUnits(Point position) {
            return this.units[position.X, position.Y];
        }

        /**
         * Checks if a position is an enemy position relatively to an unit.
         * @param position The position.
         * @param unit The unit.
         */
        public bool isEnemyPosition(Point position, IUnit unit) {
            if(this.units[position.X, position.Y].Count == 0) {
                return false;
            } else {
                return !this.units[position.X, position.Y][0].getOwner().Equals(unit.getOwner());
            }
        }

        /**
         * Places an unit on the map.
         * @param unit The unit.
         * @param position The position for the unit.
         */
        public void placeUnit(IUnit unit, Point position) {
            this.units[position.X, position.Y].Add(unit);
        }

        /**
         * Gets the squares (its type) at a certain position.
         * @param position The position.
         * @returns The square at this position.
         */
        public ISquare getSquare(Point position) {
            return this.squares[position.X, position.Y];
        }

        /**
         * Moves an unit to a new position.
         * @param unit The unit.
         * @param newPosition The new position.
         * @throws An exception if it's an enemy position.
         */
        public void moveUnit(IUnit unit, Point currentPosition, Point newPosition) {
            this.units[currentPosition.X, currentPosition.Y].Remove(unit);
            if(this.isEnemyPosition(newPosition, unit)) {
                throw new Exception("Erreur dans le deplacement");
            }
            this.units[newPosition.X, newPosition.Y].Add(unit);
            unit.move(this.getSquare(newPosition));
        }

        /**
         * Remove an unit from the map.
         * @param unit The unit.
         * @param position The position of the unit.
         */
        public void removeUnit(IUnit unit, Point position) {
            this.units[position.X, position.Y].Remove(unit);
        }

        /**
         * Retrieves all units of a player.
         * @param player The player.
         * @returns A dictionary of the units with the type of square they're on.
         */
        public Dictionary<IUnit, ISquare> getUnits(IPlayer player) {
            Dictionary<IUnit, ISquare> result = new Dictionary<IUnit, ISquare>();
            for(int x=0; x<this.size; x++) {
                for(int y=0; y<this.size; y++) {
                    List<IUnit> unitsAtPosition = this.units[x, y];
                    if(unitsAtPosition.Count > 0 && unitsAtPosition[0].getOwner() == player) {
                        foreach(IUnit unit in unitsAtPosition) {
                            result.Add(unit, this.squares[x, y]);
                        }
                    }
                }
            }
            return result;
        }
    }
}