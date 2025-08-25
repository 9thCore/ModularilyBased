using ModularilyBased.API.Register;
using System.Collections;
using UnityEngine;

namespace ModularilyBased.Register
{
    internal static class MapRoomRegister
    {
        public static IEnumerator Register()
        {
            RegisterWalls();
            yield return null;

            RegisterRotatedWalls();
        }

        private static void RegisterWalls()
        {
            Vector3 scale = new Vector3(3.5f, 3f, 0.5f);

            FaceBuilder wallBuilder = new FaceBuilder(FaceType.Wall, TechType.BaseMapRoom, Base.CellType.MapRoom)
                .WithScale(scale);

            new FaceBuilder(wallBuilder)
                .WithSeabaseFace(new Int3(0, 0, 1), Base.Direction.West)
                .WithPosition(new Vector3(1.5f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();

            new FaceBuilder(wallBuilder)
                .WithSeabaseFace(new Int3(2, 0, 1), Base.Direction.East)
                .WithPosition(new Vector3(8.5f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();
        }

        private static void RegisterRotatedWalls()
        {
            Vector3 scale = new Vector3(3.5f, 3f, 0.5f);

            FaceBuilder wallBuilder = new FaceBuilder(FaceType.Wall, TechType.BaseMapRoom, Base.CellType.MapRoomRotated)
                .WithScale(scale);

            new FaceBuilder(wallBuilder)
                .WithSeabaseFace(new Int3(1, 0, 2), Base.Direction.North)
                .WithPosition(new Vector3(5f, 0f, 8.5f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();

            new FaceBuilder(wallBuilder)
                .WithSeabaseFace(new Int3(1, 0, 0), Base.Direction.South)
                .WithPosition(new Vector3(5f, 0f, 1.5f))
                .Register();
        }
    }
}
