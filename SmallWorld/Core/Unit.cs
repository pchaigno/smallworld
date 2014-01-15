using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace SmallWorld {

    [Serializable()]
    public abstract class Unit: IUnit, ISerializable {
        protected const int MOVEMENT_COST = 2;
        protected const int ATTACK = 2;
        protected const int DEFENSE = 1;
        protected const int DEFAULT_LIFE_POINTS = 5;
        protected const int DEFAULT_MOVEMENT_POINTS = 2;
        protected int lifePoints;
        protected int remainingMovementPoints;
        public int LifePoints {
            get {
                return this.lifePoints;
            }
        }
        public int DefaultLifePoints {
            get {
                return DEFAULT_LIFE_POINTS;
            }
        }
        public int Attack {
            get {
                return ATTACK;
            }
        }
        public int Defense {
            get {
                return DEFENSE;
            }
        }
        public IPlayer Owner {
            get;
            set;
        }
        public int RemainingMovementPoints {
            get {
                return this.remainingMovementPoints;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="owner">The player owner of the unit.</param>
        public Unit(IPlayer owner) {
            this.Owner = owner;
            this.lifePoints = DEFAULT_LIFE_POINTS;
            this.remainingMovementPoints = DEFAULT_MOVEMENT_POINTS;
        }

        /// <summary>
        /// Constructor for the deserialization.
        /// </summary>
        /// <param name="info">Information for the serialization.</param>
        /// <param name="context">The context for the serialization.</param>
        public Unit(SerializationInfo info, StreamingContext context) {
            this.lifePoints = (int)info.GetValue("LifePoints", typeof(int));
            this.remainingMovementPoints = (int)info.GetValue("MovementPoints", typeof(int));
        }

        /// <summary>
        /// Method for the serialization.
        /// Fills info with the attributs' values.
        /// </summary>
        /// <param name="info">Information for the serialization.</param>
        /// <param name="context">The context for the serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("LifePoints", this.lifePoints);
            info.AddValue("MovementPoints", this.remainingMovementPoints);
            info.AddValue("Owner", this.Owner.Number);
        }

        /// <summary>
        /// Removes one life point to the unit.
        /// </summary>
        /// <retruns>True if the life points have been decreased.</retruns>
        public bool DecreaseLifePoints() {
            if(this.lifePoints < 1) {
                return false;
            }
            this.lifePoints--;
            return true;
        }

        /// <summary>
        /// Resets the remaining movement points to the default number.
        /// </summary>
        public void ResetMovementPoints() {
            this.remainingMovementPoints = DEFAULT_MOVEMENT_POINTS;
        }

        /// <summary>
        /// Checks if the unit is alive.
        /// </summary>
        /// <returns>True if the unit is alive.</returns>
        public bool IsAlive() {
            return this.lifePoints > 0;
        }

        /// <summary>
        /// Computes the points won by the unit.
        /// </summary>
        /// <param name="tile">The type of tile the unit is currently on.</param>
        /// <param name="neighbours">The neighbour tiles (array of 4 tiles or null if out bounds).</param>
        /// <returns>The points won by the unit for this round.</returns>
        public abstract int GetPoints(ITile tile, ITile[] neighbours);

        /// <summary>
        /// Updates the number of remaining points after a move.
        /// </summary>
        /// <param name="destination">The type of tile the destination is.</param>
        /// <returns>False if the unit couldn't be move to that destination.</returns>
        public virtual bool Move(ITile destination) {
            if(this.remainingMovementPoints < MOVEMENT_COST) {
                return false;
            }
            this.remainingMovementPoints -= MOVEMENT_COST;
            return true;
        }

        /// <summary>
        /// Checks if the unit can move during this round to a certain destination.
        /// The destination must be next to the current position,
        /// the unit must have some movement points left,
        /// the tile can't be a sea.
        /// </summary>
        /// <param name="currentPosition">The current position.</param>
        /// <param name="currentTile">The current type of tile.</param>
        /// <param name="destination">The destination to reach.</param>
        /// <param name="tile">The type of tile the destination is.</param>
        /// <returns>True if the unit can move to the destination.</returns>
        public virtual bool CanMove(IPoint currentPosition, ITile currentTile, IPoint destination, ITile tile, bool occupied) {
            return !(tile is ISea)
                && remainingMovementPoints >= MOVEMENT_COST
                && destination.IsNext(currentPosition);
        }
    }
}