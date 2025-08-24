using ModularilyBased.API.Buildable;
using UnityEngine;

namespace ModularilyBased.JSON
{
    /// <summary>
    /// Representation of a face.
    /// </summary>
    public class FaceData
    {
        /// <summary>
        /// The type of the face, used to determine if a constructable can be built on it.
        /// </summary>
        public BaseFaceIdentifier.FaceType face = BaseFaceIdentifier.FaceType.None;
        /// <summary>
        /// Base faces that this face occupies within a room. Used to determine if the constructable can be placed (if the face is not currently occupied).
        /// </summary>
        public Base.Face[] seabaseFaces;
        /// <summary>
        /// Position within the room of the collider.
        /// </summary>
        public Vector3 position = Vector3.zero;
        /// <summary>
        /// Scale of the collider.
        /// </summary>
        public Vector3 scale = Vector3.one;
        /// <summary>
        /// Rotation of the collider, local to the room.
        /// </summary>
        public Quaternion rotation = Quaternion.identity;
    }
}
