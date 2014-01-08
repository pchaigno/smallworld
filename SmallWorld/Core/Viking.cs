using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace SmallWorld {

    [Serializable()]
    public class Viking: Unit, IViking {

        /// <summary>
        /// Empty constructor.
        /// </summary>
        /// <param name="owner">Owner of this unit.</param>
        public Viking(IPlayer owner): base(owner) {

        }

        /// <summary>
        /// Constructor for deserialization.
        /// </summary>
        /// <param name="info">The information for the serialization.</param>
        /// <param name="context">The context for the serialization.</param>
        public Viking(SerializationInfo info, StreamingContext context): base(info, context) {

        }

        /// <summary>
        /// Computes the points won by the unit.
        /// </summary>
        /// <remarks>
        /// Vikings win one more point when they are next to the sea;
        /// they don't win any if they are on the sea or in the desert.
        /// </remarks>
        /// <param name="square">The type of square the unit is currently on.</param>
        /// <param name="neighbours">The neighbour squares (array of 4 squares or null if out bounds).</param>
        /// <returns>The number of points won by the unit depending on the square she's on.</returns>
        public override int GetPoints(ISquare square, ISquare[] neighbours) {
            int points = 1;
            if(square is ILowland) {
                points = 2;
            } else if(square is ISea || square is IDesert) {
                points = 0;
            }
            foreach(ISquare neighbour in neighbours) {
                if(neighbour is ISea) {
                    points++;
                    break;
                }
            }
            return points;
        }

        /// <summary>
        /// Checks if the unit can move during this round to a certain destination.
        /// </summary>
        /// <remarks>
        /// The destination must be next to the current position,
        /// the unit must have some movement points left.
        /// Contrary to must units, viking can move on the sea.
        /// </remarks>
        /// <param name="currentPosition">The current position.</param>
        /// <param name="currentSquare">The current type of square.</param>
        /// <param name="destination">The destination to reach.</param>
        /// <param name="square">The type of square the destination is.</param>
        /// <returns>True if the unit can move to the destination.</returns>
        public override bool CanMove(IPoint currentPosition, ISquare currentSquare, IPoint destination, ISquare square) {
            return destination.IsNext(currentPosition) 
                && remainingMovementPoints>0;
        }
    }
}