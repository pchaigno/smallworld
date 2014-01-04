using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld {

    public class Point {
        public int X {
            get;
            set;
        }
        public int Y {
            get;
            set;
        }

        /**
         * Constructor
         * @param x The abscissa.
         * @param y The ordinate.
         */
        public Point(int x, int y) {
            this.X = x;
            this.Y = y;
        }

        /**
         * Checks if a position is adjacent to the current one.
         * @param pt The position.
         * @returns True if the two positions are adjacent.
         */
        public bool isNext(Point pt) {
            return Math.Abs(this.X - pt.X) + Math.Abs(this.Y - pt.Y) == 1;
        }

        public override bool Equals(object obj) {
            if(obj == null) {
                return false;
            }
            if(!(obj is Point)) {
                return false;
            }
            Point pt = (Point)obj;
            return this.X == pt.X && this.Y == pt.Y;
        }

        public override int GetHashCode() {
            int hash = 17;
            hash = hash * 23 + X.GetHashCode();
            hash = hash * 23 + Y.GetHashCode();
            return hash;
        }
    }
}
