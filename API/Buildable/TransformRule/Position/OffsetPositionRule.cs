using UnityEngine;

namespace ModularilyBased.API.Buildable.TransformRule.Position
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

        /**
         * <summary>
         * Construct a position rule with the given positional offset, in the form of a <c>Vector3</c>.
         * </summary>
         */
        public OffsetPositionRule(Vector3 offset)
        {
            this.offset = offset;
        }

        public override Vector3 Calculate(Transform transform)
        {
            return transform.TransformDirection(offset) + transform.position;
        }
    }
}
