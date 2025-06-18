using System;
using UnityEngine;

namespace ModularilyBased.Library.TransformRule.Rotation
{
    /**
    * <summary>
    * Rotation rule that offsets the module's rotation and snaps the player-given rotation.
    * Will do nothing different from an <c>OffsetRotationRule</c> if the module is not freely rotatable.
    * </summary>
    */
    public class SnappedRotationRule : OffsetRotationRule
    {
        /**
         * <summary>
         * Instance of a <c>SnappedRotationRule</c> that describes no additional offset applied to the module and snapping to the nearest cardinal direction.
         * </summary>
         */
        public static readonly SnappedRotationRule NoOffsetCardinal = new SnappedRotationRule(0f, 90f);

        public readonly float snap;

        /**
         * <summary>
         * Construct a rotation rule with the given <paramref name="offset"/> and <paramref name="snap"/> (Y axis).
         * </summary>
         */
        public SnappedRotationRule(float offset, float snap) : base(offset)
        {
            this.snap = snap;
        }

        public override Quaternion Calculate(Transform transform)
        {
            float additive = FixedRotation()
                ? 0f
                : (float)Math.Round(Builder.additiveRotation / snap, 0, MidpointRounding.AwayFromZero) * snap;

            return transform.rotation * Quaternion.AngleAxis(additive + offset, Vector3.up);
        }
    }
}
