﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallWorld;
using mWrapper;
using System.Drawing;

namespace SmallWorld {

    // TODO Should be a singleton.
    public abstract class GameBuilder: IGameBuilder {
        // TODO Are those really necessary?
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
            ISquare[,] squares = map.getSquares();
            int[][] mapAsIntegers = SquareFactory.getNumbers(squares);

            IPlayer player1 = new Player(name1, factory1);
            IPlayer player2 = new Player(name2, factory2);

            int[][] starts = Wrapper.getStartsPlayers(mapAsIntegers, this.mapSize);
            Point startPlayer1 = new Point(starts[0][0], starts[0][1]);
            Point startPlayer2 = new Point(starts[1][0], starts[1][1]);

            List<IUnit> units1 = player1.createUnits(this.nbUnits);
            for(int i = 0; i < this.nbUnits; i++) {
                map.placeUnit(units1[i], startPlayer1);
            }


            List<IUnit> units2 = player2.createUnits(this.nbUnits);
            for(int i = 0; i < this.nbUnits; i++) {
                map.placeUnit(units2[i], startPlayer2);
            }

            return new Game(player1, player2, map, this.maxRounds);
        }
    }
}