using ModularilyBased.Patch;
using System;
using UnityEngine;

namespace ModularilyBased.Library
{
    public class ModuleSnapper : MonoBehaviour
    {
        public RoomRule room = RoomRule.None;
        public PlacementRule placement = PlacementRule.None;
        public RotationRule rotation = null;

        /**
         * <summary>
         * Apply snapping to the targetted object.
         * </summary>
         */
        public static ModuleSnapper SetSnappingRules(GameObject target, RoomRule room, PlacementRule placement, RotationRule rotation)
        {
            if (target == null)
            {
                return null;
            }

            ModuleSnapper snapper = target.EnsureComponent<ModuleSnapper>();
            snapper.room = room;
            snapper.placement = placement;
            snapper.rotation = rotation;

            return snapper;
        }

        public bool CanBuildOn(BaseFaceIdentifier identifier)
        {
            TechType roomType = identifier.room;

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
            CorridorCap = 1 << 2
        }

        public abstract class RotationRule
        {
            public abstract Quaternion Apply(Quaternion rotation);
        }

        /**
         * <summary>
         * Rotation rule that offsets the module's rotation.
         * </summary>
         */
        public class OffsetRotationRule : RotationRule
        {
            /**
             * <summary>
             * Instance of an <c>OffsetRotationRule</c> that describes no additional offset applied to the module.
             * </summary>
             */
            public static OffsetRotationRule NoOffset = new OffsetRotationRule(0f, 0f, 0f);
            public static OffsetRotationRule NoOffsetFixed = new OffsetRotationRule(0f, 0f, 0f, true);

            public bool fixedRotation;
            public Quaternion offset;

            /**
             * <summary>
             * Construct a rotation rule with the given <paramref name="offset"/>.
             * if <paramref name="fixedRotation"/> is set to <c>true</c>, then the player will not be able to rotate the module - use for wall-mounted modules.
             * </summary>
             */
            public OffsetRotationRule(Quaternion offset, bool fixedRotation = false)
            {
                this.fixedRotation = fixedRotation;
                this.offset = offset;
            }

            /**
             * <summary>
             * Construct a rotation rule with the given Euler angles.
             * if <paramref name="fixedRotation"/> is set to <c>true</c>, then the player will not be able to rotate the module - use for wall-mounted modules.
             * </summary>
             */
            public OffsetRotationRule(float x, float y, float z, bool fixedRotation = false)
            {
                this.fixedRotation = fixedRotation;
                offset = Quaternion.Euler(x, y, z);
            }

            public override Quaternion Apply(Quaternion rotation)
            {
                return fixedRotation ? offset : rotation * offset;
            }
        }
    }
}
