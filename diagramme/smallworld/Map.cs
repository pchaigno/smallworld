using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld {

    public class Map: IMap {
        // TODO Can't we use matrices?
        private Dictionary<Point, List<IUnit>> units;
        private Dictionary<Point, ISquare> squares;
        private int size;

        /**
         * Constructor
         * @param squares An associative array with the type of square for each position.
         */
        public Map(Dictionary<Point, ISquare> squares) {
            this.squares = squares;
            size = (int)Math.Sqrt(squares.Count);

            // Initialize the matrix of units:
            units = new Dictionary<Point, List<IUnit>>();
            foreach(Point key in squares.Keys) {
                units.Add(key, new List<IUnit>());
            }

        }

        /**
         * @returns The size of the map.
         */
        public int getSize() {
            return size;
        }

        /**
         * @returns The composition of the map as a dictionary of squares by coordinates.
         */
        public Dictionary<Point, ISquare> getSquares() {
            return squares;
        }

        /**
         * @returns The position of the units on the map as a dictionary of units by coordinates.
         */
        public Dictionary<Point, List<IUnit>> getUnits() {
            return units;
        }

        /**
         * @param position A position.
         * @returns The units at the position given.
         */
        public List<IUnit> getUnits(Point position) {
            return units[position];
        }

        /**
         * Checks if a position is an enemy position relatively to an unit.
         * @param position The position.
         * @param unit The unit.
         */
        public bool isEnemyPosition(Point position, IUnit unit) {
            if(units[position].Count == 0) {
                return false;
            } else {
                return !units[position][0].getOwner().Equals(unit.getOwner());
            }
        }

        /**
         * Places an unit on the map.
         * @param unit The unit.
         * @param position The position for the unit.
         */
        public void placeUnit(IUnit unit, Point position) {
            units[position].Add(unit);
        }

        /**
         * Gets the squares (its type) at a certain position.
         * @param position The position.
         * @returns The square at this position.
         */
        public ISquare getSquare(Point position) {
            return squares[position];
        }

        /**
         * Moves an unit to a new position.
         * @param unit The unit.
         * @param newPosition The new position.
         * @throws An exception if it's an enemy position.
         */
        public void moveUnit(IUnit unit, Point newPosition) {
            units[unit.getPosition()].Remove(unit);
            if(this.isEnemyPosition(newPosition, unit)) {
                throw new Exception("Erreur dans le deplacement");
            }
            units[newPosition].Add(unit);
            unit.move(newPosition);
        }

        /**
         * Remove an unit from the map.
         * @param unit The unit.
         * @param position The position of the unit.
         */
        public void removeUnit(IUnit unit, Point position) {
            units[position].Remove(unit);
        }

        /**
         * Retrieves all units of a player.
         * @param player The player.
         * @returns All his units on the map.
         */
        public List<IUnit> getUnits(IPlayer player) {
            List<IUnit> result = new List<IUnit>();
            foreach(List<IUnit> unitsL in units.Values) {
                if(unitsL.Count>0 && unitsL[0].getOwner()==player) {
                    foreach(IUnit unit in unitsL) {
                        result.Add(unit);
                    }
                }
            }

            return result;
        }
    }
}