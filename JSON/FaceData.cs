using ModularilyBased.Library;
using UnityEngine;

namespace ModularilyBased.JSON
{
    public class FaceData
    {
        public BaseFaceIdentifier.FaceType face = BaseFaceIdentifier.FaceType.None;
        public Base.Face seabaseFace = default;
        public int centerFaceIndex = 0;
        public Vector3 position = Vector3.zero;
        public Vector3 colliderPosition = Vector3.zero;
        public Vector3 scale = Vector3.one;
        public Quaternion rotation = Quaternion.identity;
        public Quaternion colliderRotation = Quaternion.identity;
    }
}
