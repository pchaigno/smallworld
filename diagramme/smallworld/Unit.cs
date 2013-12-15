using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld
{
    public abstract class Unit : IUnit
    {
        protected int attack;
        protected int defense;
        protected int lifePoints;
        protected IPlayer owner;
        protected int movementPoints;
        protected int remainingMovementPoints;
        protected Point position;
        protected Dictionary<Point, ISquare> squares;


        protected Unit(IPlayer owner)
        {
            this.owner = owner;
            attack = 2;
            defense = 1;
            lifePoints = 5;
            movementPoints = 2;
            remainingMovementPoints = movementPoints;
        }

        public int getDefense()
        {
            return defense;
        }

        public int getLifePoints()
        {
            return lifePoints;
        }

        public int getAttack()
        { 
            return attack;
        }

        public int getRemainingMovementPoints()
        {
            return remainingMovementPoints;
        }

        public IPlayer getOwner()
        {
            return owner;
        }

        public void resetMovementPoints()
        {
            remainingMovementPoints = movementPoints;
        }

        public void setPosition(Point p, Dictionary<Point, ISquare> squares)
        {
            this.position = p;
            this.squares = squares;
        }

        protected Boolean isNext(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) == 1;
        }

        public virtual int getPoint()
        {
            if (squares[position] is ISea)
                return 0;
            else
                return 1;
        }

        public virtual void move(Point destination)
        {
            this.position = destination;
            remainingMovementPoints -= 2;
        }

        public virtual Boolean canMove(Point destination, ISquare destinationSquare)
        {
            return isNext(destination, position) && remainingMovementPoints > 0 && !(destinationSquare is ISea);
        }
    }
}
