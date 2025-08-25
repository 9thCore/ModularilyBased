using ModularilyBased.API.Register;
using Nautilus.Handlers;
using UnityEngine;
using static HandReticle;

namespace ModularilyBased.Register
{
    internal static class CorridorHelper
    {
        public static readonly Base.CellType CorridorRotate0 = EnumHandler.AddEntry<Base.CellType>($"{nameof(ModularilyBased)}_{nameof(CorridorRotate0)}");
        public static readonly Base.CellType CorridorRotate90 = EnumHandler.AddEntry<Base.CellType>($"{nameof(ModularilyBased)}_{nameof(CorridorRotate90)}");
        public static readonly Base.CellType CorridorRotate180 = EnumHandler.AddEntry<Base.CellType>($"{nameof(ModularilyBased)}_{nameof(CorridorRotate180)}");
        public static readonly Base.CellType CorridorRotate270 = EnumHandler.AddEntry<Base.CellType>($"{nameof(ModularilyBased)}_{nameof(CorridorRotate270)}");

        public static void Register(
            FaceType faceType,
            TechType roomTechType,
            Base.CellType cell,
            Vector3 scale,
            params CorridorFace[] faces)
        {
            FaceBuilder builder = new FaceBuilder(faceType, roomTechType, cell)
                .WithScale(scale);

            faces.ForEach(face =>
            {
                new FaceBuilder(builder)
                    .WithSeabaseFace(new Int3(0, 0, 0), face.Direction)
                    .WithPosition(face.Position)
                    .WithRotation(GetRotation(face.Direction))
                    .Register();
            });
        }

        private static Quaternion GetRotation(Base.Direction direction)
        {
            return direction switch
            {
                Base.Direction.North => Quaternion.Euler(0, 180, 0),
                Base.Direction.South => Quaternion.Euler(0, 0, 0),
                Base.Direction.East => Quaternion.Euler(0, 270, 0),
                Base.Direction.West => Quaternion.Euler(0, 90, 0),
                _ => Quaternion.identity
            };
        }

        public record CorridorFace(Base.Direction Direction, Vector3 Position);
    }
}
