using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallWorld;

namespace SmallWorld
{
    public abstract class GameBuilder : IGameBuilder
    {
        protected int max_rounds;
        protected int map_size;
        protected int nb_units;


        public IGame buildGame(String name1, IUnitFactory factory1, String name2, IUnitFactory factory2)
        {
            IMapBuilder mapBuilder = new MapBuilder();
            IMap map = mapBuilder.buildMap(map_size);
            IPlayer player1 = new Player(name1, factory1);
            IPlayer player2 = new Player(name2, factory2);

            //Create & place unit

            return new Game(player1, player2, map, max_rounds);
        }


    }
}
