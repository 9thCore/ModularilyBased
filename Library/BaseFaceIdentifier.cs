using UnityEngine;

namespace ModularilyBased.Library
{
    public class BaseFaceIdentifier : MonoBehaviour
    {
        public BaseExplicitFace ExplicitFace { get; internal set; }
        public TechType Room { get; internal set; } = TechType.None;
        public FaceType Face { get; internal set; } = FaceType.None;
        public BoxCollider Collider { get; internal set; }
        // Only relevant for large rooms, as they contain multiple center faces
        public int CenterFaceIndex { get; internal set; }

        public bool IsWall()
        {
            return Face == FaceType.LongSide || Face == FaceType.ShortSide || Face == FaceType.CorridorSide;
        }

        public bool IsCenter()
        {
            return Face == FaceType.Center;
        }

        public bool IsCap()
        {
            return Face == FaceType.CorridorCap;
        }

        public bool IsLadder()
        {
            return Face == FaceType.Ladder;
        }

        public enum FaceType
        {
            None,
            LongSide,
            ShortSide,
            CorridorSide,
            CorridorCap,
            // Top,
            // Bottom,
            Center,
            Ladder
        }
    }
}
