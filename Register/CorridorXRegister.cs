using ModularilyBased.API.Register;
using UnityEngine;

namespace ModularilyBased.Register
{
    internal static class CorridorXRegister
    {
        public static void Register()
        {
            CorridorHelper.Register(
                FaceType.CorridorCap,
                TechType.BaseCorridorX,
                Base.CellType.Empty,
                new Vector3(2.25f, 2.25f, 0.5f),
                new CorridorHelper.CorridorFace(Base.Direction.North, new Vector3(0f, 0f, 2.25f)),
                new CorridorHelper.CorridorFace(Base.Direction.East, new Vector3(2.25f, 0f, 0f)),
                new CorridorHelper.CorridorFace(Base.Direction.South, new Vector3(0f, 0f, -2.25f)),
                new CorridorHelper.CorridorFace(Base.Direction.West, new Vector3(-2.25f, 0f, 0f)));
        }
    }
}
