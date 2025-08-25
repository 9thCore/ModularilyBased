using ModularilyBased.API.Register;
using System.Collections;
using UnityEngine;

namespace ModularilyBased.Register
{
    internal static class MoonpoolRegister
    {
        public static IEnumerator Register()
        {
            RegisterWalls();
            yield return null;

            RegisterRotatedWalls();
        }

        private static void RegisterWalls()
        {
            FaceBuilder builder = new FaceBuilder(FaceType.Wall, TechType.BaseMoonpool, Base.CellType.Moonpool)
                .WithScale(new Vector3(3.5f, 3f, 0.5f));

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 0), Base.Direction.South)
                .WithPosition(new Vector3(5f, 0f, -0.5f))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(2, 0, 0), Base.Direction.South)
                .WithPosition(new Vector3(10f, 0f, -0.5f))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 2), Base.Direction.North)
                .WithPosition(new Vector3(5f, 0f, 10.5f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(2, 0, 2), Base.Direction.North)
                .WithPosition(new Vector3(10f, 0f, 10.5f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(3, 0, 1), Base.Direction.East)
                .WithPosition(new Vector3(15f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(0, 0, 1), Base.Direction.West)
                .WithPosition(new Vector3(0f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();
        }

        private static void RegisterRotatedWalls()
        {
            FaceBuilder builder = new FaceBuilder(FaceType.Wall, TechType.BaseMoonpool, Base.CellType.MoonpoolRotated)
                .WithScale(new Vector3(3.5f, 3f, 0.5f));

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(0, 0, 1), Base.Direction.West)
                .WithPosition(new Vector3(-0.5f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(0, 0, 2), Base.Direction.West)
                .WithPosition(new Vector3(-0.5f, 0f, 10f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(2, 0, 1), Base.Direction.East)
                .WithPosition(new Vector3(10.5f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(2, 0, 2), Base.Direction.East)
                .WithPosition(new Vector3(10.5f, 0f, 10f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 3), Base.Direction.North)
                .WithPosition(new Vector3(5f, 0f, 15f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 0), Base.Direction.South)
                .WithPosition(new Vector3(5f, 0f, 0f))
                .Register();
        }
    }
}
