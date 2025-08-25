using ModularilyBased.API.Register;
using System.Collections;
using UnityEngine;

namespace ModularilyBased.Register
{
    internal static class CorridorTRegister
    {
        public static IEnumerator Register()
        {
            RegisterWalls();
            yield return null;

            RegisterCaps();
        }

        private static void RegisterWalls()
        {
            Vector3 scale = new Vector3(2.25f, 2.25f, 0.5f);

            CorridorHelper.Register(
                FaceType.Wall,
                TechType.BaseCorridorT,
                CorridorHelper.CorridorRotate0,
                scale,
                new CorridorHelper.CorridorFace(Base.Direction.South, new Vector3(0f, 0f, -0.875f)));

            CorridorHelper.Register(
                FaceType.Wall,
                TechType.BaseCorridorT,
                CorridorHelper.CorridorRotate90,
                scale,
                new CorridorHelper.CorridorFace(Base.Direction.West, new Vector3(-0.875f, 0f, 0f)));

            CorridorHelper.Register(
                FaceType.Wall,
                TechType.BaseCorridorT,
                CorridorHelper.CorridorRotate180,
                scale,
                new CorridorHelper.CorridorFace(Base.Direction.North, new Vector3(0f, 0f, 0.875f)));

            CorridorHelper.Register(
                FaceType.Wall,
                TechType.BaseCorridorT,
                CorridorHelper.CorridorRotate270,
                scale,
                new CorridorHelper.CorridorFace(Base.Direction.East, new Vector3(0.875f, 0f, 0f)));
        }

        private static void RegisterCaps()
        {
            Vector3 scale = new Vector3(2.25f, 2.25f, 0.5f);

            CorridorHelper.Register(
                FaceType.CorridorCap,
                TechType.BaseCorridorT,
                CorridorHelper.CorridorRotate0,
                scale,
                new CorridorHelper.CorridorFace(Base.Direction.East, new Vector3(2.25f, 0f, 0f)),
                new CorridorHelper.CorridorFace(Base.Direction.West, new Vector3(-2.25f, 0f, 0f)),
                new CorridorHelper.CorridorFace(Base.Direction.North, new Vector3(0f, 0f, 2.25f)));

            CorridorHelper.Register(
                FaceType.CorridorCap,
                TechType.BaseCorridorT,
                CorridorHelper.CorridorRotate90,
                scale,
                new CorridorHelper.CorridorFace(Base.Direction.South, new Vector3(0f, 0f, -2.25f)),
                new CorridorHelper.CorridorFace(Base.Direction.North, new Vector3(0f, 0f, 2.25f)),
                new CorridorHelper.CorridorFace(Base.Direction.East, new Vector3(2.25f, 0f, 0f)));

            CorridorHelper.Register(
                FaceType.CorridorCap,
                TechType.BaseCorridorT,
                CorridorHelper.CorridorRotate180,
                scale,
                new CorridorHelper.CorridorFace(Base.Direction.West, new Vector3(-2.25f, 0f, 0f)),
                new CorridorHelper.CorridorFace(Base.Direction.East, new Vector3(2.25f, 0f, 0f)),
                new CorridorHelper.CorridorFace(Base.Direction.South, new Vector3(0f, 0f, -2.25f)));

            CorridorHelper.Register(
                FaceType.CorridorCap,
                TechType.BaseCorridorT,
                CorridorHelper.CorridorRotate270,
                scale,
                new CorridorHelper.CorridorFace(Base.Direction.North, new Vector3(2.25f, 0f, 0f)),
                new CorridorHelper.CorridorFace(Base.Direction.South, new Vector3(0f, 0f, -2.25f)),
                new CorridorHelper.CorridorFace(Base.Direction.West, new Vector3(-2.25f, 0f, 0f)));
        }
    }
}
