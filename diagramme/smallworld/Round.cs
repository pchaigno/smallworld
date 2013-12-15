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

        public bool unselectUnit(IUnit unit)
        {
            // TODO : WHAT ???
            throw new NotImplementedException();
        }

        public List<IUnit> getUnits(Point position)
        {
            return game.getMap().getUnits(position);
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

        public void executeMove() 
        {
            if (game.getMap().isEnemyPosition(destination, selectedUnit))
            {
                if (combat())
                {
                    game.getMap().moveUnit(selectedUnit, destination);
                }
            }
            game.getMap().moveUnit(selectedUnit, destination);
        }

        private Boolean combat()
        {
            IUnit enemy = getBestUnit();
            //TODO combat

            game.getMap().removeUnit(enemy, destination);
            enemy.terminate();

            return true;
        }

        private IUnit getBestUnit()
        {
            IUnit result = null;
            List<IUnit> units = game.getMap().getUnits(destination);
            if (units.Count > 0)
            {
                result = units[0];
                for (int i = 1; i < units.Count; i++)
                {
                    if (result.getLifePoints() < units[i].getLifePoints())
                    {
                        result = units[i];
                    }
                }
            }
            else
            {
                //Throw exception
            }
            return result;
        }
    }
}
