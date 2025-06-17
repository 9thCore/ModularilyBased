using UnityEngine;

namespace ModularilyBased.Library.TransformRule.Position
{
    /**
    * <summary>
    * Position rule that offsets the module's position.
    * </summary>
    */
    public class OffsetPositionRule : PositionRule
    {
        /**
         * <summary>
         * Instance of an <c>OffsetPositionRule</c> that describes no additional offset applied to the module.
         * </summary>
         */
        public static OffsetPositionRule NoOffset = new OffsetPositionRule(0f, 0f, 0f);

        public Vector3 offset;

        /**
         * <summary>
         * Construct a position rule with the given positional offsets.
         * </summary>
         */
        public OffsetPositionRule(float x, float y, float z)
        {
            offset = new Vector3(x, y, z);
        }

        public override Vector3 Calculate(Transform transform)
        {
            return -offset.x * transform.forward + offset.y * transform.up - offset.z * transform.right + transform.position;
        }
    }
}
