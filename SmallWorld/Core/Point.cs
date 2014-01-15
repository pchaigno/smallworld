using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SmallWorld {

    [Serializable()]
    public class Point: IPoint {
        public int X {
            get;
            set;
        }
        public int Y {
            get;
            set;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">The abscissa.</param>
        /// <param name="y">The ordinate.</param>
        public Point(int x, int y) {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Constructor for the deserialization.
        /// </summary>
        /// <param name="info">Information for the serialization.</param>
        /// <param name="context">The context for the serialization.</param>
        public Point(SerializationInfo info, StreamingContext context) {
            this.X = (int)info.GetValue("X", typeof(int));
            this.Y = (int)info.GetValue("Y", typeof(int));
        }

        /// <summary>
        /// Method for the serialization.
        /// Fills info with the attributs' values.
        /// </summary>
        /// <param name="info">Information for the serialization.</param>
        /// <param name="context">The context for the serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("X", this.X);
            info.AddValue("Y", this.Y);
        }

        /// <summary>
        /// Checks if a position is adjacent to the current one.
        /// </summary>
        /// <param name="pt">The position.</param>
        /// <returns>True if the two positions are adjacent.</returns>
        public bool IsNext(IPoint pt) {
            return Math.Abs(this.X - pt.X) + Math.Abs(this.Y - pt.Y) == 1;
        }

        /// <summary>
        /// Checks if a position is valid; it's valid if it is on the map.
        /// </summary>
        /// <param name="size">The size of the map.</param>
        /// <returns>True if the position is on the map.</returns>
        public bool isValid(int size) {
            if(this.X<0 && this.X>=size) {
                return false;
            }
            if(this.Y<0 && this.Y>=size) {
                return false;
            }
            return true;
        }

        public override bool Equals(object obj) {
            if(obj == null) {
                return false;
            }
            if(!(obj is Point)) {
                return false;
            }
            IPoint pt = (Point)obj;
            return this.X == pt.X && this.Y == pt.Y;
        }

        public override int GetHashCode() {
            int hash = 17;
            hash = hash * 23 + this.X.GetHashCode();
            hash = hash * 23 + this.Y.GetHashCode();
            return hash;
        }
    }
}
