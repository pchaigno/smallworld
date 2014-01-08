using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld {

    public interface IPoint {
        int X {
            get;
            set;
        }
        int Y {
            get;
            set;
        }

        bool IsNext(IPoint pt);
    }
}