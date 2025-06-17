using UnityEngine;

namespace ModularilyBased.Library.TransformRule.Rotation
{
    /**
    * <summary>
    * Rotation rule that offsets the module's rotation.
    * </summary>
    */
    public class OffsetRotationRule : RotationRule
    {
        /**
         * <summary>
         * Instance of an <c>OffsetRotationRule</c> that describes no additional offset applied to the module.
         * </summary>
         */
        public static OffsetRotationRule NoOffset = new OffsetRotationRule(0f);
        public static OffsetRotationRule NoOffsetFixed = new OffsetRotationRule(0f, true);

        public bool fixedRotation;
        public float offset;

        /**
         * <summary>
         * Construct a rotation rule with the given offset (Y axis).
         * if <paramref name="fixedRotation"/> is set to <c>true</c>, then the player will not be able to rotate the module - use for wall-mounted modules.
         * </summary>
         */
        public OffsetRotationRule(float offset, bool fixedRotation = false)
        {
            this.fixedRotation = fixedRotation;
            this.offset = offset;
        }

        public override Quaternion Calculate(Transform transform)
        {
            float additive = fixedRotation ? 0f : Builder.additiveRotation;
            return transform.rotation * Quaternion.AngleAxis(additive + offset, Vector3.up);
        }
    }
}
