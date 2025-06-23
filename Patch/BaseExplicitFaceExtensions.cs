using ModularilyBased.Library;
using System;
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
            if (!face.TryGetCellIdentifier(out TechType type)
                || !face.TryGetFaceType(out BaseFaceIdentifier.FaceType faceType))
            {
                scale = Vector3.zero;
                return false;
            }

            if (Corridors.Contains(type))
            {
                scale = CorridorColliderScale;
                return true;
            }

            switch (type)
            {
                case TechType.BaseRoom
                or TechType.BaseLargeRoom
                or TechType.BaseMapRoom
                or TechType.BaseMoonpool:
                    scale = faceType switch
                    {
                        BaseFaceIdentifier.FaceType.Center => RoomCenterColliderScale,
                        BaseFaceIdentifier.FaceType.Ladder => RoomLadderColliderScale,
                        _ => RoomSideColliderScale
                    };

                    return true;
                default:
                    scale = Vector3.zero;
                    return false;
            }
        }

        public static bool TryGetColliderRotationOffset(this BaseExplicitFace face, out Quaternion offset)
        {
            if (!face.TryGetCellIdentifier(out TechType type)
                || !face.TryGetFaceType(out BaseFaceIdentifier.FaceType faceType))
            {
                offset = Quaternion.identity;
                return false;
            }

            if (Corridors.Contains(type) && faceType == BaseFaceIdentifier.FaceType.CorridorCap)
            {
                offset = CorridorCapRotation;
                return true;
            }

            offset = Quaternion.identity;
            return true;
        }

        public static bool TryGetColliderDistance(this BaseExplicitFace face, out float distance)
        {
            if (!face.TryGetCellIdentifier(out TechType type)
                || !face.TryGetFaceType(out BaseFaceIdentifier.FaceType faceType))
            {
                distance = 0f;
                return false;
            }

            if (Corridors.Contains(type))
            {
                if (faceType == BaseFaceIdentifier.FaceType.CorridorCap)
                {
                    distance = 2.25f;
                    return true;
                }

                distance = 0.875f;
                return true;
            }

            bool longSide = (faceType == BaseFaceIdentifier.FaceType.LongSide);

            switch (type)
            {
                case TechType.BaseRoom:
                    distance = -0.75f;
                    return true;
                case TechType.BaseLargeRoom:
                    distance = 0f;
                    return true;
                case TechType.BaseMapRoom:
                    distance = -1.5f;
                    return true;
                case TechType.BaseMoonpool:
                    distance = longSide ? 0.5f : 0f;
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

            if (face.IsBottomFace())
            {
                if (face.IsCenterFace())
                {
                    type = BaseFaceIdentifier.FaceType.Center;
                    return true;
                }

                type = BaseFaceIdentifier.FaceType.Ladder;
                return true;
            }

            if (Corridors.Contains(tech))
            {
                // eugh
                if (face.gameObject.name.Contains("Cap"))
                {
                    type = BaseFaceIdentifier.FaceType.CorridorCap;
                    return true;
                }
            }

            if (RectangularRooms.Contains(tech))
            {
                // eugh
                if (face.gameObject.name.Contains("Short"))
                {
                    type = BaseFaceIdentifier.FaceType.ShortSide;
                    return true;
                }
            }

            type = BaseFaceIdentifier.FaceType.LongSide;
            return true;
        }

        public static bool IsCenterFace(this BaseExplicitFace face)
        {
            return face.IsCenterFace(out _);
        }

        public static bool IsCenterFace(this BaseExplicitFace face, out int centerIndex)
        {
            Vector3 position = face.transform.localPosition;
            
            if ((position.x == 5 && (position.z == 5 || position.z == 10 || position.z == 15 || position.z == 20))
                || (position.z == 5 && (position.x == 5 || position.x == 10 || position.x == 15 || position.x == 20)))
            {
                centerIndex = 0;
                return true;
            }

            centerIndex = default;
            return false;
        }

        public static bool IsBottomFace(this BaseExplicitFace face)
        {
            return face.face?.direction == Base.Direction.Below
                && face.gameObject.name.Contains("Cover");
        }

        public static readonly Vector3 RoomSideColliderScale = new Vector3(0.5f, 3.0f, 3.5f);
        public static readonly Vector3 RoomCenterColliderScale = new Vector3(2.5f, 3.0f, 2.5f);
        public static readonly Vector3 RoomLadderColliderScale = new Vector3(1.25f, 3.0f, 1.25f);

        public static readonly Vector3 CorridorColliderScale = new Vector3(0.5f, 2.25f, 2.25f);

        public static readonly Quaternion CorridorCapRotation = Quaternion.Euler(0f, 270f, 0f);

        // Because some rooms may have short & long sides
        public static readonly HashSet<TechType> RectangularRooms = new HashSet<TechType>()
        {
            TechType.BaseLargeRoom,
            TechType.BaseMoonpool
        };

        public static readonly HashSet<TechType> Corridors = new HashSet<TechType>()
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
