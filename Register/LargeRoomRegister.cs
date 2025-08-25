using ModularilyBased.API.Register;
using System;
using System.Collections;
using UnityEngine;

namespace ModularilyBased.Register
{
    // The big upside of code, the ability to iterate (else this file would be long as hell)
    internal static class LargeRoomRegister
    {
        internal static IEnumerator Register()
        {
            RegisterWalls();
            yield return null;

            RegisterLadders();
            yield return null;

            RegisterCenters();
            yield return null;

            RegisterWaterPark();
            yield return null;

            RegisterRotatedWalls();
            yield return null;

            RegisterRotatedLadders();
            yield return null;

            RegisterRotatedCenters();
            yield return null;

            RegisterRotatedWaterPark();
        }

        private static void RegisterWalls()
        {
            FaceBuilder builder = new FaceBuilder(FaceType.Wall, TechType.BaseLargeRoom, Base.CellType.LargeRoom)
                .WithScale(new Vector3(3.5f, 3f, 0.5f));

            // Describes the "long edge" walls, because they're the same, just offset
            for (int i = 1; i <= 4; i++)
            {
                new FaceBuilder(builder)
                    .WithSeabaseFace(new Int3(i, 0, 0), Base.Direction.South)
                    .WithPosition(new Vector3(i * 5f, 0f, 0f))
                    .Register();

                new FaceBuilder(builder)
                    .WithSeabaseFace(new Int3(i, 0, 2), Base.Direction.North)
                    .WithPosition(new Vector3(i * 5f, 0f, 10f))
                    .WithRotation(Quaternion.Euler(0, 180, 0))
                    .Register();
            }

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(0, 0, 1), Base.Direction.West)
                .WithPosition(new Vector3(0f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(5, 0, 1), Base.Direction.East)
                .WithPosition(new Vector3(25f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();
        }

        private static void RegisterLadders()
        {
            FaceBuilder builder = new FaceBuilder(FaceType.Ladder, TechType.BaseLargeRoom, Base.CellType.LargeRoom)
                .WithScale(new Vector3(1.25f, 3f, 1.25f));

            // "long edge" ladders
            for (int i = 1; i <= 4; i++)
            {
                new FaceBuilder(builder)
                    .WithSeabaseFace(new Int3(i, 0, 0), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(i, 0, 0), Base.Direction.Above)
                    .WithPosition(new Vector3(i * 5f, 0f, 1.6f))
                    .Register();

                new FaceBuilder(builder)
                    .WithSeabaseFace(new Int3(i, 0, 2), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(i, 0, 2), Base.Direction.Above)
                    .WithPosition(new Vector3(i * 5f, 0f, 8.4f))
                    .WithRotation(Quaternion.Euler(0, 180, 0))
                    .Register();
            }

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(0, 0, 1), Base.Direction.Below)
                .WithSeabaseFace(new Int3(0, 0, 1), Base.Direction.Above)
                .WithPosition(new Vector3(1.6f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(5, 0, 1), Base.Direction.Below)
                .WithSeabaseFace(new Int3(5, 0, 1), Base.Direction.Above)
                .WithPosition(new Vector3(23.4f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();
        }

        private static void RegisterCenters()
        {
            FaceBuilder builder = new FaceBuilder(FaceType.Center, TechType.BaseLargeRoom, Base.CellType.LargeRoom);

            // Single centers
            FaceBuilder singleFace = new FaceBuilder(builder).WithScale(new Vector3(2.5f, 3f, 2.5f));
            for (int i = 1; i <= 4; i++)
            {
                new FaceBuilder(singleFace)
                    .WithSeabaseFace(new Int3(i, 0, 1), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(i, 0, 1), Base.Direction.Above)
                    .WithPosition(new Vector3(i * 5f, 0f, 5f))
                    .Register();
            }

            // Double centers
            FaceBuilder doubleFace = new FaceBuilder(builder).WithScale(new Vector3(5.83f, 3f, 2.5f));
            for (int i = 1; i <= 3; i++)
            {
                float x = i switch
                {
                    1 => 6.66f,
                    2 => 12.5f,
                    3 => 18.34f,
                    _ => 0
                };

                new FaceBuilder(doubleFace)
                    .WithSeabaseFace(new Int3(i, 0, 1), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(i, 0, 1), Base.Direction.Above)
                    .WithSeabaseFace(new Int3(i + 1, 0, 1), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(i + 1, 0, 1), Base.Direction.Above)
                    .WithRequiredModuleSize(2)
                    .WithPosition(new Vector3(x, 0f, 5f))
                    .Register();
            }

            // Triple centers
            FaceBuilder tripleFace = new FaceBuilder(builder).WithScale(new Vector3(8.75f, 3f, 2.5f));
            for (int i = 1; i <= 2; i++)
            {
                float x = i switch
                {
                    1 => 8.125f,
                    2 => 16.875f,
                    _ => 0
                };

                new FaceBuilder(tripleFace)
                    .WithSeabaseFace(new Int3(i, 0, 1), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(i, 0, 1), Base.Direction.Above)
                    .WithSeabaseFace(new Int3(i + 1, 0, 1), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(i + 1, 0, 1), Base.Direction.Above)
                    .WithSeabaseFace(new Int3(i + 2, 0, 1), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(i + 2, 0, 1), Base.Direction.Above)
                    .WithRequiredModuleSize(3)
                    .WithPosition(new Vector3(x, 0f, 5f))
                    .Register();
            }

            // Quadruple center
            new FaceBuilder(builder)
                .WithScale(new Vector3(17.5f, 3f, 2.5f))
                    .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.Above)
                    .WithSeabaseFace(new Int3(2, 0, 1), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(2, 0, 1), Base.Direction.Above)
                    .WithSeabaseFace(new Int3(3, 0, 1), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(3, 0, 1), Base.Direction.Above)
                    .WithSeabaseFace(new Int3(4, 0, 1), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(4, 0, 1), Base.Direction.Above)
                    .WithRequiredModuleSize(4)
                    .WithPosition(new Vector3(12.5f, 0f, 5f))
                    .Register();
        }

        private static void RegisterWaterPark()
        {
            FaceBuilder builder = new FaceBuilder(FaceType.WaterPark, TechType.BaseLargeRoom, Base.CellType.LargeRoom)
                .WithScale(new Vector3(2.75f, 2.5f, 1.5f));

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.North)
                .WithPosition(new Vector3(5f, 0, 7.5f))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.South)
                .WithPosition(new Vector3(5f, 0, 2.5f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.West)
                .WithPosition(new Vector3(3.1f, 0, 5f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(2, 0, 1), Base.Direction.North)
                .WithPosition(new Vector3(10f, 0, 7.5f))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(2, 0, 1), Base.Direction.South)
                .WithPosition(new Vector3(10f, 0, 2.5f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(2, 0, 1), Base.Direction.East)
                .WithPosition(new Vector3(11.9f, 0, 5f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(2, 0, 1), Base.Direction.West)
                .WithPosition(new Vector3(8.1f, 0, 5f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(3, 0, 1), Base.Direction.North)
                .WithPosition(new Vector3(15f, 0, 7.5f))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(3, 0, 1), Base.Direction.South)
                .WithPosition(new Vector3(15f, 0, 2.5f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(3, 0, 1), Base.Direction.West)
                .WithPosition(new Vector3(13.1f, 0, 5f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(3, 0, 1), Base.Direction.East)
                .WithPosition(new Vector3(16.9f, 0, 5f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(4, 0, 1), Base.Direction.North)
                .WithPosition(new Vector3(20f, 0, 7.5f))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(4, 0, 1), Base.Direction.East)
                .WithPosition(new Vector3(21.9f, 0, 5f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(4, 0, 1), Base.Direction.South)
                .WithPosition(new Vector3(20f, 0, 2.5f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();
        }

        private static void RegisterRotatedWalls()
        {
            FaceBuilder builder = new FaceBuilder(FaceType.Wall, TechType.BaseLargeRoom, Base.CellType.LargeRoomRotated)
                .WithScale(new Vector3(3.5f, 3f, 0.5f));

            // Describes the "long edge" walls, because they're the same, just offset
            for (int i = 1; i <= 4; i++)
            {
                new FaceBuilder(builder)
                    .WithSeabaseFace(new Int3(0, 0, i), Base.Direction.West)
                    .WithPosition(new Vector3(0f, 0f, i * 5f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                    .Register();

                new FaceBuilder(builder)
                    .WithSeabaseFace(new Int3(2, 0, i), Base.Direction.East)
                    .WithPosition(new Vector3(10f, 0f, i * 5f))
                    .WithRotation(Quaternion.Euler(0, 270, 0))
                    .Register();
            }

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 0), Base.Direction.South)
                .WithPosition(new Vector3(5f, 0f, 0f))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 5), Base.Direction.North)
                .WithPosition(new Vector3(5f, 0f, 25f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();
        }

        private static void RegisterRotatedLadders()
        {
            FaceBuilder builder = new FaceBuilder(FaceType.Ladder, TechType.BaseLargeRoom, Base.CellType.LargeRoomRotated)
                .WithScale(new Vector3(1.25f, 3f, 1.25f));

            // "long edge" ladders
            for (int i = 1; i <= 4; i++)
            {
                new FaceBuilder(builder)
                    .WithSeabaseFace(new Int3(0, 0, i), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(0, 0, i), Base.Direction.Above)
                    .WithPosition(new Vector3(1.6f, 0f, i * 5f))
                    .WithRotation(Quaternion.Euler(0, 90, 0))
                    .Register();

                new FaceBuilder(builder)
                    .WithSeabaseFace(new Int3(2, 0, i), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(2, 0, i), Base.Direction.Above)
                    .WithPosition(new Vector3(8.4f, 0f, i * 5f))
                    .WithRotation(Quaternion.Euler(0, 270, 0))
                    .Register();
            }

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 0), Base.Direction.Below)
                .WithSeabaseFace(new Int3(1, 0, 0), Base.Direction.Above)
                .WithPosition(new Vector3(5f, 0f, 1.6f))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 5), Base.Direction.Below)
                .WithSeabaseFace(new Int3(1, 0, 5), Base.Direction.Above)
                .WithPosition(new Vector3(5f, 0f, 23.4f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();
        }

        private static void RegisterRotatedCenters()
        {
            FaceBuilder builder = new FaceBuilder(FaceType.Center, TechType.BaseLargeRoom, Base.CellType.LargeRoomRotated)
                .WithRotation(Quaternion.Euler(0, 270, 0));

            // Single centers
            FaceBuilder singleFace = new FaceBuilder(builder).WithScale(new Vector3(2.5f, 3f, 2.5f));
            for (int i = 1; i <= 4; i++)
            {
                new FaceBuilder(singleFace)
                    .WithSeabaseFace(new Int3(1, 0, i), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(1, 0, i), Base.Direction.Above)
                    .WithPosition(new Vector3(5f, 0f, i * 5f))
                    .Register();
            }

            // Double centers
            FaceBuilder doubleFace = new FaceBuilder(builder).WithScale(new Vector3(5.83f, 3f, 2.5f));
            for (int i = 1; i <= 3; i++)
            {
                float z = i switch
                {
                    1 => 6.66f,
                    2 => 12.5f,
                    3 => 18.34f,
                    _ => 0
                };

                new FaceBuilder(doubleFace)
                    .WithSeabaseFace(new Int3(1, 0, i), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(1, 0, i), Base.Direction.Above)
                    .WithSeabaseFace(new Int3(1, 0, i + 1), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(1, 0, i + 1), Base.Direction.Above)
                    .WithRequiredModuleSize(2)
                    .WithPosition(new Vector3(5f, 0f, z))
                    .Register();
            }

            // Triple centers
            FaceBuilder tripleFace = new FaceBuilder(builder).WithScale(new Vector3(8.75f, 3f, 2.5f));
            for (int i = 1; i <= 2; i++)
            {
                float z = i switch
                {
                    1 => 8.125f,
                    2 => 16.875f,
                    _ => 0
                };

                new FaceBuilder(tripleFace)
                    .WithSeabaseFace(new Int3(1, 0, i), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(1, 0, i), Base.Direction.Above)
                    .WithSeabaseFace(new Int3(1, 0, i + 1), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(1, 0, i + 1), Base.Direction.Above)
                    .WithSeabaseFace(new Int3(1, 0, i + 2), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(1, 0, i + 2), Base.Direction.Above)
                    .WithRequiredModuleSize(3)
                    .WithPosition(new Vector3(5f, 0f, z))
                    .Register();
            }

            // Quadruple center
            new FaceBuilder(builder)
                .WithScale(new Vector3(17.5f, 3f, 2.5f))
                    .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.Above)
                    .WithSeabaseFace(new Int3(1, 0, 2), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(1, 0, 2), Base.Direction.Above)
                    .WithSeabaseFace(new Int3(1, 0, 3), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(1, 0, 3), Base.Direction.Above)
                    .WithSeabaseFace(new Int3(1, 0, 4), Base.Direction.Below)
                    .WithSeabaseFace(new Int3(1, 0, 4), Base.Direction.Above)
                    .WithRequiredModuleSize(4)
                    .WithPosition(new Vector3(5f, 0f, 12.5f))
                    .Register();
        }

        private static void RegisterRotatedWaterPark()
        {
            FaceBuilder builder = new FaceBuilder(FaceType.WaterPark, TechType.BaseLargeRoom, Base.CellType.LargeRoomRotated)
                .WithScale(new Vector3(2.75f, 2.5f, 1.5f));

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 3), Base.Direction.East)
                .WithPosition(new Vector3(7.5f, 0f, 15f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 3), Base.Direction.South)
                .WithPosition(new Vector3(5f, 0f, 13.1f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 3), Base.Direction.West)
                .WithPosition(new Vector3(2.5f, 0f, 15f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 3), Base.Direction.North)
                .WithPosition(new Vector3(5f, 0f, 16.9f))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 4), Base.Direction.East)
                .WithPosition(new Vector3(7.5f, 0f, 20f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 4), Base.Direction.West)
                .WithPosition(new Vector3(2.5f, 0f, 20f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 4), Base.Direction.North)
                .WithPosition(new Vector3(5f, 0f, 21.9f))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 2), Base.Direction.East)
                .WithPosition(new Vector3(7.5f, 0f, 10f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 2), Base.Direction.West)
                .WithPosition(new Vector3(2.5f, 0f, 10f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 2), Base.Direction.North)
                .WithPosition(new Vector3(5f, 0f, 11.9f))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 2), Base.Direction.South)
                .WithPosition(new Vector3(5f, 0f, 8.1f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.South)
                .WithPosition(new Vector3(5f, 0f, 3.1f))
                .WithRotation(Quaternion.Euler(0, 180, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.East)
                .WithPosition(new Vector3(7.5f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 90, 0))
                .Register();

            new FaceBuilder(builder)
                .WithSeabaseFace(new Int3(1, 0, 1), Base.Direction.West)
                .WithPosition(new Vector3(2.5f, 0f, 5f))
                .WithRotation(Quaternion.Euler(0, 270, 0))
                .Register();
        }
    }
}
