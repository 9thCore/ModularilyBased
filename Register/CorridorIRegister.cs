using ModularilyBased.API.Register;
using System.Collections;
using UnityEngine;

namespace ModularilyBased.Register
{
    internal static class CorridorIRegister
    {
        public static IEnumerator Register()
        {
            RegisterWalls();
            yield return null;

            RegisterGenericCaps(TechType.BaseCorridorI);
            yield return null;

            RegisterGenericCaps(TechType.BaseCorridorGlassI);
        }

        private static void RegisterWalls()
        {
            FaceBuilder builder = new FaceBuilder(FaceType.Wall, TechType.BaseCorridorI)
                .WithScale(new Vector3(2.25f, 2.25f, 0.5f));

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(0, 0, 0), Base.Direction.East)
                .WithPosition(new Vector3(0.875f, 0f, 0f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(0, 0, 0), Base.Direction.West)
                .WithPosition(new Vector3(-0.875f, 0f, 0f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();
        }

        private static void RegisterGenericCaps(TechType techType)
        {
            FaceBuilder builder = new FaceBuilder(FaceType.CorridorCap, techType)
                .WithScale(new Vector3(2.25f, 2.25f, 0.5f));

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(0, 0, 0), Base.Direction.North)
                .WithPosition(new Vector3(0f, 0f, 2.25f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(0, 0, 0), Base.Direction.South)
                .WithPosition(new Vector3(0f, 0f, -2.25f))
                .Register();
        }
    }
}
