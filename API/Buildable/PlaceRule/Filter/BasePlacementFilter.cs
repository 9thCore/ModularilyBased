using ModularilyBased.API.Buildable;
using ModularilyBased.API.Buildable.PlaceRule;

namespace ModularilyBased.API.Buildable.PlaceRule.Filter
{
    /// <summary>
    /// Filter used in <see cref="FilteredPlacementRule"/> to determine if the constructable can be placed on a given face.
    /// </summary>
    public abstract class BasePlacementFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="snapper">The component of the given constructable.</param>
        /// <param name="identifier">The face it's attempting to be built on.</param>
        /// <returns>If the constructable can be build on this face.</returns>
        public abstract bool CanBuildOn(ModuleSnapper snapper, BaseFaceIdentifier identifier);
    }
}
