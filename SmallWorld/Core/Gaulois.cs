using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmallWorld {
    
    [Serializable()]
    public class Gaulois: Unit, IGaulois {

        /// <summary>
        /// Empty constructor.
        /// </summary>
        /// <param name="owner">The unit's owner.</param>
        public Gaulois(IPlayer owner): base(owner) {

        }

        /// <summary>
        /// Constructor for the deserialization.
        /// </summary>
        /// <param name="info">Information for the serialization.</param>
        /// <param name="context">The context for the serialization.</param>
        public Gaulois(SerializationInfo info, StreamingContext context): base(info, context) {

        }

        /// <summary>
        /// Computes the points won by the unit.
        /// </summary>
        /// <remarks>
        /// Gaulois win twice the points if they are on lowland;
        /// they don't win any if they are on a mountain or on the sea.
        /// </remarks>
        /// <param name="square">The type of square the unit is currently on.</param>
        /// <param name="neighbours">The neighbour squares (array of 4 squares or null if out bounds).</param>
        /// <returns>The number of points won by the unit depending on the square she's on.</returns>
        public override int GetPoints(ISquare square, ISquare[] neighbours) {
            if(square is ILowland) {
                return 2;
            } else if(square is ISea || square is IMountain) {
                return 0;
            } else {
                return 1;
            }
        }

        /// <summary>
        /// Moves the unit to its destination point and update the number of remaining points.
        /// </summary>
        /// <remarks>
        /// Gaulois use one point less than others to move on lowland.
        /// </remarks>
        /// <param name="destination">The type of square the destination is.</param>
        public override void Move(ISquare destination) {
            base.Move(destination);
            if(destination is ILowland) {
                this.remainingMovementPoints++;
            }
        }
    }
}