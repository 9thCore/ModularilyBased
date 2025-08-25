using UnityEngine;

namespace ModularilyBased.API.Buildable.TransformRule.Position
{
    /// <summary>
    /// A generic position "transformation" subrule
    /// </summary>
    public abstract class PositionRule : BaseTransformationRule<Vector3>
    {
        internal abstract override Vector3 Calculate(Transform transform);
    }
}
