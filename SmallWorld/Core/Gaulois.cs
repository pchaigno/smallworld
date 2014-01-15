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
        /// <param name="tile">The type of tile the unit is currently on.</param>
        /// <param name="neighbours">The neighbour tiles (array of 4 tiles or null if out bounds).</param>
        /// <returns>The number of points won by the unit depending on the tile she's on.</returns>
        public override int GetPoints(ITile tile, ITile[] neighbours) {
            if(tile is ILowland) {
                return 2;
            } else if(tile is ISea || tile is IMountain) {
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
        /// <param name="destination">The type of tile the destination is.</param>
        /// <returns>False if the unit couldn't be move to that destination.</returns>
        public override bool Move(ITile destination) {
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
        /// the tile can't be a sea.
        /// </summary>
        /// <param name="currentPosition">The current position.</param>
        /// <param name="currentTile">The current type of tile.</param>
        /// <param name="destination">The destination to reach.</param>
        /// <param name="tile">The type of tile the destination is.</param>
        /// <returns>True if the unit can move to the destination.</returns>
        public virtual bool CanMove(IPoint currentPosition, ITile currentTile, IPoint destination, ITile tile, bool occupied) {
            return !(tile is ISea)
                && destination.IsNext(currentPosition)
                && (remainingMovementPoints >= MOVEMENT_COST
                    || ((tile is ILowland) && remainingMovementPoints >= MOVEMENT_COST / 2));
        }
    }
}