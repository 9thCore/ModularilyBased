using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using UnityEngine;
using Nautilus.Utility;
using ModularilyBased.Library;
using Nautilus.Assets.Gadgets;

namespace ModularilyBased.Example
{
    public static class WallSnappedExample
    {
        public static PrefabInfo Info { get; private set; }

        public static void Register()
        {
            Info = PrefabInfo.WithTechType("WallSnappedExample", "Wall snapped example", "An example of wall snapping.");
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
                BaseFaceSnapper.SetSnappingRules(obj, BaseFaceSnapper.RoomRule.SmallRoom, BaseFaceSnapper.PlacementRule.Side, BaseFaceSnapper.OffsetRotationRule.NoOffsetFixed);
            };

            prefab.Register();
        }
    }
}
