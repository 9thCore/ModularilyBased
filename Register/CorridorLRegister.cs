using ModularilyBased.API.Register;
using System.Collections;
using UnityEngine;

namespace ModularilyBased.Register
{
    internal static class CorridorLRegister
    {
        public static IEnumerator Register()
        {
            RegisterGenericCaps(TechType.BaseCorridorL);
            yield return null;

            RegisterGenericCaps(TechType.BaseCorridorGlassL);
        }

        private static void RegisterGenericCaps(TechType techType)
        {
            Vector3 scale = new Vector3(2.25f, 2.25f, 0.5f);

            CorridorHelper.Register(
                FaceType.CorridorCap,
                techType,
                CorridorHelper.CorridorRotate0,
                scale,
                new CorridorHelper.CorridorFace(Base.Direction.North, new Vector3(0f, 0f, 2f)),
                new CorridorHelper.CorridorFace(Base.Direction.East, new Vector3(2f, 0f, 0f)));

            CorridorHelper.Register(
                FaceType.CorridorCap,
                techType,
                CorridorHelper.CorridorRotate90,
                scale,
                new CorridorHelper.CorridorFace(Base.Direction.East, new Vector3(2f, 0f, 0f)),
                new CorridorHelper.CorridorFace(Base.Direction.South, new Vector3(0f, 0f, -2f)));

            CorridorHelper.Register(
                FaceType.CorridorCap,
                techType,
                CorridorHelper.CorridorRotate180,
                scale,
                new CorridorHelper.CorridorFace(Base.Direction.South, new Vector3(0f, 0f, -2f)),
                new CorridorHelper.CorridorFace(Base.Direction.West, new Vector3(-2f, 0f, 0f)));

            CorridorHelper.Register(
                FaceType.CorridorCap,
                techType,
                CorridorHelper.CorridorRotate270,
                scale,
                new CorridorHelper.CorridorFace(Base.Direction.West, new Vector3(-2f, 0f, 0f)),
                new CorridorHelper.CorridorFace(Base.Direction.North, new Vector3(0f, 0f, 2f)));
        }
    }
}
