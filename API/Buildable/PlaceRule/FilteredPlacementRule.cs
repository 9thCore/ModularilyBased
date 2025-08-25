using ModularilyBased.API.Buildable.PlaceRule.Filter;
using ModularilyBased.API.Register;
using ModularilyBased.Functionality;
using System.Collections.Generic;

namespace ModularilyBased.API.Buildable.PlaceRule
{
    /// <summary>
    /// Placement rule that disallows placement if any of its filters fails.
    /// </summary>
    public class FilteredPlacementRule : PlacementRule
    {
        internal readonly HashSet<BasePlacementFilter> filters = new();

        public FilteredPlacementRule(FaceType snap) : base(snap)
        {

        }

        public FilteredPlacementRule(FaceType snap, params BasePlacementFilter[] filters) : base(snap)
        {
            this.filters.AddRange(filters);
        }

        public FilteredPlacementRule(FaceType snap, IEnumerable<BasePlacementFilter> filters) : base(snap)
        {
            this.filters.AddRange(filters);
        }

        public FilteredPlacementRule AddFilter(BasePlacementFilter filter)
        {
            filters.Add(filter);
            return this;
        }

        public FilteredPlacementRule AddFilters(IEnumerable<BasePlacementFilter> filters)
        {
            this.filters.AddRange(filters);
            return this;
        }

        public FilteredPlacementRule AddFilters(params BasePlacementFilter[] filters)
        {
            this.filters.AddRange(filters);
            return this;
        }

        internal override bool CanBuildOn(ModuleSnapper snapper, BaseFaceIdentifier identifier)
        {
            if (!base.CanBuildOn(snapper, identifier))
            {
                return false;
            }

            foreach (BasePlacementFilter filter in filters)
            {
                if (!filter.CanBuildOn(snapper, identifier))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
