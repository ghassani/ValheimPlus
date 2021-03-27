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
    [HarmonyPatch(typeof(SpawnArea), "Awake")]
    public static class SpawnArea_Patch_Awake
    {
        private static void Postfix(ref SpawnArea __instance)
        {
            if (ZNet.instance != null && !ZNet.instance.IsServer())  return;
            
            Debug.Log("SpawnArea.Awake()");

            foreach(SpawnArea.SpawnData data in __instance.m_prefabs)
            {
                data.m_minLevel = 3;
                data.m_maxLevel = 3;

                Debug.Log($"SpawnArea.Awake() - Data: {data.m_prefab.name}");
            }

            //SpawnManagerMod.Instance.SpawnManager.AddSpawnArea(__instance);
        }
    }

    [HarmonyPatch(typeof(SpawnArea), "SpawnOne")]
    public static class SpawnArea_Patch_SpawnOne
    {
        private static void Postfix(ref SpawnArea __instance)
        {
            if (ZNet.instance != null && !ZNet.instance.IsServer())  return;
            
            Debug.Log("SpawnArea.SpawnOne()");
        }
    }
}