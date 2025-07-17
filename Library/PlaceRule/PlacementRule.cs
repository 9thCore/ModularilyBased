namespace ModularilyBased.Library.PlaceRule
{
    /**
     * <summary>
     * Describes under which conditions a snapping module might be allowed to be placed.
     * </summary>
     */
    public class PlacementRule
    {
        public static readonly PlacementRule SnapToWall = new PlacementRule(SnapType.Wall);
        public static readonly PlacementRule SnapToCenter = new PlacementRule(SnapType.Center);
        public static readonly PlacementRule SnapToLadder = new PlacementRule(SnapType.Ladder);
        public static readonly PlacementRule SnapToCorridorCap = new PlacementRule(SnapType.CorridorCap);
        public static readonly PlacementRule SnapToWaterPark = new PlacementRule(SnapType.WaterParkSide);

        /**
         * <summary>
         * Snap to any Large Room center face, as long as it fits a two-face wide module. (e.g. size of the Alien Containment Unit)
         * </summary>
         */
        public static readonly PlacementRule LargeRoomDoubleFace = new PlacementRule(SnapType.Center, 2);

        /**
         * <summary>
         * Snap to any Large Room center face, as long as it fits a three-face wide module.
         * </summary>
         */
        public static readonly PlacementRule LargeRoomTripleFace = new PlacementRule(SnapType.Center, 3);

        /**
         * <summary>
         * Snap to any Large Room center face, as long as it fits a four-face wide module.
         * </summary>
         */
        public static readonly PlacementRule LargeRoomQuadrupleFace = new PlacementRule(SnapType.Center, 4);

        public readonly SnapType snap = SnapType.None;
        public readonly int requiredFaces = 1;

        /**
         * <summary>
         * Set the snapping rule to <paramref name="snap"/> and the amount of faces required to fit to <paramref name="snap"/> (e.g. Bioreactor and Nuclear Reactor require 1, while the Alien Containment Unit requires 2).
         * </summary>
         */
        public PlacementRule(SnapType snap, int requiredFaces = 1)
        {
            this.snap = snap;
            this.requiredFaces = requiredFaces;
        }

        public virtual bool CanBuildOn(ModuleSnapper snapper, BaseFaceIdentifier identifier)
        {
            if (requiredFaces != identifier.SeabaseFaces.Length
                || !CanSnapTo(identifier.GetSnapType())
                )
            {
                return false;
            }
            
            return true;
        }

        public bool CanSnapTo(SnapType type)
        {
            return snap == type;
        }

        public enum SnapType
        {
            None,
            Wall,
            Center,
            CorridorCap,
            Ladder,
            WaterParkSide
        }
    }
}
