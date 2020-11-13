using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;

namespace FasterPlaylistScroll
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        public const string HarmonyId = "com.github.pulselane.FasterPlaylistScroll";
        internal static Harmony harmony;

        [Init]
        public Plugin(IPALogger logger, Config conf)
        {
            Logger.log = logger;
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
        }

        [OnEnable]
        public void OnEnable()
        {
            harmony = new Harmony(HarmonyId);
            ApplyHarmonyPatches();
        }

        [OnDisable]
        public void OnDisable()
        {
            RemoveHarmonyPatches();
        }

        public static void ApplyHarmonyPatches()
        {
            try
            {
                Logger.log.Debug("Applying Harmony patches.");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Logger.log.Critical("Error applying Harmony patches: " + ex.Message);
                Logger.log.Debug(ex);
            }
        }

        public static void RemoveHarmonyPatches()
        {
            try
            {
                harmony.UnpatchAll(HarmonyId);
            }
            catch (Exception ex)
            {
                Logger.log.Critical("Error removing Harmony patches: " + ex.Message);
                Logger.log.Debug(ex);
            }
        }
    }
}
