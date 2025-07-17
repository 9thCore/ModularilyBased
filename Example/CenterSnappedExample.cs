using ModularilyBased.Library;
using ModularilyBased.Library.PlaceRule;
using ModularilyBased.Library.TransformRule;
using ModularilyBased.Library.TransformRule.Rotation;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Utility;
using System;
using TMPro;
using UnityEngine;

namespace ModularilyBased.Example
{
    internal class CenterSnappedExample
    {
        public static PrefabInfo Info { get; private set; }

        public static void Register()
        {
            Info = PrefabInfo
                .WithTechType("CenterSnappedExample", "Center snapped example", "A Bioreactor lookalike.")
                .WithIcon(SpriteManager.Get(TechType.BaseBioReactor));

            CustomPrefab prefab = new CustomPrefab(Info);

            prefab.SetRecipe(new Nautilus.Crafting.RecipeData(
                new CraftData.Ingredient(TechType.Titanium, 3)
                ));

            prefab.SetPdaGroupCategory(TechGroup.InteriorPieces, TechCategory.InteriorPiece);

            prefab.SetGameObject(GetGameObject);
            prefab.Register();
        }

        public static GameObject GetGameObject()
        {
            BaseBioReactorGeometry[] geometries = Resources.FindObjectsOfTypeAll<BaseBioReactorGeometry>();
            if (geometries.Length == 0)
            {
                throw new InvalidOperationException("???");
            }

            // Don't take the "GlassDome" version
            BaseBioReactorGeometry geometry = geometries[0].name.Contains("Dome") ? geometries[1] : geometries[0];

            GameObject prefab = GameObject.Instantiate(geometry.gameObject);
            prefab.SetActive(false);
            prefab.name = Info.TechType.ToString();

            prefab.EnsureComponent<CenterSnappedExampleBehaviour>();
            GameObject.Destroy(geometry);

            GameObject model = prefab.transform.Find("Bio_reactor").gameObject;

            PrefabUtils.AddBasicComponents(prefab, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Global);

            Constructable constructable = PrefabUtils.AddConstructable(
                    prefab,
                    Info.TechType,
                    ConstructableFlags.Rotatable,
                    model);

            ConstructableBounds[] allBounds = prefab.GetComponents<ConstructableBounds>();
            foreach (ConstructableBounds bounds in allBounds)
            {
                bounds.bounds.size -= new Vector3(0f, 1f, 0f);
            }

            TransformationRule rule = new TransformationRule()
                .WithRotationRule(SnappedRotationRule.NoOffsetCardinal);

            ModuleSnapper.SetSnappingRules(
                constructable,
                ModuleSnapper.RoomRule.SmallRoom | ModuleSnapper.RoomRule.LargeRoom,
                PlacementRule.SnapToCenter,
                rule);

            MaterialUtils.ApplySNShaders(model);

            return prefab;
        }

        public class CenterSnappedExampleBehaviour : MonoBehaviour
        {
            public TextMeshProUGUI text;
            public PowerSource powerSource;

            public void Start()
            {
                powerSource = gameObject.EnsureComponent<PowerSource>();
                powerSource.maxPower = 100f;

                text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
                text.text = $"<color=#00ff00>yo this is kinda cool ngl</color>";
            }

            public void Update() {
                float available = powerSource.maxPower - powerSource.power;
                if (available > 0f)
                {
                    float generated = BaseBioReactor.powerPerSecond * DayNightCycle.main.deltaTime;
                    if (available < generated)
                    {
                        generated = available;
                    }

                    powerSource.AddEnergy(generated, out _);
                }
            }
        }
    }
}
