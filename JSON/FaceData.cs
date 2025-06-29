using ModularilyBased.Library;
using UnityEngine;

namespace ModularilyBased.JSON
{
    public class FaceData
    {
        public BaseFaceIdentifier.FaceType face = BaseFaceIdentifier.FaceType.None;
        public Base.Face[] seabaseFaces;
        public Vector3 position = Vector3.zero;
        public Vector3 scale = Vector3.one;
        public Quaternion rotation = Quaternion.identity;
    }
}
