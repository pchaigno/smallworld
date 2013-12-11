using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class DwarfFactory : IDwarfFactory
    {
        public const int DEFAULT_ATTACK = 1;
        public const int DEFAULT_DEFENSE = 1;
        public const int DEFAULT_LIFEPOINTS = 1;
    
        public IUnit createUnit()
        {
            throw new NotImplementedException();
        }
    }
}
