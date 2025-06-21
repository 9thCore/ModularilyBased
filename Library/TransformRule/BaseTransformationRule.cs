using UnityEngine;

namespace ModularilyBased.Library.TransformRule
{
    public abstract class BaseTransformationRule<T>
    {
        public TransformationRule container;

        public abstract T Calculate(Transform transform);
    }
}
