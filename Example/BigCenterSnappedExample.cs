using ModularilyBased.Library;
using ModularilyBased.Library.PlaceRule;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Utility;
using UnityEngine;

namespace ModularilyBased.Example
{
    internal class BigCenterSnappedExample
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

            prefab.SetGameObject(GetGameObject);
            prefab.Register();
        }

        public static GameObject GetGameObject()
        {
            GameObject obj = new GameObject();
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;

            GameObject model = GameObject.CreatePrimitive(PrimitiveType.Cube);
            model.transform.SetParent(obj.transform);
            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;

            model.transform.localScale = new Vector3(7.5f, 2.5f, 1f);

            PrefabUtils.AddBasicComponents(obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);
            Constructable constructable = PrefabUtils.AddConstructable(obj, Info.TechType, ConstructableFlags.None, model);
            constructable.placeDefaultDistance = 4f;
            
            MaterialUtils.ApplySNShaders(model);

            ConstructableBounds bounds = obj.EnsureComponent<ConstructableBounds>();
            bounds.bounds.size = model.transform.localScale;
            
            ModuleSnapper.SetSnappingRules(
                constructable,
                ModuleSnapper.RoomRule.LargeRoom,
                PlacementRule.LargeRoomDoubleFace);

            return obj;
        }
    }
}
