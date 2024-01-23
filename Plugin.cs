using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using TwoHandedCompany.Patches;

namespace TwoHandedCompany
{
    public class PLUGIN_INFO
    {
        public const string modGUID = "Ken.TwoHandedCompany";
        public const string modName = "Two Handed Company";
        public const string modVersion = "1.0.0";
    }

    [BepInPlugin(PLUGIN_INFO.modGUID, PLUGIN_INFO.modName, PLUGIN_INFO.modVersion)]
    public class TwoHandedBase : BaseUnityPlugin
    {
        Harmony harmony = new Harmony(PLUGIN_INFO.modGUID);
        ManualLogSource mls;

        private void Awake()
        {
            mls = BepInEx.Logging.Logger.CreateLogSource(PLUGIN_INFO.modGUID);
            mls.LogInfo($"{PLUGIN_INFO.modName} loaded");
            harmony.PatchAll(typeof(TwoHandedBase));
            harmony.PatchAll(typeof(PlayerControllerBPatch));
        }
    }
}

namespace TwoHandedCompany.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        [HarmonyPatch("BeginGrabObject")]
        [HarmonyPostfix]
        private static void UpdateBeginGrabObject(ref bool ___twoHanded)
        {
            ___twoHanded = ((StartOfRound.Instance?.currentLevel.levelID) != 3 || StartOfRound.Instance.inShipPhase) && ___twoHanded;
        }

        [HarmonyPatch("GrabObjectClientRpc")]
        [HarmonyPostfix]
        private static void UpdateGrabObjectClientRpc(ref bool ___twoHanded)
        {
            ___twoHanded = ((StartOfRound.Instance?.currentLevel.levelID) != 3 || StartOfRound.Instance.inShipPhase) && ___twoHanded;
        }

        [HarmonyPatch("SwitchToItemSlot")]
        [HarmonyPostfix]
        private static void UpdateSwitchToItemSlot(ref bool ___twoHanded)
        {
            ___twoHanded = ((StartOfRound.Instance?.currentLevel.levelID) != 3 || StartOfRound.Instance.inShipPhase) && ___twoHanded;
        }
    }
}