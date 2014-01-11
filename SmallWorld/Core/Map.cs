using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmallWorld {

    [Serializable()]
    public class Map: IMap, ISerializable {
        private List<IUnit>[,] units;
        private ISquare[,] squares;
        private int size;
        public List<IUnit>[,] Units {
            get {
                return this.units;
            }
        }
        public ISquare[,] Squares {
            get {
                return this.squares;
            }
        }
        public int Size {
            get {
                return this.size;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="squares">A matrix with the type of square for each position.</param>
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

        /// <summary>
        /// Constructor for the deserialization.
        /// </summary>
        /// <param name="info">Information for the serialization.</param>
        /// <param name="context">The context for the serialization.</param>
        public Map(SerializationInfo info, StreamingContext context) {
            this.size = (int)info.GetValue("Size", typeof(int));
            this.units = (List<IUnit>[,])info.GetValue("Units", typeof(List<IUnit>[,]));
            this.squares = (ISquare[,])info.GetValue("Squares", typeof(ISquare[,]));
        }

        /// <summary>
        /// Method for the serialization.
        /// Fills info with the attributs' values.
        /// </summary>
        /// <param name="info">Information for the serialization.</param>
        /// <param name="context">The context for the serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Size", this.size);
            info.AddValue("Units", this.units);
            info.AddValue("Squares", this.squares);
        }

        /// <summary>
        /// Retrieves the units at a position.
        /// </summary>
        /// <param name="position">A position.</param>
        /// <returns>The units at the position given.</returns>
        public List<IUnit> GetUnits(IPoint position) {
            return this.units[position.X, position.Y];
        }

        /// <summary>
        /// Checks if a position is an enemy position relatively to an unit.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>True if the position is an enemy position.</returns>
        public bool IsEnemyPosition(IPoint position, IUnit unit) {
            if(this.units[position.X, position.Y].Count == 0) {
                return false;
            } else {
                return !this.units[position.X, position.Y][0].Owner.Equals(unit.Owner);
            }
        }

        /// <summary>
        /// Places an unit on the map.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="position">The position for the unit.</param>
        public void PlaceUnit(IUnit unit, IPoint position) {
            this.units[position.X, position.Y].Add(unit);
        }

        /// <summary>
        /// Gets the squares (its type) at a certain position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>The square at this position.</returns>
        public ISquare GetSquare(IPoint position) {
            return this.squares[position.X, position.Y];
        }

        /// <summary>
        /// Moves an unit to a new position.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="currentPosition">The current position.</param>
        /// <param name="newPosition">The new position.</param>
        /// <exception cref="TODO">An exception if it's an enemy position.</exception>
        public void MoveUnit(IUnit unit, IPoint currentPosition, IPoint newPosition) {
            this.units[currentPosition.X, currentPosition.Y].Remove(unit);
            if(this.IsEnemyPosition(newPosition, unit)) {
                // TODO Need to define a better exception.
                throw new Exception("Erreur dans le deplacement");
            }
            this.units[newPosition.X, newPosition.Y].Add(unit);
        }

        /// <summary>
        /// Removes an unit from the map.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="position">The position of the unit.</param>
        /// <returns>False if the unit wasn't found at this position.</returns>
        public bool RemoveUnit(IUnit unit, IPoint position) {
            return this.units[position.X, position.Y].Remove(unit);
        }

        /// <summary>
        /// Retrieves all units of a player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>A dictionary of the units with their position.</returns>
        public Dictionary<IUnit, IPoint> GetUnits(IPlayer player) {
            Dictionary<IUnit, IPoint> result = new Dictionary<IUnit, IPoint>();
            for(int x=0; x<this.size; x++) {
                for(int y=0; y<this.size; y++) {
                    List<IUnit> unitsAtPosition = this.units[x, y];
                    if(unitsAtPosition.Count > 0 && unitsAtPosition[0].Owner == player) {
                        foreach(IUnit unit in unitsAtPosition) {
                            result.Add(unit, new Point(x, y));
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Retrieves an unit that can still move.
        /// </summary>
        /// <param name="player">The player whose turn it is.</param>
        /// <returns>A tuple with an idle unit and its position.</returns>
        public Tuple<IUnit, IPoint> GetIdleUnit(IPlayer player) {
            for(int x = 0; x < this.size; x++) {
                for(int y = 0; y < this.size; y++) {
                    List<IUnit> unitsAtPosition = this.units[x, y];
                    if(unitsAtPosition.Count>0 && unitsAtPosition[0].Owner==player) {
                        foreach(IUnit unit in unitsAtPosition) {
                            if(unit.RemainingMovementPoints > 0) {
                                return Tuple.Create<IUnit, IPoint>(unit, new Point(x, y));
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}