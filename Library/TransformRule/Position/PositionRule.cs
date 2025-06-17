using UnityEngine;

namespace ModularilyBased.Library.TransformRule.Position
{
    public abstract class PositionRule
    {
        /**
         * <summary>
         * Return the module's position, given the snap collider's <paramref name="transform"/>.
         * </summary>
         */
        public abstract Vector3 Calculate(Transform transform);
    }
}
