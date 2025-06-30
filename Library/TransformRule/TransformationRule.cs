using ModularilyBased.Library.TransformRule.Position;
using ModularilyBased.Library.TransformRule.Rotation;

namespace ModularilyBased.Library.TransformRule
{
    public class TransformationRule
    {
        public PositionRule PositionRule { get; private set; }
        public RotationRule RotationRule { get; private set; }

        internal Constructable constructable;

        /**
         * <summary>
         * Construct a <c>TransformationRule</c> with no noticeable effects on the module.
         * </summary>
         */
        public TransformationRule()
        {
            WithPositionRule(OffsetPositionRule.NoOffset).WithRotationRule(OffsetRotationRule.NoOffset);
        }

        /**
         * <summary>
         * Construct a <c>TransformationRule</c> with no noticeable rotation effect, but with the given position <paramref name="rule"/> on the module.
         * </summary>
         */
        public TransformationRule(OffsetPositionRule rule)
        {
            WithPositionRule(rule).WithRotationRule(OffsetRotationRule.NoOffset);
        }

        /**
         * <summary>
         * Construct a <c>TransformationRule</c> with no noticeable position effect, but with the given rotation <paramref name="rule"/> on the module.
         * </summary>
         */
        public TransformationRule(OffsetRotationRule rule)
        {
            WithPositionRule(OffsetPositionRule.NoOffset).WithRotationRule(rule);
        }

        /**
         * <summary>
         * Construct a <c>TransformationRule</c> with the given <paramref name="positionRule"/> and <paramref name="rotationRule"/>.
         * </summary>
         */
        public TransformationRule(OffsetPositionRule positionRule, OffsetRotationRule rotationRule)
        {
            WithPositionRule(positionRule).WithRotationRule(rotationRule);
        }
        
        /**
         * <summary>
         * Builder-type pattern, assigns the given <paramref name="positionRule"/> to the <c>TransformationRule</c>.
         * </summary>
         */
        public TransformationRule WithPositionRule(PositionRule positionRule)
        {
            PositionRule = positionRule;
            positionRule.container = this;
            return this;
        }

        /**
         * <summary>
         * Builder-type pattern, assigns the given <paramref name="rotationRule"/> to the <c>TransformationRule</c>.
         * </summary>
         */
        public TransformationRule WithRotationRule(RotationRule rotationRule)
        {
            RotationRule = rotationRule;
            rotationRule.container = this;
            return this;
        }
    }
}
