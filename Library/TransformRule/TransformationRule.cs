using ModularilyBased.Library.TransformRule.Position;
using ModularilyBased.Library.TransformRule.Rotation;

namespace ModularilyBased.Library.TransformRule
{
    public class TransformationRule
    {
        public PositionRule positionRule = OffsetPositionRule.NoOffset;
        public RotationRule rotationRule = OffsetRotationRule.NoOffset;
        
        public TransformationRule WithPositionRule(PositionRule positionRule)
        {
            this.positionRule = positionRule;
            return this;
        }

        public TransformationRule WithRotationRule(RotationRule rotationRule)
        {
            this.rotationRule = rotationRule;
            return this;
        }
    }
}
