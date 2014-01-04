using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld {

    public class Player: IPlayer {
        private IUnitFactory factory;
        private string name;
        private int points;
        private int number;
        private static int count = 0;

        /**
         * Constructor
         * @param name The name of the player.
         * @param factory The factory for this player (represent the nation).
         */
        public Player(string name, IUnitFactory factory) {
            this.name = name;
            this.factory = factory;
            count++;
            this.number = count;
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
         * @returns The number assigned to the player.
         */
        public int getNumber() {
            return this.number;
        }

        /**
         * @returns The number associated to the nation of the player.
         */
        public int getNationNumber() {
            return this.factory.getNumber();
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
         * @throws ArgumentOutOfRangeException If n if negative.
         */
        public void addPoints(int n) {
            if(n < 0) {
                throw new ArgumentOutOfRangeException("Only a positive number of points can be added to a player.");
            }
            this.points += n;
        }

        public override bool Equals(Object obj) {
            if(obj == null) {
                return false;
            }
            if(!(obj is Player)) {
                return false;
            }
            Player player = (Player)obj;
            return this.number == player.number;
        }

        public override int GetHashCode() {
            return this.number.GetHashCode();
        }
    }
}