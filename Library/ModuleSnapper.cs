using ModularilyBased.Library.TransformRule;
using ModularilyBased.Patch;
using System;
using UnityEngine;

namespace ModularilyBased.Library
{
    public class ModuleSnapper : MonoBehaviour
    {
        public RoomRule room = RoomRule.None;
        public PlacementRule placement = PlacementRule.None;
        public TransformationRule transformationRule = null;

        /**
         * <summary>
         * Apply snapping to the targetted object.
         * </summary>
         */
        public static ModuleSnapper SetSnappingRules(Constructable target, RoomRule room, PlacementRule placement, TransformationRule transformationRule = null)
        {
            if (target == null)
            {
                return null;
            }

            ModuleSnapper snapper = target.gameObject.EnsureComponent<ModuleSnapper>();
            snapper.room = room;
            snapper.placement = placement;
            snapper.transformationRule = transformationRule ??= new TransformationRule();

            transformationRule.constructable = target;

            return snapper;
        }

        public bool CanBuildOn(BaseFaceIdentifier identifier)
        {
            if ((identifier.IsWall() && !placement.HasFlag(PlacementRule.Side))
                || (identifier.IsCenter() && !placement.HasFlag(PlacementRule.Center))
                || (identifier.IsCap() && !placement.HasFlag(PlacementRule.CorridorCap))
                || (identifier.IsLadder() && !placement.HasFlag(PlacementRule.Ladder))
                )
            {
                return false;
            }

            TechType roomType = identifier.Room;

            if ((roomType == TechType.BaseRoom && !room.HasFlag(RoomRule.SmallRoom))
                || (roomType == TechType.BaseLargeRoom && !room.HasFlag(RoomRule.LargeRoom))
                || (roomType == TechType.BaseMapRoom && !room.HasFlag(RoomRule.MapRoom))
                || (roomType == TechType.BaseMoonpool && !room.HasFlag(RoomRule.Moonpool))
                || (BaseExplicitFaceExtensions.Corridors.Contains(roomType) && !room.HasFlag(RoomRule.Corridor)))
            {
                return false;
            }

            return true;
        }

        [Flags]
        public enum RoomRule
        {
            None = 0,
            SmallRoom = 1 << 0,
            LargeRoom = 1 << 1,
            MapRoom = 1 << 2,
            Moonpool = 1 << 3,
            Corridor = 1 << 4
        }

        [Flags]
        public enum PlacementRule
        {
            None = 0,
            Side = 1 << 0,
            Center = 1 << 1,
            CorridorCap = 1 << 2,
            Ladder = 1 << 3
        }
    }
}
