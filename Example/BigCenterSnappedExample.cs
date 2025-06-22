using ModularilyBased.Library;
using ModularilyBased.Library.PlaceRule;
using ModularilyBased.Library.PlaceRule.Filter;
using ModularilyBased.Library.TransformRule;
using ModularilyBased.Library.TransformRule.Position;
using ModularilyBased.Library.TransformRule.Rotation;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Utility;
using System;
using TMPro;
using UnityEngine;

namespace ModularilyBased.Example
{
    public class BigCenterSnappedExample
    {
        public static PrefabInfo Info { get; private set; }

        public static void Register()
        {
            Info = PrefabInfo
                .WithTechType("BigCenterSnappedExample", "Big center snapped example", "An example which is big, fit for a large room.")
                .WithIcon(SpriteManager.Get(TechType.Magnetite));

            CustomPrefab prefab = new CustomPrefab(Info);

            prefab.SetRecipe(new Nautilus.Crafting.RecipeData(
                new CraftData.Ingredient(TechType.AdvancedWiringKit, 4),
                new CraftData.Ingredient(TechType.AcidMushroom)
                ));
            prefab.SetPdaGroupCategory(TechGroup.InteriorPieces, TechCategory.InteriorPiece);

            CloneTemplate template = new CloneTemplate(Info, "cc14ee20-80c5-4573-ae1b-68bebc0feadf");

            template.ModifyPrefab += (GameObject obj) =>
            {
                obj.transform.localScale = new Vector3(obj.transform.localScale.x * 3, obj.transform.localScale.y, obj.transform.localScale.z);

                GameObject model = obj.GetComponentInChildren<MeshRenderer>().gameObject;

                PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);
                Constructable constructable = PrefabUtils.AddConstructable(obj, Info.TechType, ConstructableFlags.None, model);

                ConstructableBounds bounds = obj.EnsureComponent<ConstructableBounds>();
                bounds.bounds.size = new Vector3(6f, 0.25f, 1f); // who tf knows what these units mean

                PlacementRule placement = new FilteredPlacementRule(PlacementRule.SnapType.Center)
                .AddFilter(LargeRoomPlacementFilter.HalfWidth);

                TransformationRule transformation = new TransformationRule()
                .WithRotationRule(SnappedRotationRule.NoOffsetCardinal)
                .WithPositionRule(new OffsetPositionRule(0f, -1.25f, 2.5f));

                ModuleSnapper.SetSnappingRules(
                    constructable,
                    ModuleSnapper.RoomRule.LargeRoom,
                    placement,
                    transformation);
            };

            prefab.SetGameObject(template);
            prefab.Register();
        }
    }
}
