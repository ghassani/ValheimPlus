using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValheimPlus
{
     public class CreatureSpawnerConfigEntry
    {
        public String Name;
        public String ObjectName;
        public bool? RequireSpawnArea;
        public bool? SpawnAtDay;
        public bool? SpawnAtNight;
        public bool? SpawnInPlayerBase;
        public float? TriggerDistance;
        public float? RespawnTimeMinutes;
        public float? LevelupChance;
        public int? MinLevel;
        public int? MaxLevel;
        public float? TriggerNoise;

        public CreatureSpawnerConfigEntry()
        {

        }

        public CreatureSpawnerConfigEntry(CreatureSpawner spawner)
        {
            Name = spawner.name;
            ObjectName = spawner.m_creaturePrefab.name;
            RequireSpawnArea = spawner.m_requireSpawnArea;
            SpawnAtDay = spawner.m_spawnAtDay;
            SpawnAtNight = spawner.m_spawnAtNight;
            SpawnInPlayerBase = spawner.m_spawnInPlayerBase;
            TriggerDistance = spawner.m_triggerDistance;
            RespawnTimeMinutes = spawner.m_respawnTimeMinuts;
            LevelupChance = spawner.m_levelupChance;
            MinLevel = spawner.m_minLevel;
            MaxLevel = spawner.m_maxLevel;
            TriggerNoise = spawner.m_triggerNoise;
        }
    }
}
