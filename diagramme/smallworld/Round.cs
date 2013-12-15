using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld
{
    public class Round : IRound
    {
        IGame game;

        IUnit selectedUnit;
        Point destination;

        public Round(IGame game)
        {
            this.game = game;
        }

        public List<Point> getAdvisedDestinations(IUnit unit, Point position)
        {
            //TODO from wrapper
            return new List<Point>();
        }

        public void selectUnit(IUnit unit)
        {
            this.selectedUnit = unit;
        }

        public bool setDestination(Point destination)
        {
            if (this.selectedUnit == null)
            {
                return false;
            }

            Boolean result = selectedUnit.canMove(destination);
            if (result)
            {
                this.destination = destination;
            }

            return result;




        }

        public bool unselectUnit(IUnit unit)
        {
            // TODO : WHAT ???
            throw new NotImplementedException();
        }

        public List<IUnit> getUnits(Point position)
        {
            return game.getMap().getUnits(position);
        }
    }
}
