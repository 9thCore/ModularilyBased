using UnityEngine;

namespace ModularilyBased.Patch
{
    public class BaseFaceIdentifier : MonoBehaviour
    {
        public BaseExplicitFace explicitFace;
        public TechType room = TechType.None;
        public FaceType face = FaceType.None;
        public BoxCollider collider;

        public bool IsWall()
        {
            return face == FaceType.LongSide || face == FaceType.ShortSide || face == FaceType.CorridorSide;
        }

        public bool IsCenter()
        {
            return face == FaceType.Center;
        }

        public bool IsCap()
        {
            return face == FaceType.CorridorCap;
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
            Center
        }
    }
}
