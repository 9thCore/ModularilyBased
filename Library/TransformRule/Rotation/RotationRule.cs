using UnityEngine;

namespace ModularilyBased.Library.TransformRule.Rotation
{
    public abstract class RotationRule : BaseTransformationRule
    {
        /**
         * <summary>
         * Return the module's rotation, given the snap collider's <paramref name="transform"/>.
         * </summary>
         */
        public abstract Quaternion Calculate(Transform transform);
    }
}
