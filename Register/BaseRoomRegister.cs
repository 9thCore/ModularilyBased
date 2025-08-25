using ModularilyBased.API.Register;
using System.Collections;
using UnityEngine;

namespace ModularilyBased.Register
{
    internal static class BaseRoomRegister
    {
        public static IEnumerator Register()
        {
            RegisterWalls();
            yield return null;

            RegisterLadders();
            yield return null;

            RegisterWaterPark();
            yield return null;
            
            RegisterCenter();
        }

        public static void RegisterWalls()
        {
            // Common properties for the wall
            FaceBuilder wallBuilder = new FaceBuilder(FaceType.Wall, TechType.BaseRoom)
                .WithScale(new Vector3(3.5f, 3f, 0.5f));

            // Cardinals
            new FaceBuilder(wallBuilder)
                .WithSeabaseFace(new Int3(2, 0, 1), Base.Direction.East)
                .WithPosition(new Vector3(9.25f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, -90, 0))
                .Register();

            new FaceBuilder(wallBuilder)
                .WithSeabaseFace(new Int3(1, 0, 0), Base.Direction.South)
                .WithPosition(new Vector3(5f, 0f, 0.75f))
                .Register();

            new FaceBuilder(wallBuilder)
                .WithSeabaseFace(new Int3(0, 0, 1), Base.Direction.West)
                .WithPosition(new Vector3(0.75f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();

            new FaceBuilder(wallBuilder)
                .WithSeabaseFace(new Int3(1, 0, 2), Base.Direction.North)
                .WithPosition(new Vector3(5f, 0f, 9.25f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();

            // Ordinals
            new FaceBuilder(wallBuilder).WithSeabaseFace(new Int3(2, 0, 0), Base.Direction.South)
                .WithPosition(new Vector3(8f, 0f, 2f))
                .WithRotation(Quaternion.Euler(0, -45, 0))
                .Register();

            new FaceBuilder(wallBuilder).WithSeabaseFace(new Int3(0, 0, 0), Base.Direction.West)
                .WithPosition(new Vector3(2f, 0f, 2f))
                .WithRotation(Quaternion.Euler(0, 45, 0))
                .Register();

            new FaceBuilder(wallBuilder).WithSeabaseFace(new Int3(0, 0, 2), Base.Direction.North)
                .WithPosition(new Vector3(2f, 0f, 8f))
                .WithRotation(Quaternion.Euler(0, 135, 0))
                .Register();

            new FaceBuilder(wallBuilder).WithSeabaseFace(new Int3(2, 0, 2), Base.Direction.East)
                .WithPosition(new Vector3(8f, 0f, 8f))
                .WithRotation(Quaternion.Euler(0, 225, 0))
                .Register();
        }

        public static void RegisterLadders()
        {
            // Common properties for the ladder
            FaceBuilder ladderBuilder = new FaceBuilder(FaceType.Ladder, TechType.BaseRoom)
                .WithScale(new Vector3(1.25f, 3f, 1.25f));

            new FaceBuilder(ladderBuilder)
                .WithSeabaseFace(new Int3(1, 0, 0), Base.Direction.Below)
                .WithSeabaseFace(new Int3(1, 0, 0), Base.Direction.Above)
                .WithPosition(new Vector3(5f, 0f, 1.577f))
                .WithRotation(Quaternion.Euler(0, 0, 0))
                .Register();

            new FaceBuilder(ladderBuilder)
                .WithSeabaseFace(new Int3(0, 0, 1), Base.Direction.Below)
                .WithSeabaseFace(new Int3(0, 0, 1), Base.Direction.Above)
                .WithPosition(new Vector3(1.577f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();

            new FaceBuilder(ladderBuilder)
                .WithSeabaseFace(new Int3(1, 0, 2), Base.Direction.Below)
                .WithSeabaseFace(new Int3(1, 0, 2), Base.Direction.Above)
                .WithPosition(new Vector3(5f, 0f, 8.423f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();

            new FaceBuilder(ladderBuilder)
                .WithSeabaseFace(new Int3(2, 0, 1), Base.Direction.Below)
                .WithSeabaseFace(new Int3(2, 0, 1), Base.Direction.Above)
                .WithPosition(new Vector3(8.423f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();
        }

        public static void RegisterWaterPark()
        {
            // Common properties for the waterpark
            FaceBuilder waterParkBuilder = new FaceBuilder(FaceType.WaterPark, TechType.BaseRoom)
                .WithScale(new Vector3(2.75f, 2.5f, 0.5f));

            new FaceBuilder(waterParkBuilder)
                .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.East)
                .WithPosition(new Vector3(7.75f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();

            new FaceBuilder(waterParkBuilder)
                .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.South)
                .WithPosition(new Vector3(5f, 0f, 2.25f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();

            new FaceBuilder(waterParkBuilder)
                .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.West)
                .WithPosition(new Vector3(2.25f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();

            new FaceBuilder(waterParkBuilder)
                .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.North)
                .WithPosition(new Vector3(5f, 0f, 7.75f))
                .Register();
        }

        public static void RegisterCenter()
        {
            new FaceBuilder(FaceType.Center, TechType.BaseRoom)
                .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.Below)
                .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.Above)
                .WithPosition(new Vector3(5f, 0f, 5f))
                .WithScale(new Vector3(2.5f, 3f, 2.5f))
                .Register();
        }
    }
}
