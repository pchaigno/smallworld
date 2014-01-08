using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace SmallWorld {

    [Serializable()]
    public abstract class Unit: IUnit, ISerializable {
        protected const int ATTACK = 2;
        protected const int DEFENSE = 1;
        protected const int DEFAULT_LIFE_POINTS = 5;
        protected const int DEFAULT_MOVEMENT_POINTS = 2;
        protected int lifePoints;
        protected int remainingMovementPoints;
        protected IPlayer owner;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="owner">The player owner of the unit.</param>
        public Unit(IPlayer owner) {
            this.owner = owner;
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
            info.AddValue("Owner", this.owner.GetNumber());
        }

        /// <summary>
        /// Returns the unit's life points.
        /// </summary>
        /// <returns>The unit's life points.</returns>
        public int GetLifePoints() {
            return this.lifePoints;
        }

        /// <summary>
        /// Remove one life point to the unit.
        /// </summary>
        public void DecreaseLifePoints() {
            this.lifePoints--;
        }

        /// <summary>
        /// Returns the default number of life points for this unit.
        /// </summary>
        /// <returns>The default number of life points for this unit.</returns>
        public int GetDefaultLifePoints() {
            return DEFAULT_LIFE_POINTS;
        }

        /// <summary>
        /// Returns the unit's defense.
        /// </summary>
        /// <returns>The unit's defense.</returns>
        public int GetAttack() {
            return ATTACK;
        }

        /// <summary>
        /// Returns the unit's defense.
        /// </summary>
        /// <returns>The unit's defense.</returns>
        public int GetDefense() {
            return DEFENSE;
        }

        /// <summary>
        /// Returns the number of remaining movement points for this round.
        /// </summary>
        /// <returns>The number of remaining movement points for this round.</returns>
        public int GetRemainingMovementPoints() {
            return this.remainingMovementPoints;
        }

        /// <summary>
        /// Returns the player owner of the unit.
        /// </summary>
        /// <returns>The player owner of the unit.</returns>
        public IPlayer GetOwner() {
            return this.owner;
        }

        /// <summary>
        /// Sets the unit's owner.
        /// </summary>
        /// <param name="owner">The new unit's owner.</param>
        public void SetOwner(IPlayer owner) {
            this.owner = owner;
        }

        /// <summary>
        /// Reset the remaining movement points to the default number.
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
        /// <param name="square">The type of square the unit is currently on.</param>
        /// <param name="neighbours">The neighbour squares (array of 4 squares or null if out bounds).</param>
        /// <returns>The points won by the unit for this round.</returns>
        public abstract int GetPoints(ISquare square, ISquare[] neighbours);

        /// <summary>
        /// Updates the number of remaining points after a move.
        /// </summary>
        /// <param name="destination">The type of square the destination is.</param>
        public virtual void Move(ISquare destination) {
            this.remainingMovementPoints -= 2;
        }

        /// <summary>
        /// Checks if the unit can move during this round to a certain destination.
        /// The destination must be next to the current position,
        /// the unit must have some movement points left,
        /// the square can't be a sea.
        /// </summary>
        /// <param name="currentPosition">The current position.</param>
        /// <param name="currentSquare">The current type of square.</param>
        /// <param name="destination">The destination to reach.</param>
        /// <param name="square">The type of square the destination is.</param>
        /// <returns>True if the unit can move to the destination.</returns>
        public virtual bool CanMove(IPoint currentPosition, ISquare currentSquare, IPoint destination, ISquare square) {
            return destination.IsNext(currentPosition) 
                && remainingMovementPoints > 0
                && !(square is ISea);
        }
    }
}