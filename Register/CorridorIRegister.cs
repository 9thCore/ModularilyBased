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
            CorridorHelper.Register(
                FaceType.Wall,
                TechType.BaseCorridorI,
                CorridorHelper.CorridorRotate0,
                new Vector3(2.25f, 2.25f, 0.5f),
                new CorridorHelper.CorridorFace(Base.Direction.East, new Vector3(0.875f, 0f, 0f)),
                new CorridorHelper.CorridorFace(Base.Direction.West, new Vector3(-0.875f, 0f, 0f)));

            CorridorHelper.Register(
                FaceType.Wall,
                TechType.BaseCorridorI,
                CorridorHelper.CorridorRotate90,
                new Vector3(2.25f, 2.25f, 0.5f),
                new CorridorHelper.CorridorFace(Base.Direction.North, new Vector3(0f, 0f, 0.875f)),
                new CorridorHelper.CorridorFace(Base.Direction.South, new Vector3(0f, 0f, -0.875f)));
        }

        private static void RegisterGenericCaps(TechType techType)
        {
            CorridorHelper.Register(
                FaceType.CorridorCap,
                techType,
                CorridorHelper.CorridorRotate0,
                new Vector3(2.25f, 2.25f, 0.5f),
                new CorridorHelper.CorridorFace(Base.Direction.North, new Vector3(0f, 0f, 2.25f)),
                new CorridorHelper.CorridorFace(Base.Direction.South, new Vector3(0f, 0f, -2.25f)));

            CorridorHelper.Register(
                FaceType.CorridorCap,
                techType,
                CorridorHelper.CorridorRotate90,
                new Vector3(2.25f, 2.25f, 0.5f),
                new CorridorHelper.CorridorFace(Base.Direction.East, new Vector3(2.25f, 0f, 0f)),
                new CorridorHelper.CorridorFace(Base.Direction.West, new Vector3(-2.25f, 0f, 0f)));
        }
    }
}
