using HarmonyLib;
using UnityEngine;

namespace ValheimPlus.GameClasses
{
    [HarmonyPatch(typeof(SpawnArea), "Awake")]
    public static class SpawnArea_Patch_Awake
    {
        private static void Postfix(ref SpawnArea __instance)
        {
            if (SpawnManager.instance != null)
            {
                SpawnManager.instance.SyncSpawnArea(__instance);
            }
        }
    }
}
