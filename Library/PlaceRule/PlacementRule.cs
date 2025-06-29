using System;

namespace ModularilyBased.Library.PlaceRule
{
    public class PlacementRule
    {
        public static readonly PlacementRule SnapToWall = new PlacementRule(SnapType.Wall);
        public static readonly PlacementRule SnapToCenter = new PlacementRule(SnapType.Center);
        public static readonly PlacementRule SnapToLadder = new PlacementRule(SnapType.Ladder);
        public static readonly PlacementRule SnapToCorridorCap = new PlacementRule(SnapType.CorridorCap);
        public static readonly PlacementRule SnapToWaterPark = new PlacementRule(SnapType.WaterParkSide);

        public static readonly PlacementRule LargeRoomDoubleFace = new PlacementRule(SnapType.Center, 2);
        public static readonly PlacementRule LargeRoomTripleFace = new PlacementRule(SnapType.Center, 3);
        public static readonly PlacementRule LargeRoomQuadrupleFace = new PlacementRule(SnapType.Center, 4);

        public readonly SnapType snap = SnapType.None;
        public readonly int requiredFaces = 1;

        public PlacementRule(SnapType snap, int requiredFaces = 1)
        {
            this.snap = snap;
            this.requiredFaces = requiredFaces;
        }

        public virtual bool CanBuildOn(ModuleSnapper snapper, BaseFaceIdentifier identifier)
        {
            if (requiredFaces != identifier.SeabaseFaces.Length
                || (identifier.IsWall() && !CanSnapTo(SnapType.Wall))
                || (identifier.IsCenter() && !CanSnapTo(SnapType.Center))
                || (identifier.IsCap() && !CanSnapTo(SnapType.CorridorCap))
                || (identifier.IsLadder() && !CanSnapTo(SnapType.Ladder))
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
