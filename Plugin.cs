using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    internal class Plugin : BaseUnityPlugin
    {
        internal new static ManualLogSource Logger { get; private set; }

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
            InitializeLibrary();

            // register harmony patches, if there are any
            Harmony.CreateAndPatchAll(Assembly, $"{PluginInfo.PLUGIN_GUID}");
            Logger.LogInfo($"{PluginInfo.PLUGIN_GUID} has loaded. Happy base-building!");
        }

        private void InitializeLibrary()
        {
            string directory = "Definitions";

            if (!RoomFaceData.TryAddRoomDefinitionsDirectory(directory))
            {
                Logger.LogError($"Could not find directory {Path.GetDirectoryName(Assembly.Location)}/{directory}. Default room data could not be loaded");
            }
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
            WaterParkSnappedExample.Register();
        }

        private void Update()
        {
            if (!Plugin.debugMode.Value)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                Plugin.Logger.LogInfo($"Beginning room cache...");

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
                            data = new RoomFaceData(RoomFaceData.GetJsonPath("ModularilyBased", filename));
                            cache.Add(filename, data);
                        }

                        string extraID = type.ToString();

                        if (!data.storage.TryGetValue(extraID, out List<FaceData> faces))
                        {
                            faces = new();
                            data.storage.Add(extraID, faces);
                        }

                        GameObject root = cell.GetComponent<SnapHolder>().root;
                        foreach (BaseFaceIdentifier identifier in root.GetComponentsInChildren<BaseFaceIdentifier>(true))
                        {
                            Transform face = identifier.transform;
                            Transform collider = identifier.Collider.transform;

                            FaceData faceData = new FaceData()
                            {
                                face = identifier.Face,
                                seabaseFaces = identifier.SeabaseFaces,
                                scale = collider.localScale,
                                position = face.localPosition,
                                rotation = face.localRotation,
                            };

                            bool unique = faces
                                .All(face =>
                                {
                                    if (face.face != faceData.face)
                                    {
                                        return true;
                                    }

                                    return !face.seabaseFaces.SequenceEqual(faceData.seabaseFaces);
                                });

                            if (unique)
                            {
                                Logger.LogInfo($"Added ({faceData.face}, {faceData.seabaseFaces}) to {filename}/{extraID}");
                                faces.Add(faceData);
                            }
                        }
                    }
                }

                foreach (KeyValuePair<string, RoomFaceData> pair in cache)
                {
                    pair.Value.SaveWithConverters(new Int3Converter());
                    pair.Value.storage.Keys.ForEach(key => Logger.LogInfo($"Successfully saved into {pair.Key}/{key}"));
                }
            }
        }
    }
}