﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public class Viking : IViking
    {
        private int attack;
        private int defense;
        private int lifePoints;

        public Viking(int defense, int attack, int lifePoints)
        {
            throw new System.NotImplementedException();
        }
    
        public int getDefense()
        {
            throw new NotImplementedException();
        }

        public int getLifePoints()
        {
            throw new NotImplementedException();
        }

        public int getAttack()
        {
            throw new NotImplementedException();
        }
    }
}
