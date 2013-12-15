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



        private void attaquer()
        {
            Random randCombat = new Random();
            Random rand = new Random();
            int nbToursCombat = 3 + randCombat.Next((Math.Max(this.PointsVie, unitDef.PointsVie)) + 2);
            int n = 0;
            //Console.WriteLine("combat nbTours "+nbToursCombat);
            while (nbToursCombat - n > 0 && this.estEnVie() && unitDef.estEnVie())
            {
                double ratioVie = (double)this.PointsVie / (double)this.PointsVieMax;
                double ratioVieDef = (double)unitDef.PointsVie / (double)unitDef.PointsVieMax;
                double attaUnit = (double)this.PointsAttaque * (double)ratioVie;
                double defUnitdef = (double)unitDef.PointsDefense * (double)ratioVieDef;
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
                    ratioChanceDef = ratioAttDef/2;
                }
                double ratioCombat = (double)((double)rand.Next(100) / 100);
                //Console.WriteLine(ratioChanceDef+" "+ratioCombat+" "+ratioVie);
                if (ratioCombat <= ratioChanceDef)
                {
                    // Console.WriteLine(unit.Proprietaire.Nom+" gagne tour " + (n+1));
                    unitDef.perdPV(1);
                }
                else
                {
                    //Console.WriteLine(unit.Proprietaire.Nom + " perd tour" + (n + 1));
                    this.perdPV(1);
                }
                n++;
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
