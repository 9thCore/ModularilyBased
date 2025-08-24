using ModularilyBased.API.Buildable.TransformRule;
using UnityEngine;

namespace ModularilyBased.API.Buildable.TransformRule.Rotation
{
    public abstract class RotationRule : BaseTransformationRule<Quaternion>
    {
        /**
         * <summary>
         * Return the module's rotation, given the snap collider's <paramref name="transform"/>.
         * </summary>
         */
        public abstract override Quaternion Calculate(Transform transform);
    }
}
