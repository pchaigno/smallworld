using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmallWorld {

    public interface ISquare: ISerializable {
        int Number {
            get;
        }
    }
}