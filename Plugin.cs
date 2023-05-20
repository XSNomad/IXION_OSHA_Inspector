using BepInEx;
using BepInEx.Unity.IL2CPP;
using BulwarkStudios.Stanford.Torus.Buildings;
using HarmonyLib;

namespace OSHA_Inspector;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    public override void Load()
    {
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();

        foreach (var patchedMethod in harmony.GetPatchedMethods())
        {
            Log.LogInfo($"Patched: {patchedMethod.DeclaringType?.FullName}:{patchedMethod}");
        }
    }
}
public class NoAccidents
{
    [HarmonyPatch(typeof(CommandSectorAccidentManagement), nameof(CommandSectorAccidentManagement.BulwarkStudios_Stanford_Core_Commands_ICommandCustomTickable_OnCustomTick))]
    public class CommandSectorAccidentManagement_BulwarkStudios_Stanford_Core_Commands_ICommandCustomTickable_OnCustomTick_Patch
    {
        public static void Prefix(CommandSectorAccidentManagement __instance)
        {
            __instance.ResetAccidentGauge();
        }
    }
    [HarmonyPatch(typeof(CommandSectorAccidentManagement), nameof(CommandSectorAccidentManagement.GetTremorChance))]
    public class CommandSectorAccidentManagement_GetTremorChance_Patch
    {
        public static void Postfix(ref float __result)
        {
            __result = 0f;
        }
    }
}
