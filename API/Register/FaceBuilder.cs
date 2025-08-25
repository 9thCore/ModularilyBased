using ModularilyBased.Functionality;
using System.Collections.Generic;
using UnityEngine;

namespace ModularilyBased.API.Register
{
    /// <summary>
    /// Helper builder that can construct and register a face.
    /// </summary>
    public class FaceBuilder
    {
        internal FaceData FinalFaceData => new FaceData(faceType, seabaseFaces.ToArray(), requiredModuleSize, position, scale, rotation);

        internal readonly FaceType faceType;
        internal readonly TechType techType;
        internal readonly Base.CellType cellType;

        internal List<Base.Face> seabaseFaces = new();
        internal int requiredModuleSize = 1;
        internal Vector3 position = Vector3.zero;
        internal Vector3 scale = Vector3.one;
        internal Quaternion rotation = Quaternion.identity;

        /// <summary>
        /// Begin building a generic face for the room described by <paramref name="techType"/>. Will apply to all variations of the room, if it has multiple possible <see cref="Base.CellType"/>s.
        /// </summary>
        /// <param name="faceType">The face's type</param>
        /// <param name="techType">Room's <see cref="TechType"/></param>
        public FaceBuilder(FaceType faceType, TechType techType) : this(faceType, techType, Base.CellType.Empty) { }

        /// <summary>
        /// Begin building a generic face for the room described by <paramref name="techType"/> and <paramref name="cellType"/>.
        /// </summary>
        /// <param name="faceType">The face's type</param>
        /// <param name="techType">Room's <see cref="TechType"/></param>
        /// <param name="cellType">Room variation's <see cref="Base.CellType"/></param>
        public FaceBuilder(FaceType faceType, TechType techType, Base.CellType cellType)
        {
            this.faceType = faceType;
            this.techType = techType;
            this.cellType = cellType;
        }

        /// <summary>
        /// Copy a <see cref="FaceBuilder"/> into a new builder, "branching" out and allowing reusing common properties.
        /// </summary>
        /// <param name="builder"></param>
        public FaceBuilder(FaceBuilder builder)
        {
            faceType = builder.faceType;
            techType = builder.techType;
            cellType = builder.cellType;
            seabaseFaces = new(builder.seabaseFaces);
            position = builder.position;
            scale = builder.scale;
            rotation = builder.rotation;
        }

        /// <summary>
        /// Construct a <see cref="Base.Face"/> from the parameters given and add it to this face. If any of the <see cref="Base.Face"/>s are occupied, this face will not be available for placement.
        /// </summary>
        /// <param name="cell">Face positional identifier</param>
        /// <param name="direction">Face directional identifier</param>
        /// <returns><see cref="FaceBuilder"/></returns>
        public FaceBuilder WithSeabaseFace(Int3 cell, Base.Direction direction)
        {
            return WithSeabaseFace(new Base.Face(cell, direction));
        }

        /// <summary>
        /// Add the <see cref="Base.Face"/> given to this face. If any of the <see cref="Base.Face"/>s are occupied, this face will not be available for placement.
        /// </summary>
        /// <param name="face"></param>
        /// <returns><see cref="FaceBuilder"/></returns>
        public FaceBuilder WithSeabaseFace(Base.Face face)
        {
            seabaseFaces.Add(face);
            return this;
        }

        /// <summary>
        /// Set a potential module's required size to snap onto this face - usually, should be 1 (exception in vanilla being the large room, with how it has several "center" faces, of several sizes)
        /// </summary>
        /// <param name="requiredModuleSize">Required size for a module to snap to this face</param>
        /// <returns><see cref="FaceBuilder"/></returns>
        public FaceBuilder WithRequiredModuleSize(int requiredModuleSize)
        {
            this.requiredModuleSize = requiredModuleSize;
            return this;
        }

        /// <summary>
        /// Set the face's local position in the room.
        /// </summary>
        /// <param name="position"></param>
        /// <returns><see cref="FaceBuilder"/></returns>
        public FaceBuilder WithPosition(Vector3 position)
        {
            this.position = position;
            return this;
        }

        /// <summary>
        /// Set the face's scale.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns><see cref="FaceBuilder"/></returns>
        public FaceBuilder WithScale(Vector3 scale)
        {
            this.scale = scale;
            return this;
        }

        /// <summary>
        /// Set the face's local rotation in the room.
        /// </summary>
        /// <param name="rotation"></param>
        /// <returns><see cref="FaceBuilder"/></returns>
        public FaceBuilder WithRotation(Quaternion rotation)
        {
            this.rotation = rotation;
            return this;
        }

        /// <summary>
        /// Register the face constructed by this builder. The builder may still be used.
        /// </summary>
        public void Register()
        {
            RoomFaceHolder.AddFace(techType, cellType, FinalFaceData);
        }
    }
}
