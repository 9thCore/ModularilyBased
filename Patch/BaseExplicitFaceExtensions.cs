using System.Collections.Generic;
using UnityEngine;

namespace ModularilyBased.Patch
{
    public static class BaseExplicitFaceExtensions
    {
        public static bool TryGetCellIdentifier(this BaseExplicitFace face, out TechType type)
        {
            BaseDeconstructable deconstructable = face.parent;
            if (deconstructable == null)
            {
                type = TechType.None;
                return false;
            }

            type = deconstructable.recipe;
            return true;
        }

        public static bool TryGetColliderScale(this BaseExplicitFace face, out Vector3 scale)
        {
            if (!face.TryGetCellIdentifier(out TechType type))
            {
                scale = Vector3.zero;
                return false;
            }

            switch (type)
            {
                case TechType.BaseRoom
                or TechType.BaseLargeRoom
                or TechType.BaseMapRoom
                or TechType.BaseMoonpool:
                    scale = RoomSideColliderScale;
                    return true;
                case TechType.BaseCorridor
                or TechType.BaseCorridorGlass:
                    scale = CorridorSideColliderScale;
                    return true;
                default:
                    scale = Vector3.zero;
                    return false;
            }
        }

        public static bool TryGetColliderDistance(this BaseExplicitFace face, out float distance)
        {
            if (!face.TryGetCellIdentifier(out TechType type)
                || !face.TryGetFaceType(out BaseFaceIdentifier.FaceType faceType))
            {
                distance = 0f;
                return false;
            }

            bool longSide = (faceType == BaseFaceIdentifier.FaceType.LongSide);

            switch (type)
            {
                case TechType.BaseRoom:
                    distance = -0.5f;
                    return true;
                case TechType.BaseLargeRoom:
                    distance = 0.25f;
                    return true;
                case TechType.BaseMapRoom:
                    distance = -1.25f;
                    return true;
                case TechType.BaseMoonpool:
                    distance = longSide ? 0.35f : -0.25f;
                    return true;
                case TechType.BaseCorridor
                or TechType.BaseCorridorGlass:
                    distance = 0f;
                    return true;
                default:
                    distance = 0f;
                    return false;
            }
        }

        public static bool TryGetFaceType(this BaseExplicitFace face, out BaseFaceIdentifier.FaceType type)
        {
            if (!face.TryGetCellIdentifier(out TechType tech))
            {
                type = BaseFaceIdentifier.FaceType.None;
                return false;
            }

            if (RectangularRooms.Contains(tech))
            {
                // eugh
                if (face.gameObject.name.Contains("Short"))
                {
                    type = BaseFaceIdentifier.FaceType.ShortSide;
                    return true;
                }

                type = BaseFaceIdentifier.FaceType.LongSide;
                return true;
            }

            type = BaseFaceIdentifier.FaceType.AnySide;
            return true;
        }

        public static readonly Vector3 RoomSideColliderScale = new Vector3(0.5f, 3.0f, 3.5f);
        public static readonly Vector3 CorridorSideColliderScale = new Vector3(0.5f, 2.5f, 3.5f);

        // Because some rooms may have short & long sides
        public static readonly HashSet<TechType> RectangularRooms = new HashSet<TechType>()
        {
            TechType.BaseLargeRoom,
            TechType.BaseMoonpool
        };
    }
}
