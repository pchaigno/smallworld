using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class Player: IPlayer {
        private IUnitFactory factory;
        private string name;
        private int points;

        /**
         * Constructor
         * @param name The name of the player.
         * @param factory The factory for this player (represent the nation).
         */
        public Player(string name, IUnitFactory factory) {
            this.name = name;
            this.factory = factory;
        }

        /**
         * @returns The player's name.
         */
        public string getName() {
            return this.name;
        }

        /**
         * @returns The number of points collected by the player.
         */
        public int getPoints() {
            return this.points;
        }

        /**
         * Creates some units using the factory of the player.
         * @param nbUnits The number of units to create.
         * @returns The list of units created.
         */
        public List<IUnit> createUnits(int nbUnits) {
            List<IUnit> units = new List<IUnit>();
            for(int i=0; i<nbUnits; i++) {
                units.Add(factory.createUnit(this));
            }
            return units;
        }

        /**
         * Add some points to the player.
         * @param n The number of points to add.
         */
        public void addPoints(int n) {
            this.points += n;
        }
    }
}