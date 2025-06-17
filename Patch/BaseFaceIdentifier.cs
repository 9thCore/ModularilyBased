using System.Collections.Generic;
using UnityEngine;

namespace ModularilyBased.Patch
{
    public class BaseFaceIdentifier : MonoBehaviour
    {
        public BaseExplicitFace explicitFace;
        public TechType room = TechType.None;
        public FaceType face = FaceType.None;
        public BoxCollider collider;

        public enum FaceType
        {
            None,
            LongSide,
            ShortSide,
            CorridorSide,
            CorridorCap,
            // Top,
            // Bottom,
            // Center
        }
    }
}
