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
        /// <returns>False if the unit couldn't be move to that destination.</returns>
        public override bool Move(ISquare destination) {
            if(destination is ILowland) {
                int movementCost = MOVEMENT_COST / 2;
                if(this.remainingMovementPoints < movementCost) {
                    return false;
                }
                this.remainingMovementPoints -= movementCost;
                return true;
            }
            return base.Move(destination);
        }

        /// <summary>
        /// Checks if the unit can move during this round to a certain destination.
        /// The destination must be next to the current position,
        /// the unit must have some movement points left,
        /// the square can't be a sea.
        /// </summary>
        /// <param name="currentPosition">The current position.</param>
        /// <param name="currentSquare">The current type of square.</param>
        /// <param name="destination">The destination to reach.</param>
        /// <param name="square">The type of square the destination is.</param>
        /// <returns>True if the unit can move to the destination.</returns>
        public virtual bool CanMove(IPoint currentPosition, ISquare currentSquare, IPoint destination, ISquare square) {
            return !(square is ISea)
                && destination.IsNext(currentPosition)
                && (remainingMovementPoints >= MOVEMENT_COST 
                    || ((square is ILowland) && remainingMovementPoints>=MOVEMENT_COST/2));
        }
    }
}