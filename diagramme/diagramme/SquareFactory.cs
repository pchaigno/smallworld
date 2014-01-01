using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public class SquareFactory : ISquareFactory
    {
        public IMountain mountain;

        public ILowland lowland;

        public IForest forest;

        public ISea sea;

        public IDesert desert;

        private SquareFactory()
        {
            throw new System.NotImplementedException();
        }
    
        public IDesert getDesert()
        {
            throw new NotImplementedException();
        }

        public ISea getSea()
        {
            throw new NotImplementedException();
        }

        public IMountain getMountain()
        {
            throw new NotImplementedException();
        }

        public IForest getForest()
        {
            throw new NotImplementedException();
        }

        public ILowland getLowland()
        {
            throw new NotImplementedException();
        }


        public ISquareFactory getInstance()
        {
            throw new NotImplementedException();
        }
    }
}
