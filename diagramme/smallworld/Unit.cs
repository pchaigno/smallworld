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

        protected Unit(IPlayer owner) {
            this.owner = owner;
            attack = 2;
            defense = 1;
            lifePoints = 5;
            maxLifePoints = 5;
            movementPoints = 2;
            remainingMovementPoints = movementPoints;
        }

        public int getRemainingMovementPoints() {
            return remainingMovementPoints;
        }

        public IPlayer getOwner() {
            return owner;
        }

        public void resetMovementPoints() {
            remainingMovementPoints = movementPoints;
        }

        public Point getPosition() {
            return position;
        }

        public Boolean isAlive() {
            return lifePoints > 0;
        }

        public void setPosition(Point p, Dictionary<Point, ISquare> squares) {
            this.position = p;
            this.squares = squares;
        }

        public void terminate() {
            // TODO destructeur
        }

        protected Boolean isNext(Point a, Point b) {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) == 1;
        }

        public virtual int getPoint() {
            if(squares[position] is ISea) {
                return 0;
            } else {
                return 1;
            }
        }

        public virtual void move(Point destination) {
            this.position = destination;
            remainingMovementPoints -= 2;
        }

        public virtual Boolean canMove(Point destination) {
            return isNext(destination, position) 
                && remainingMovementPoints > 0 
                && !(squares[destination] is ISea);
        }
    }
}