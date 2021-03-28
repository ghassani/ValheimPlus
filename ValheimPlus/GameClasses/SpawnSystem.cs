using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace ValheimPlus.GameClasses
{
    [HarmonyPatch(typeof(SpawnSystem), "Awake")]
    public static class SpawnSystem_Awake_Patch
    {
        private static void Postfix(ref SpawnSystem __instance)
        {
            if (SpawnManager.instance != null)
            {
                SpawnManager.instance.SyncSpawnSystem(__instance);
            }
        }
    }
}
