using HarmonyLib;

namespace ValheimPlus.GameClasses
{
    [HarmonyPatch(typeof(CreatureSpawner), "Awake")]
    public static class CreatureSpawner_Patch_Awake
    {
        private static void Postfix(ref CreatureSpawner __instance)
        {
            if (SpawnManager.instance != null)
            {
               SpawnManager.instance.SyncCreatureSpawner(__instance);
            }
        }
    }
}