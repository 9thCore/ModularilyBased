using ModularilyBased.API.Buildable.PlaceRule;
using ModularilyBased.API.Buildable.TransformRule;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModularilyBased.API.Buildable
{
    public class ModuleSnapper : MonoBehaviour
    {
        /**
         * <summary>
         * Rooms this module can be placed in.
         * </summary>
         */
        public RoomRule room = RoomRule.None;

        /**
         * <summary>
         * Rules that decide if this module can be placed in a snap point.
         * </summary>
         */
        public PlacementRule placement;

        /**
         * <summary>
         * Rules that decide how the module may be positioned and rotated after finding a valid snap point.
         * </summary>
         */
        public TransformationRule transformationRule = null;

        /**
         * <summary>
         * Apply snapping to the <paramref name="target"/>, following the rules provided.
         * </summary>
         * <remarks>
         * <para>
         * <paramref name="room"/> represents an enumeration flag of which rooms the module can be placed in. Note that not all rooms have all faces.<br/>
         * <paramref name="placement"/> represents rules for deciding if a given snap is valid for the module.<br/>
         * <paramref name="transformationRule"/> represents rules applied to the module's position and rotation, taking into account the snap point.<br/>
         * Check <c>README.md</c> for more detailed documentation.
         * </para>
         * </remarks>
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
            if (!placement.CanBuildOn(this, identifier))
            {
                return false;
            }

            TechType roomType = identifier.Room;

            if (roomType == TechType.BaseRoom && !room.HasFlag(RoomRule.SmallRoom)
                || roomType == TechType.BaseLargeRoom && !room.HasFlag(RoomRule.LargeRoom)
                || roomType == TechType.BaseMapRoom && !room.HasFlag(RoomRule.MapRoom)
                || roomType == TechType.BaseMoonpool && !room.HasFlag(RoomRule.Moonpool)
                || Corridors.Contains(roomType) && !room.HasFlag(RoomRule.Corridor))
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

        public static readonly HashSet<TechType> Corridors = new()
        {
            TechType.BaseCorridor,
            TechType.BaseCorridorI,
            TechType.BaseCorridorL,
            TechType.BaseCorridorX,
            TechType.BaseCorridorT,
            TechType.BaseCorridorGlass,
            TechType.BaseCorridorGlassI,
            TechType.BaseCorridorGlassL
        };
    }
}
