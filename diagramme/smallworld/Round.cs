using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld
{
    public class Round : IRound
    {
        private IGame game;
        private IPlayer player;

        private IUnit selectedUnit;
        private Point destination;

        private String lastMoveInfo;

        public Round(IGame game, IPlayer player)
        {
            this.game = game;
            this.player = player;
            lastMoveInfo = "";
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

        public void unselectUnit()
        {
            selectedUnit = null;
        }

        public List<IUnit> getUnits(Point position)
        {
            return game.getMap().getUnits(position);
        }

        public Boolean isCurrentPlayerPosition(Point position)
        {
            List<IUnit> units = game.getMap().getUnits(position);
            return units.Count > 0 && units[0].getOwner() == player;
        }

        public bool setDestination(Point destination)
        {
            if (this.selectedUnit == null)
            {
                lastMoveInfo = "You have to select a unit first.";
                return false;

            }

            Boolean result = selectedUnit.canMove(destination);
            if (result)
            {
                this.destination = destination;
            }
            else
            {
                lastMoveInfo = "You cannot move here.";
            }

            Console.WriteLine(result);

            return result;
        }

        public void executeMove() 
        {
            if (game.getMap().isEnemyPosition(destination, selectedUnit))
            {
                if (combat())
                {
                    Console.WriteLine(game.getMap().getUnits(destination).Count);
                    if (game.getMap().getUnits(destination).Count == 0)
                    {
                        game.getMap().moveUnit(selectedUnit, destination);
                    }
                }
            }
            else
            {
                game.getMap().moveUnit(selectedUnit, destination);
                lastMoveInfo = player.getName() + " moved a unit.";
            }

            selectedUnit = null;
            //destination = null; TODO find alternative or check
        }

        private Boolean combat()
        {
            IUnit enemy = getBestUnit();
            //TODO combat

            Random randCombat = new Random();
            Random rand = new Random();

            int nbRound = 3 + randCombat.Next((Math.Max(selectedUnit.lifePoints, enemy.lifePoints)) + 2);
            int n = 0;

            while (nbRound > n && selectedUnit.isAlive() && enemy.isAlive())
            {
                double ratioLife = (double)selectedUnit.lifePoints / (double)selectedUnit.maxLifePoints;
                double ratioLifeDef = (double)enemy.lifePoints / (double)enemy.maxLifePoints;
                double attaUnit = (double)selectedUnit.attack * (double)ratioLife;
                double defUnitdef = (double)enemy.defense * (double)ratioLifeDef;
                double ratioAttDef = (double)(attaUnit / defUnitdef);
                double ratioChanceDef = 0;
                if (ratioAttDef > 1) // avantage attaquant
                {
                    ratioChanceDef = (1 / ratioAttDef) / 2;
                    ratioChanceDef = (0.5 - ratioChanceDef) + 0.5;
                }
                else if (ratioAttDef == 1) //égalité, aucun n'a l'avantage
                {
                    ratioChanceDef = 0.5; // 50% de chnce de gagner
                }
                else // avantage défense
                {
                    ratioChanceDef = ratioAttDef / 2;
                }
                double ratioCombat = (double)((double)rand.Next(100) / 100);

                if (ratioCombat <= ratioChanceDef)
                {
                    enemy.lifePoints--;
                }
                else
                {
                    selectedUnit.lifePoints--;
                }
                n++;
            }

            if (!selectedUnit.isAlive())
            {
                game.getMap().removeUnit(selectedUnit, selectedUnit.getPosition());
                selectedUnit.terminate();
                lastMoveInfo = player.getName() + " lost the fight.";
                return false;
            }
            else if (!enemy.isAlive())
            {
                game.getMap().removeUnit(enemy, destination);
                enemy.terminate();
                lastMoveInfo = player.getName() + " won the fight.";
                return true;
            }
            else
            {
                lastMoveInfo = "The fight ended with a draw (Grammar ??)";
                return false;
            }

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
                    if (result.lifePoints < units[i].lifePoints)
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

        public String getLastMoveInfo()
        {
            return lastMoveInfo;
        }
    }
}
