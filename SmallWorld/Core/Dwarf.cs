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
        /// they don't win any if they are on a sea or on a lowland tile.
        /// </remarks>
        /// <param name="tile">The type of tile the unit is currently on.</param>
        /// <param name="neighbours">The neighbour tiles (array of 4 tiles or null if out bounds).</param>
        /// <returns>The number of points won by the unit depending on the tile she's on.</returns>
        public override int GetPoints(ITile tile, ITile[] neighbours) {
            if(tile is IForest) {
                return 2;
            } else if(tile is ISea || tile is ILowland) {
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
        /// <param name="currentTile">The current type of tile.</param>
        /// <param name="destination">The destination to reach.</param>
        /// <param name="tile">The type of tile the destination is.</param>
        /// <returns>True if the unit can move to the destination.</returns>
        public override bool CanMove(IPoint currentPosition, ITile currentTile, IPoint destination, ITile tile, bool occupied) {
            return base.CanMove(currentPosition, currentTile, destination, tile, occupied)
                || (currentTile is IMountain && tile is IMountain && !occupied);
        }
    }
}