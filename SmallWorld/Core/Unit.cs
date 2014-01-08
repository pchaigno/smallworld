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

        /**
         * Constructor
         * @param owner The player owner of the unit.
         */
        public Unit(IPlayer owner) {
            this.owner = owner;
            this.lifePoints = DEFAULT_LIFE_POINTS;
            this.remainingMovementPoints = DEFAULT_MOVEMENT_POINTS;
        }

        /**
         * Constructor for the deserialization.
         * @param info Information for the serialization.
         * @param context The context for the serialization.
         */
        public Unit(SerializationInfo info, StreamingContext context) {
            this.lifePoints = (int)info.GetValue("LifePoints", typeof(int));
            this.remainingMovementPoints = (int)info.GetValue("MovementPoints", typeof(int));
        }
        
        /**
         * Method for the serialization.
         * Fills info with the attributs' values.
         * @param info Information for the serialization.
         * @param context The context for the serialization.
         */
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("LifePoints", this.lifePoints);
            info.AddValue("MovementPoints", this.remainingMovementPoints);
            info.AddValue("Owner", this.owner.getNumber());
        }

        /**
         * @returns The unit's life points.
         */
        public int getLifePoints() {
            return this.lifePoints;
        }

        /**
         * Remove one life point to the unit.
         */
        public void decreaseLifePoints() {
            this.lifePoints--;
        }

        /**
         * @returns The default number of life points for this unit.
         */
        public int getDefaultLifePoints() {
            return DEFAULT_LIFE_POINTS;
        }

        /**
         * @returns The unit's defense.
         */
        public int getAttack() {
            return ATTACK;
        }

        /**
         * @returns The unit's defense.
         */
        public int getDefense() {
            return DEFENSE;
        }

        /**
         * @returns The number of remaining movement points for this round.
         */
        public int getRemainingMovementPoints() {
            return this.remainingMovementPoints;
        }

        /**
         * @returns The player owner of the unit.
         */
        public IPlayer getOwner() {
            return this.owner;
        }

        /**
         * @param owner The new unit's owner.
         */
        public void setOwner(IPlayer owner) {
            this.owner = owner;
        }

        /**
         * Reset the remaining movement points to the default number.
         */
        public void resetMovementPoints() {
            this.remainingMovementPoints = DEFAULT_MOVEMENT_POINTS;
        }

        /**
         * Checks if the unit is alive.
         * @returns True if the unit is alive.
         */
        public bool isAlive() {
            return this.lifePoints > 0;
        }

        /**
         * @param square The type of square the unit is currently on.
         * @param neighbours The neighbour squares (array of 4 squares or null if out bounds).
         * @returns The points won by the unit for this round.
         */
        public abstract int getPoints(ISquare square, ISquare[] neighbours);

        /**
         * Move the unit to its destination point and update the number of remaining points.
         * @param destination The type of square the destination is.
         */
        public virtual void move(ISquare destination) {
            this.remainingMovementPoints -= 2;
        }

        /**
         * Checks if the unit can move during this round to a certain destination.
         * The destination must be next to the current position,
         * the unit must have some movement points left,
         * the square can't be a sea.
         * @param currentPosition The current position.
         * @param destination The destination to reach.
         * @param square The type of square the destination is.
         * @returns True if the unit can move to the destination.
         */
        public virtual bool canMove(IPoint currentPosition, ISquare currentSquare, IPoint destination, ISquare square) {
            return destination.isNext(currentPosition) 
                && remainingMovementPoints > 0
                && !(square is ISea);
        }
    }
}