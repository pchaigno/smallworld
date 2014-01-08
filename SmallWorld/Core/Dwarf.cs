using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmallWorld {
    
    [Serializable()]
    public class Dwarf: Unit, IDwarf {

        /// <summary>
        /// Empty constructor.
        /// </summary>
        /// <param name="owner">The unit's owner.</param>
        public Dwarf(IPlayer owner): base(owner) {

        }

        /// <summary>
        /// The constructor for the deserialization.
        /// </summary>
        /// <param name="info">Information for the serialization.</param>
        /// <param name="context">The context for the serialization.</param>
        public Dwarf(SerializationInfo info, StreamingContext context): base(info, context) {

        }

        /// <summary>
        /// Computes the points won by the unit.
        /// </summary>
        /// <remarks>
        /// Dwarfs win twice the points when they are on a forest;
        /// they don't win any if they are on a sea or on a lowland square.
        /// </remarks>
        /// <param name="square">The type of square the unit is currently on.</param>
        /// <param name="neighbours">The neighbour squares (array of 4 squares or null if out bounds).</param>
        /// <returns>The number of points won by the unit depending on the square she's on.</returns>
        public override int GetPoints(ISquare square, ISquare[] neighbours) {
            if(square is IForest) {
                return 2;
            } else if(square is ISea || square is ILowland) {
                return 0;
            } else {
                return 1;
            }
        }

        /// <summary>
        /// Checks if the unit can move during this round to a certain destination.
        /// </summary>
        /// <remarks>
        /// Dwarfs have the particularity that they can't move on mountains.
        /// </remarks>
        /// <param name="currentPosition">The current position.</param>
        /// <param name="currentSquare">The current type of square.</param>
        /// <param name="destination">The destination to reach.</param>
        /// <param name="square">The type of square the destination is.</param>
        /// <returns>True if the unit can move to the destination.</returns>
        public override bool CanMove(IPoint currentPosition, ISquare currentSquare, IPoint destination, ISquare square) {
            return base.CanMove(currentPosition, currentSquare, destination, square)
                || (currentSquare is IMountain && square is IMountain);
        }
    }
}