using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using ModularilyBased.Example;

namespace ModularilyBased
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.snmodding.nautilus")]
    public class Plugin : BaseUnityPlugin
    {
        public new static ManualLogSource Logger { get; private set; }

        private static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

        public ConfigEntry<bool> register;

        private void Awake()
        {
            register = Config.Bind("General",
                "RegisterExample",
                false,
                "Whether to register library examples into the game. Meant just to provide working buildables.");

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
    }
}