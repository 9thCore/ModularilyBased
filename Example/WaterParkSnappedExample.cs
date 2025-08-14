using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using UnityEngine;
using Nautilus.Utility;
using ModularilyBased.Library;
using Nautilus.Assets.Gadgets;
using ModularilyBased.Library.TransformRule;
using ModularilyBased.Library.TransformRule.Rotation;
using ModularilyBased.Library.PlaceRule;
using System.Linq;

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
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.MercuryOre, 1)
                ));

            prefab.SetPdaGroupCategory(TechGroup.InteriorPieces, TechCategory.InteriorPiece);

            CloneTemplate template = new CloneTemplate(Info, TechType.Centrifuge);
            prefab.SetGameObject(template);

            template.ModifyPrefab += (GameObject obj) =>
            {
                GameObject.DestroyImmediate(obj.FindChild("HACK"));
                GameObject model = obj.GetComponentInChildren<Renderer>().transform.parent.gameObject;
                GameObject.DestroyImmediate(obj.GetComponent<TechTag>());

                PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);

                Constructable constructable = PrefabUtils.AddConstructable(obj, Info.TechType, ConstructableFlags.None, model);

                ConstructableBounds bounds = obj.EnsureComponent<ConstructableBounds>();
                OrientedBounds.EncapsulateRenderers(obj.transform.worldToLocalMatrix, obj.GetComponentsInChildren<Renderer>().ToList(), out bounds.bounds.position, out bounds.bounds.extents);
                bounds.bounds.position += Vector3.forward * 0.1f;

                obj.EnsureComponent<BoxCollider>().size = new Vector3(0.3f, 0.5f, 0.5f);

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
