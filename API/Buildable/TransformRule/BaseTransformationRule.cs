using UnityEngine;

namespace ModularilyBased.API.Buildable.TransformRule
{
    /// <summary>
    /// Base class for all "transformation" subrules
    /// </summary>
    /// <typeparam name="T">What it calculates. Only <see cref="Vector3"/> and <see cref="Quaternion"/> are used in practice</typeparam>
    public abstract class BaseTransformationRule<T>
    {
        internal TransformationRule container;
        internal abstract T Calculate(Transform transform);
    }
}
