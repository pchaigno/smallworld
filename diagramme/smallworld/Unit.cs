using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld
{
    public class Unit : IUnit
    {
        protected int attack;
        protected int defense;
        protected int lifePoints;
        protected IPlayer owner;
        protected int movementPoints;
        protected int remainingMovementPoints;
        protected Point position;
        protected ISquare square;


        public Unit(IPlayer owner)
        {
            this.owner = owner;
            attack = 2;
            defense = 1;
            lifePoints = 5;
            movementPoints = 2;
            remainingMovementPoints = movementPoints;
            this.square = null;
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

        public virtual int getPoint()
        {
            if (square is ISea)
                return 0;
            else
                return 1;
        }
        

        public virtual void move(ISquare destinationSquare, Point destination)
        {
            this.square = destinationSquare;
            this.position = destination;
            remainingMovementPoints -= 2;
        }

        public virtual Boolean canMove(Point destination, ISquare destinationSquare)
        {
            return isNext(destination, position) && remainingMovementPoints > 0 && !(destinationSquare is ISea);
        }

        public Boolean isNext(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) == 1;
        }
    }
}
