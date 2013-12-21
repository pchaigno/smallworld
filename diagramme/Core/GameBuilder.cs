using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallWorld;
using System.Drawing;

namespace SmallWorld {

    public abstract class GameBuilder: IGameBuilder {
        protected int maxRounds;
        protected int mapSize;
        protected int nbUnits;

        /**
         * Build the game.
         * Creates the game manager with the right players, units and map.
         * The map is created in the C++ library as for the unit positions.
         * @param name1 The first player's name.
         * @param factory1 The factory for the first player.
         * @param name2 The second player's name.
         * @param factory2 The factory for the second player.
         * @returns The game manager.
         */
        public IGame buildGame(String name1, IUnitFactory factory1, String name2, IUnitFactory factory2) {
            IMapBuilder mapBuilder = new MapBuilder();
            IMap map = mapBuilder.buildMap(this.mapSize);
            IPlayer player1 = new Player(name1, factory1);
            IPlayer player2 = new Player(name2, factory2);

            ISquare[,] squares = map.getSquares();

            //TODO GET start from Wrapper
            Point start1 = new Point(0, 0);
            List<IUnit> units1 = player1.createUnits(this.nbUnits);
            for(int i = 0; i < this.nbUnits; i++) {
                map.placeUnit(units1[i], start1);
                units1[i].setPosition(start1);
            }

            //TODO GET start from Wrapper
            //Point start2 = new Point(map_size - 1, map_size - 1);
            Point start2 = new Point(0, 1);

            List<IUnit> units2 = player2.createUnits(this.nbUnits);
            for(int i = 0; i < this.nbUnits; i++) {
                map.placeUnit(units2[i], start2);
                units2[i].setPosition(start2);
            }

            return new Game(player1, player2, map, this.maxRounds);
        }
    }
}