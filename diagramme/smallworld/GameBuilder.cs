using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallWorld;
using System.Drawing;

namespace SmallWorld {

    public abstract class GameBuilder: IGameBuilder {
        protected int max_rounds;
        protected int map_size;
        protected int nb_units;

        public IGame buildGame(String name1, IUnitFactory factory1, String name2, IUnitFactory factory2) {
            IMapBuilder mapBuilder = new MapBuilder();
            IMap map = mapBuilder.buildMap(map_size);
            IPlayer player1 = new Player(name1, factory1);
            IPlayer player2 = new Player(name2, factory2);

            Dictionary<Point, ISquare> squares = map.getSquares();

            //TODO GET start from Wrapper
            Point start1 = new Point(0, 0);
            List<IUnit> units1 = player1.createUnits(nb_units);
            for(int i = 0; i < nb_units; i++) {
                map.placeUnit(units1[i], start1);
                units1[i].setPosition(start1, squares);
            }

            //TODO GET start from Wrapper
            //Point start2 = new Point(map_size - 1, map_size - 1);
            Point start2 = new Point(0, 1);

            List<IUnit> units2 = player2.createUnits(nb_units);
            for(int i = 0; i < nb_units; i++) {
                map.placeUnit(units2[i], start2);
                units2[i].setPosition(start2, squares);
            }

            return new Game(player1, player2, map, max_rounds);
        }
    }
}