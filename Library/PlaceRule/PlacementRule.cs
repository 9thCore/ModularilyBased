using System;

namespace ModularilyBased.Library.PlaceRule
{
    public class PlacementRule
    {
        public static readonly PlacementRule SnapToWall = new PlacementRule(SnapType.Wall);
        public static readonly PlacementRule SnapToCenter = new PlacementRule(SnapType.Center);
        public static readonly PlacementRule SnapToLadder = new PlacementRule(SnapType.Ladder);

        public readonly SnapType snap = SnapType.None;

        public PlacementRule(SnapType snap)
        {
            this.snap = snap;
        }

        public bool CanSnapTo(SnapType type)
        {
            return snap.HasFlag(type);
        }

        [Flags]
        public enum SnapType
        {
            None = 0,
            Wall = 1 << 0,
            Center = 1 << 1,
            CorridorCap = 1 << 2,
            Ladder = 1 << 3
        }
    }
}
