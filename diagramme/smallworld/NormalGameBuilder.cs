using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class NormalGameBuilder : GameBuilder, INormalGameBuilder
    {
        public NormalGameBuilder()
        {
            max_rounds = 10;
            nb_units = 4;
            map_size = 5;
        }
    }
}
