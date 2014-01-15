using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallWorld;
using mWrapper;

namespace SmallWorld {

    public abstract class GameBuilder: IGameBuilder {
        protected int maxRounds;
        protected int mapSize;
        protected int nbUnits;

        /// <summary>
        /// Build the game.
        /// Creates the game manager with the right players, units and map.
        /// </summary>
        /// <remarks>
        /// The map is created in the C++ library as for the unit positions.
        /// </remarks>
        /// <param name="name1">The first player's name.</param>
        /// <param name="factory1">The factory for the first player.</param>
        /// <param name="name2">The second player's name.</param>
        /// <param name="factory2">The factory for the second player.</param>
        /// <returns>The game manager.</returns>
        public IGame BuildGame(string name1, IUnitFactory factory1, string name2, IUnitFactory factory2) {
            // Builds the map using the MapBuilder:
            IMap map = MapBuilder.Instance.BuildMap(this.mapSize);
            ITile[,] tiles = map.Tiles;
            int[][] mapAsIntegers = TileFactory.GetNumbers(tiles);

            IPlayer player1 = new Player(name1, factory1);
            IPlayer player2 = new Player(name2, factory2);

            // Retrieves the starting points from the wrapper:
            int[][] starts = Wrapper.getStartsPlayers(mapAsIntegers, this.mapSize);
            IPoint startPlayer1 = new Point(starts[0][0], starts[0][1]);
            IPoint startPlayer2 = new Point(starts[1][0], starts[1][1]);

            // Creates and places the units for each player:
            List<IUnit> units1 = player1.CreateUnits(this.nbUnits);
            for(int i = 0; i < this.nbUnits; i++) {
                map.PlaceUnit(units1[i], startPlayer1);
            }
            List<IUnit> units2 = player2.CreateUnits(this.nbUnits);
            for(int i = 0; i < this.nbUnits; i++) {
                map.PlaceUnit(units2[i], startPlayer2);
            }

            return new Game(player1, player2, map, this.maxRounds);
        }
    }
}