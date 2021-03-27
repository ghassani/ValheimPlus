using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace ValheimPlus.GameClasses
{
    [HarmonyPatch(typeof(CreatureSpawner), "Awake")]
    public static class CreatureSpawner_Patch_Awake
    {
        private static void Postfix(ref CreatureSpawner __instance)
        {
            if (ZNet.instance != null && !ZNet.instance.IsServer())  return;
            
            Debug.Log($"CreatureSpawner.Awake() {__instance.name}");
            
            __instance.m_minLevel = 3;
            __instance.m_maxLevel = 3;

            //SpawnManagerMod.Instance.SpawnManager.AddCreatureSpawner(__instance);
        }
    }

    [HarmonyPatch(typeof(CreatureSpawner), "Spawn")]
    public static class CreatureSpawner_Patch_Spawn
    {
        private static void Postfix(ref CreatureSpawner __instance, ref ZNetView __result)
        {
            //if (ZNet.instance != null && !ZNet.instance.IsServer())  return;
            
            Debug.Log($"CreatureSpawner.Spawn() {__instance.name}");
        }
    }
}