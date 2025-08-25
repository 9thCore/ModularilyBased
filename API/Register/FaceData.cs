using UnityEngine;

namespace ModularilyBased.API.Register
{
    /// <summary>
    /// A data representation of a face.
    /// </summary>
    /// <param name="FaceType">Type of the library, as defined by the library</param>
    /// <param name="SeabaseFaces">Real <see cref="Base.Face"/>s occupied in the room - if any of these are occupied, then the module will not be constructable on this face</param>
    /// <param name="RequiredModuleSize">How big the module must be for it to be constructable on this face (useful for large room modules - a central module being 2 faces long will not snap to a 3-wide center face)</param>
    /// <param name="Position">Local position of the face in the room</param>
    /// <param name="Scale">Scale of the face</param>
    /// <param name="Rotation">Local rotation of the face in the room</param>
    public record FaceData(FaceType FaceType, Base.Face[] SeabaseFaces, int RequiredModuleSize, Vector3 Position, Vector3 Scale, Quaternion Rotation);
}
