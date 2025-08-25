using ModularilyBased.API.Register;
using ModularilyBased.Functionality;

namespace ModularilyBased.API.Buildable.PlaceRule
{
    /**
     * <summary>
     * Describes under which conditions a snapping module might be allowed to be placed.
     * </summary>
     */
    public class PlacementRule
    {
        /// <summary>
        /// Default <see cref="PlacementRule"/>, allowing snapping to a wall (includes walls in the corridor)
        /// </summary>
        public static readonly PlacementRule SnapToWall = new PlacementRule(FaceType.Wall);
        /// <summary>
        /// Default <see cref="PlacementRule"/>, allowing snapping to the center
        /// </summary>
        public static readonly PlacementRule SnapToCenter = new PlacementRule(FaceType.Center);
        /// <summary>
        /// Default <see cref="PlacementRule"/>, allowing snapping to a ladder point
        /// </summary>
        public static readonly PlacementRule SnapToLadder = new PlacementRule(FaceType.Ladder);
        /// <summary>
        /// Default <see cref="PlacementRule"/>, allowing snapping to a corridor's cap (where corridors may connect)
        /// </summary>
        public static readonly PlacementRule SnapToCorridorCap = new PlacementRule(FaceType.CorridorCap);
        /// <summary>
        /// Default <see cref="PlacementRule"/>, allowing snapping to an ACU's side
        /// </summary>
        public static readonly PlacementRule SnapToWaterPark = new PlacementRule(FaceType.WaterPark);

        /**
         * <summary>
         * Snap to any Large Room center face, as long as it fits a two-face wide module. (e.g. size of the Alien Containment Unit)
         * </summary>
         */
        public static readonly PlacementRule LargeRoomDoubleFace = new PlacementRule(FaceType.Center, 2);

        /**
         * <summary>
         * Snap to any Large Room center face, as long as it fits a three-face wide module.
         * </summary>
         */
        public static readonly PlacementRule LargeRoomTripleFace = new PlacementRule(FaceType.Center, 3);

        /**
         * <summary>
         * Snap to any Large Room center face, as long as it fits a four-face wide module.
         * </summary>
         */
        public static readonly PlacementRule LargeRoomQuadrupleFace = new PlacementRule(FaceType.Center, 4);

        internal readonly FaceType snap = FaceType.None;
        internal readonly int requiredFaces = 1;

        /**
         * <summary>
         * Set the snapping rule to <paramref name="snap"/> and the amount of faces required to fit to <paramref name="snap"/> (e.g. Bioreactor and Nuclear Reactor require 1, while the Alien Containment Unit requires 2).
         * </summary>
         */
        public PlacementRule(FaceType snap, int requiredFaces = 1)
        {
            this.snap = snap;
            this.requiredFaces = requiredFaces;
        }

        internal virtual bool CanBuildOn(ModuleSnapper snapper, BaseFaceIdentifier identifier)
        {
            if (requiredFaces != identifier.RequiredModuleSize
                || !CanSnapTo(identifier.GetSnapType())
                )
            {
                return false;
            }
            
            return true;
        }

        internal bool CanSnapTo(FaceType type)
        {
            return snap == type;
        }
    }
}
