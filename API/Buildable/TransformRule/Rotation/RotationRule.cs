using UnityEngine;

namespace ModularilyBased.API.Buildable.TransformRule.Rotation
{
    /// <summary>
    /// A generic rotation "transformation" subrule
    /// </summary>
    public abstract class RotationRule : BaseTransformationRule<Quaternion>
    {
        internal abstract override Quaternion Calculate(Transform transform);
    }
}
