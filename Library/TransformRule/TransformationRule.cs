using ModularilyBased.Library.TransformRule.Position;
using ModularilyBased.Library.TransformRule.Rotation;

namespace ModularilyBased.Library.TransformRule
{
    public class TransformationRule
    {
        public PositionRule PositionRule { get; private set; }
        public RotationRule RotationRule { get; private set; }

        internal Constructable constructable;

        public TransformationRule()
        {
            WithPositionRule(OffsetPositionRule.NoOffset).WithRotationRule(OffsetRotationRule.NoOffset);
        }
        
        public TransformationRule WithPositionRule(PositionRule positionRule)
        {
            PositionRule = positionRule;
            positionRule.container = this;
            return this;
        }

        public TransformationRule WithRotationRule(RotationRule rotationRule)
        {
            RotationRule = rotationRule;
            rotationRule.container = this;
            return this;
        }
    }
}
