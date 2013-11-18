using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace diagramme
{
    public interface ISquareFactory
    {

        IDesert getDesert();

        ISea getSea();

        IMountain getMountain();

        IForest getForest();

        ILowland getLowland();
    }
}
