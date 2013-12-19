using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld {

    public abstract class Unit: IUnit {
        protected const int ATTACK = 2;
        protected const int DEFENSE = 1;
        protected const int DEFAULT_LIFE_POINTS = 5;
        protected const int DEFAULT_MOVEMENT_POINTS = 2;
        protected int lifePoints;
        protected int remainingMovementPoints;
        protected IPlayer owner;
        protected Point position;

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
         * Reset the remaining movement points to the default number.
         */
        public void resetMovementPoints() {
            this.remainingMovementPoints = DEFAULT_MOVEMENT_POINTS;
        }

        /**
         * @returns The unit's position.
         */
        public Point getPosition() {
            return this.position;
        }

        /**
         * Checks if the unit is alive.
         * @returns True if the unit is alive.
         */
        public Boolean isAlive() {
            return this.lifePoints > 0;
        }

        /**
         * Sets the position of the unit and
         * its copy of the composition of the map.
         * @param position The unit's position.
         * @param squares The copy of the map's composition.
         */
        // TODO Can't it be in the constructor?
        public void setPosition(Point position) {
            this.position = position;
        }

        /**
         * Checks if two positions are adjacent.
         * @param a The first position.
         * @param b The second position.
         * @returns True if the two positions are adjacent.
         */
        protected static bool isNext(Point a, Point b) {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) == 1;
        }

        /**
         * @param position The type of square the unit is currently on.
         * @returns The points won by the unit for this round.
         */
        public abstract int getPoint(ISquare square);

        /**
         * Move the unit to its destination point and update the number of remaining points.
         * @param destination The destination for the unit.
         * @param square The type of square the destination is.
         */
        public virtual void move(Point destination, ISquare square) {
            this.position = destination;
            this.remainingMovementPoints -= 2;
        }

        /**
         * Checks if the unit can move during this round to a certain destination.
         * The destination must be next to the current position,
         * the unit must have some movement points left,
         * the square can't be a sea.
         * @param destination The destination to reach.
         * @param square The type of square the destination is.
         * @returns True if the unit can move to the destination.
         */
        public virtual bool canMove(Point destination, ISquare square) {
            return isNext(destination, this.position) 
                && remainingMovementPoints > 0
                && !(square is ISea);
        }
    }
}