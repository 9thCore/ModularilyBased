using UnityEngine;

namespace ModularilyBased.API.Buildable.TransformRule
{
    public abstract class BaseTransformationRule<T>
    {
        public TransformationRule container;

        public abstract T Calculate(Transform transform);
    }
}
