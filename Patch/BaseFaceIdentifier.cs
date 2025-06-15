using UnityEngine;

namespace ModularilyBased.Patch
{
    public class BaseFaceIdentifier : MonoBehaviour
    {
        public TechType room = TechType.None;
        public FaceType face = FaceType.None;
        public BoxCollider collider;

        public enum FaceType
        {
            None,
            AnySide,
            LongSide,
            ShortSide,
            // Top,
            // Bottom,
            // Center
        }
    }
}
