﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallWorld {

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
        /// Checks if a position is adjacent to the current one.
        /// </summary>
        /// <param name="pt">The position.</param>
        /// <returns>True if the two positions are adjacent.</returns>
        public bool IsNext(IPoint pt) {
            return Math.Abs(this.X - pt.X) + Math.Abs(this.Y - pt.Y) == 1;
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
