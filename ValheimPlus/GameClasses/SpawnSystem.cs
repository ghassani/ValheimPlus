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
    [HarmonyPatch(typeof(SpawnSystem), "Awake")]
    public static class SpawnSystem_Awake_Patch
    {
        private static void Postfix(ref SpawnSystem __instance)
        {
            if (ZNet.instance != null && !ZNet.instance.IsServer())  return;
            
            Debug.Log($"SpawnSystem.Awake() Spawner Count: {__instance.m_spawners.Count}");
            
            //SpawnManagerMod.Instance.SpawnManager.AddSpawnSystem(__instance);
        }
    }

    [HarmonyPatch(typeof(SpawnSystem), "UpdateSpawnList")]
    public static class SpawnSystem_UpdateSpawnList_Patch
    {
        static void Prefix(ref SpawnSystem __instance, ref List<SpawnSystem.SpawnData> spawners, DateTime currentTime, bool eventSpawners)
        {
            if (ZNet.instance != null && !ZNet.instance.IsServer())  return;
            
            Debug.Log($"[PREFIX] SpawnSystem.UpdateSpawnList() Spawner Count: {__instance.m_spawners.Count}");

            //spawnManagerMod.Instance.SpawnManager.AddSpawnSystem(__instance);

            foreach(SpawnSystem.SpawnData data in spawners)
            {
                data.m_minLevel = 3;
                data.m_maxLevel = 3;
                data.m_levelUpMinCenterDistance = 0f;
            }
           
        }

        static void Postfix(ref SpawnSystem __instance, ref List<SpawnSystem.SpawnData> spawners, DateTime currentTime, bool eventSpawners)
        {
            if (ZNet.instance != null && !ZNet.instance.IsServer())  return;
            
            Debug.Log($"[POSTFIX] SpawnSystem.UpdateSpawnList() Spawner Count: {__instance.m_spawners.Count}");
            
            /*foreach(SpawnSystem.SpawnData data in __instance.m_spawners)
            {
                data.m_minLevel = 3;
                data.m_maxLevel = 3;
                data.m_levelUpMinCenterDistance = 0f;
            }*/

        }
    }
    
    [HarmonyPatch(typeof(SpawnSystem), "Spawn")]
    public static class SpawnSystem_Spawn_Patch
    {
        static void Prefix(ref SpawnSystem.SpawnData critter, Vector3 spawnPoint, bool eventSpawner)
        {
            
        }

        static void Postfix(ref SpawnSystem.SpawnData critter, Vector3 spawnPoint, bool eventSpawner)
        {
            if (ZNet.instance != null && !ZNet.instance.IsServer())  return;
            
            Debug.Log($"SpawnSystem.Spawn() {critter.m_name} (Prefab: {critter.m_prefab.name} - From Event: {eventSpawner} Location {spawnPoint.ToString()} Min Level: {critter.m_minLevel} Max Level: {critter.m_maxLevel} Level Up Distance {critter.m_levelUpMinCenterDistance}");
        }
    }
}
