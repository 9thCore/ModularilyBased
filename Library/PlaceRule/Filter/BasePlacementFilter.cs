namespace ModularilyBased.Library.PlaceRule.Filter
{
    public abstract class BasePlacementFilter
    {
        public abstract bool CanBuildOn(ModuleSnapper snapper, BaseFaceIdentifier identifier);
    }
}
