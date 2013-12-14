using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class SmallGameBuilder : GameBuilder, ISmallGameBuilder
    {
        public SmallGameBuilder()
        {
            max_rounds = 20;
            nb_units = 6;
            map_size = 10;
        }
    }
}
