namespace ModularilyBased.Library.PlaceRule.Filter
{
    public class LargeRoomPlacementFilter : BasePlacementFilter
    {
        public static readonly LargeRoomPlacementFilter HalfWidth = new LargeRoomPlacementFilter(2);
        public static readonly LargeRoomPlacementFilter FullWidth = new LargeRoomPlacementFilter(4);

        public int moduleLength;

        public LargeRoomPlacementFilter(int length)
        {
            moduleLength = length - 1;
        }

        public override bool CanBuildOn(ModuleSnapper snapper, BaseFaceIdentifier identifier)
        {
            return identifier.CenterFaceIndex >= moduleLength;
        }
    }
}
