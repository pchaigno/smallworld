using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld {

    public abstract class Unit: IUnit {
        public int attack {
            get;
            set;
        }
        public int defense {
            get;
            set;
        }
        public int lifePoints {
            get;
            set;
        }
        public int maxLifePoints {
            get;
            set;
        }
        protected IPlayer owner;
        protected int movementPoints;
        protected int remainingMovementPoints;
        protected Point position;
        protected Dictionary<Point, ISquare> squares;

        /**
         * Constructor
         * @param owner The player owner of the unit.
         */
        // TODO Why is it protected?
        // TODO Shouldn't default parameters by defined as constants?
        protected Unit(IPlayer owner) {
            this.owner = owner;
            attack = 2;
            defense = 1;
            lifePoints = 5;
            maxLifePoints = 5;
            movementPoints = 2;
            remainingMovementPoints = movementPoints;
        }

        /**
         * @returns The number of remaining movement points for this round.
         */
        public int getRemainingMovementPoints() {
            return remainingMovementPoints;
        }

        /**
         * @returns The player owner of the unit.
         */
        public IPlayer getOwner() {
            return owner;
        }

        /**
         * Reset the remaining movement points to the default number.
         */
        public void resetMovementPoints() {
            remainingMovementPoints = movementPoints;
        }

        /**
         * @returns The unit's position.
         */
        public Point getPosition() {
            return position;
        }

        /**
         * Checks if the unit is alive.
         * @returns True if the unit is alive.
         */
        public Boolean isAlive() {
            return lifePoints > 0;
        }

        /**
         * 
         */
        public void setPosition(Point p, Dictionary<Point, ISquare> squares) {
            this.position = p;
            this.squares = squares;
        }

        /**
         * TODO
         */
        public void terminate() {
            // TODO destructeur
        }

        /**
         * Checks if two positions are adjacent.
         * @param a The first position.
         * @param b The second position.
         * @returns True if the two positions are adjacent.
         */
        // TODO Shouldn't it be static?
        protected Boolean isNext(Point a, Point b) {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) == 1;
        }

        // TODO Is that usefull?
        public virtual int getPoint() {
            if(squares[position] is ISea) {
                return 0;
            } else {
                return 1;
            }
        }

        /**
         * Move the unit to its destination point and update the number of remaining points.
         * @param destination The destination for the unit.
         */
        public virtual void move(Point destination) {
            this.position = destination;
            remainingMovementPoints -= 2;
        }

        /**
         * Checks if the unit can move during this round to a certain destination.
         * The destination must be next to the current position,
         * the unit must have some movement points left,
         * the square can't be a sea.
         * @param destination The destination to reach.
         * @returns True if the unit can move to the destination.
         */
        public virtual Boolean canMove(Point destination) {
            return isNext(destination, position) 
                && remainingMovementPoints > 0 
                && !(squares[destination] is ISea);
        }
    }
}