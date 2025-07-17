using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using UnityEngine;
using Nautilus.Utility;
using ModularilyBased.Library;
using Nautilus.Assets.Gadgets;
using ModularilyBased.Library.TransformRule;
using ModularilyBased.Library.TransformRule.Rotation;
using ModularilyBased.Library.PlaceRule;

namespace ModularilyBased.Example
{
    internal static class WaterParkSnappedExample
    {
        public static PrefabInfo Info { get; private set; }

        public static void Register()
        {
            Info = PrefabInfo
                .WithTechType("ACUSnappedExample", "ACU snapped example", "A simple example of Alien Containment Unit side snapping.")
                .WithIcon(SpriteManager.Get(TechType.Centrifuge));

            CustomPrefab prefab = new CustomPrefab(Info);

            prefab.SetRecipe(new Nautilus.Crafting.RecipeData(
                new CraftData.Ingredient(TechType.Titanium),
                new CraftData.Ingredient(TechType.MercuryOre)
                ));

            prefab.SetPdaGroupCategory(TechGroup.InteriorPieces, TechCategory.InteriorPiece);

            CloneTemplate template = new CloneTemplate(Info, TechType.Centrifuge);
            prefab.SetGameObject(template);

            template.ModifyPrefab += (GameObject obj) =>
            {
                GameObject.Destroy(obj.FindChild("HACK"));
                GameObject model = obj.GetComponentInChildren<Renderer>().transform.parent.gameObject;

                PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);

                Constructable constructable = PrefabUtils.AddConstructable(obj, Info.TechType, ConstructableFlags.None, model);

                ConstructableBounds bounds = obj.EnsureComponent<ConstructableBounds>();

                TransformationRule rules = new TransformationRule()
                .WithRotationRule(SnappedRotationRule.NoOffsetCardinal);

                ModuleSnapper.SetSnappingRules(
                    constructable,
                    ModuleSnapper.RoomRule.SmallRoom | ModuleSnapper.RoomRule.LargeRoom,
                    PlacementRule.SnapToWaterPark,
                    rules);
            };

            prefab.Register();
        }
    }
}
