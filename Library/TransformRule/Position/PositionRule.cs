using UnityEngine;

namespace ModularilyBased.Library.TransformRule.Position
{
    public abstract class PositionRule : BaseTransformationRule<Vector3>
    {
        /**
         * <summary>
         * Return the module's position, given the snap collider's <paramref name="transform"/>.
         * </summary>
         */
        public abstract override Vector3 Calculate(Transform transform);
    }
}
