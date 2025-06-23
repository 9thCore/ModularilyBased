using System.Collections.Generic;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using ModularilyBased.Example;
using ModularilyBased.JSON;
using ModularilyBased.Library;
using UnityEngine;

namespace ModularilyBased
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.snmodding.nautilus")]
    public class Plugin : BaseUnityPlugin
    {
        public new static ManualLogSource Logger { get; private set; }

        private static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

        public static ConfigEntry<bool> debugMode;
        public ConfigEntry<bool> register;

        private void Awake()
        {
            register = Config.Bind("General",
                "RegisterExample",
                false,
                "Whether to register library examples into the game. Meant just to provide working buildables.");

            debugMode = Config.Bind("General",
                "DebugMode",
                false,
                "Whether DEBUG MODE is enabled. Allows saving face data in built bases in the current save by pressing 'V', and shows colliders as boxes.");

            // set project-scoped logger instance
            Logger = base.Logger;

            // Initialize custom prefabs
            InitializePrefabs();

            // register harmony patches, if there are any
            Harmony.CreateAndPatchAll(Assembly, $"{PluginInfo.PLUGIN_GUID}");
            Logger.LogInfo($"{PluginInfo.PLUGIN_GUID} has loaded. Happy base-building!");
        }

        private void InitializePrefabs()
        {
            if (!register.Value)
            {
                return;
            }

            WallSnappedExample.Register();
            CenterSnappedExample.Register();
            BigCenterSnappedExample.Register();
        }

        private void Update()
        {
            if (!Plugin.debugMode.Value)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                Dictionary<string, RoomFaceData> cache = new();

                foreach (Base seabase in GameObject.FindObjectsOfType<Base>())
                {
                    foreach (BaseCell cell in seabase.GetComponentsInChildren<BaseCell>())
                    {
                        BaseDeconstructable decon = cell.GetComponentInChildren<BaseDeconstructable>();

                        Base.CellType type = Base.CellType.Empty;

                        for (int i = 0; i < seabase.cellObjects.Length; i++)
                        {
                            if (cell.transform == seabase.cellObjects[i])
                            {
                                type = seabase.GetCell(i);
                                break;
                            }
                        }

                        string filename = decon.recipe.ToString();

                        RoomFaceData data = cache.GetOrDefault(filename, null);
                        if (data == null)
                        {
                            data = new RoomFaceData(filename);
                            cache.Add(filename, data);
                        }

                        string extraID = type.ToString();

                        if (data.storage.ContainsKey(extraID))
                        {
                            return;
                        }

                        List<FaceData> faces = new();
                        data.storage.Add(extraID, faces);

                        foreach (BaseFaceIdentifier identifier in cell.GetComponentsInChildren<BaseFaceIdentifier>())
                        {
                            Transform face = identifier.transform;
                            Transform collider = identifier.Collider.transform;

                            FaceData faceData = new FaceData()
                            {
                                face = identifier.Face,
                                // faceCell = new Vector3Int(identifier.SeabaseFace.cell.x, identifier.SeabaseFace.cell.y, identifier.SeabaseFace.cell.z),
                                // faceDirection = identifier.SeabaseFace.direction,
                                seabaseFace = identifier.SeabaseFace,
                                centerFaceIndex = identifier.CenterFaceIndex,
                                scale = collider.localScale,
                                position = face.localPosition,
                                colliderPosition = collider.localPosition,
                                rotation = face.localRotation,
                                colliderRotation = collider.localRotation
                            };

                            faces.Add(faceData);
                        }
                    }
                }

                foreach (RoomFaceData data in cache.Values)
                {
                    data.SaveWithConverters(new Int3Converter());
                }
            }
        }
    }
}