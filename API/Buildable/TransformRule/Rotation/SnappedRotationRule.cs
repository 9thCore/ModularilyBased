using System;
using UnityEngine;

namespace ModularilyBased.API.Buildable.TransformRule.Rotation
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
        public static readonly SnappedRotationRule NoOffsetCardinal = new SnappedRotationRule(0f, 90);

        private readonly int max;
        internal readonly int snap;

        /**
         * <summary>
         * Construct a rotation rule with the given <paramref name="offset"/> and <paramref name="snap"/> (Y axis).
         * Snap must be between 0 and 360, inclusive.
         * </summary>
         */
        public SnappedRotationRule(float offset, int snap) : base(offset)
        {
            if (snap < 0 || snap > 360)
            {
                throw new ArgumentOutOfRangeException(nameof(snap));
            }

            this.snap = snap;
            max = 360 / GreatestCommonDivisor(360, snap);
        }

        internal override Quaternion Calculate(Transform transform)
        {
            float additive = 0f;

            if (!FixedRotation())
            {
                Builder.UpdateRotation(max);
                additive = Builder.lastRotation * snap;
            }

            return transform.rotation * Quaternion.AngleAxis(additive + offset, Vector3.up);
        }

        // idk where else to throw this
        private static int GreatestCommonDivisor(int a, int b)
        {
            int r;
            while (b != 0)
            {
                r = a % b;
                a = b;
                b = r;
            }

            return a;
        }
    }
}
