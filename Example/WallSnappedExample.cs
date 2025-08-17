using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using UnityEngine;
using Nautilus.Utility;
using ModularilyBased.Library;
using Nautilus.Assets.Gadgets;
using ModularilyBased.Library.TransformRule;
using ModularilyBased.Library.TransformRule.Rotation;
using ModularilyBased.Library.TransformRule.Position;
using ModularilyBased.Library.PlaceRule;

namespace ModularilyBased.Example
{
    internal static class WallSnappedExample
    {
        public static PrefabInfo Info { get; private set; }

        public static void Register()
        {
            Info = PrefabInfo
                .WithTechType("WallSnappedExample", "Wall snapped example", "A simple example of wall snapping.")
                .WithIcon(SpriteManager.Get(TechType.BaseHatch));

            CustomPrefab prefab = new CustomPrefab(Info);

            prefab.SetRecipe(new Nautilus.Crafting.RecipeData(
                new Ingredient(TechType.Titanium, 2)
                ));

            prefab.SetPdaGroupCategory(TechGroup.InteriorPieces, TechCategory.InteriorPiece);

#if RELEASE
            CloneTemplate template = new CloneTemplate(Info, "386f311e-0d93-44cf-a180-f388820cb35b");
#else
            CloneTemplate template = new CloneTemplate(Info, "64d9faac-be43-4785-a7c1-3a8f89b72fd2"); 
#endif
            prefab.SetGameObject(template);

            template.ModifyPrefab += (GameObject obj) =>
            {
#if RELEASE
                GameObject model = obj.transform.Find("descent_trashcan_01").gameObject;
#else
                GameObject model = obj.transform.Find("discovery_trashcan_01_d").gameObject;
#endif

                PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);

                Constructable constructable = PrefabUtils.AddConstructable(obj, Info.TechType, ConstructableFlags.None, model);

                ConstructableBounds bounds = obj.EnsureComponent<ConstructableBounds>();
                bounds.bounds.position = new Vector3(0f, 0.7f, 0f);
                bounds.bounds.size = new Vector3(0.5f, 0.7f, 0.2f);

                TransformationRule rules = new TransformationRule()
                .WithPositionRule(new OffsetPositionRule(0f, -1.25f, 0.5f))
                .WithRotationRule(SnappedRotationRule.NoOffsetCardinal);

                ModuleSnapper.SetSnappingRules(
                    constructable,
                    ModuleSnapper.RoomRule.SmallRoom | ModuleSnapper.RoomRule.LargeRoom,
                    PlacementRule.SnapToWall,
                    rules);
            };

            prefab.Register();
        }
    }
}
