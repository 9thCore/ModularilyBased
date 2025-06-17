using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using UnityEngine;
using Nautilus.Utility;
using ModularilyBased.Library;
using Nautilus.Assets.Gadgets;
using ModularilyBased.Library.TransformRule;
using ModularilyBased.Library.TransformRule.Rotation;
using ModularilyBased.Library.TransformRule.Position;

namespace ModularilyBased.Example
{
    public static class WallSnappedExample
    {
        public static PrefabInfo Info { get; private set; }

        public static void Register()
        {
            Info = PrefabInfo
                .WithTechType("WallSnappedExample", "Wall snapped example", "A simple example of wall snapping.")
                .WithIcon(SpriteManager.Get(TechType.BaseHatch));

            CustomPrefab prefab = new CustomPrefab(Info);

            prefab.SetRecipe(new Nautilus.Crafting.RecipeData(
                new CraftData.Ingredient(TechType.Titanium, 2)
                ));

            prefab.SetPdaGroupCategory(TechGroup.InteriorPieces, TechCategory.InteriorPiece);

            CloneTemplate template = new CloneTemplate(Info, "386f311e-0d93-44cf-a180-f388820cb35b");
            prefab.SetGameObject(template);

            template.ModifyPrefab += (GameObject obj) =>
            {
                GameObject model = obj.transform.Find("descent_trashcan_01").gameObject;

                PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);
                PrefabUtils.AddConstructable(obj, Info.TechType, ConstructableFlags.Wall | ConstructableFlags.Base, model);

                ConstructableBounds bounds = obj.EnsureComponent<ConstructableBounds>();
                bounds.bounds.position = new Vector3(0f, 0.7f, 0f);
                bounds.bounds.size = new Vector3(0.5f, 0.7f, 0.2f);

                TransformationRule rules = new TransformationRule()
                .WithPositionRule(new OffsetPositionRule(0f, -1.25f, 1f))
                .WithRotationRule(OffsetRotationRule.NoOffset);

                ModuleSnapper.SetSnappingRules(
                    obj,
                    ModuleSnapper.RoomRule.SmallRoom,
                    ModuleSnapper.PlacementRule.Side,
                    rules);
            };

            prefab.Register();
        }
    }
}
