using UnityEngine;

namespace ModularilyBased.API.Buildable.TransformRule.Rotation
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
        public static readonly OffsetRotationRule NoOffset = new OffsetRotationRule(0f);

        public readonly float offset;

        /**
         * <summary>
         * Construct a rotation rule with the given offset (Y axis).
         * </summary>
         */
        public OffsetRotationRule(float offset)
        {
            this.offset = offset;
        }

        public override Quaternion Calculate(Transform transform)
        {
            float additive = FixedRotation() ? 0f : Builder.additiveRotation;
            return transform.rotation * Quaternion.AngleAxis(additive + offset, Vector3.up);
        }

        public bool FixedRotation()
        {
            if (container.constructable == null)
            {
                return false;
            }

            return !container.constructable.rotationEnabled;
        }
    }
}
