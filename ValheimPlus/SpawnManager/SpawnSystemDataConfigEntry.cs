using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValheimPlus
{
    

    public class SpawnSystemDataConfigEntry
    {
        String Name;
        String ObjectName;
        int? MinimumLevel;
        int? MaximumLevel;
        bool? HuntPlayer;
        bool? SpawnAtDay;
        bool? SpawnAtNight;
        float? MinimumAltitude;
        float? MaximumAltitude;
        float? SpawnInterval;
        float? SpawnChance;
        float? SpawnRadiusMin;
        float? SpawnRadiusMax;
        string RequiredGlobalKey; // Only spawn if this key is set
        string[] RequiredEnvironments; // Only spawn if this environment is active
        int? GroupSizeMin;
        int? GroupSizeMax;
        float? SpawnDistance;   // Minimum distance to another instance
        int? MaxSpawned; //Total nr of instances (if near player is set, only instances within the max spawn radius is counted)
        float? LevelUpMinCenterDistance;

        public SpawnSystemDataConfigEntry()
        {

        }

        public SpawnSystemDataConfigEntry(SpawnSystem.SpawnData data)
        {
            Name = data.m_name;
            ObjectName = data.m_prefab.name;
            MinimumLevel = data.m_minLevel;
            MaximumLevel = data.m_maxLevel;
            HuntPlayer = data.m_huntPlayer;
            SpawnAtDay = data.m_spawnAtDay;
            SpawnAtNight = data.m_spawnAtNight;
            MinimumAltitude = data.m_minAltitude;
            MaximumAltitude = data.m_maxAltitude;
            SpawnInterval = data.m_spawnInterval;
            SpawnChance = data.m_spawnChance;
            SpawnRadiusMin = data.m_spawnRadiusMin;
            SpawnRadiusMax = data.m_spawnRadiusMax;
            RequiredGlobalKey = data.m_requiredGlobalKey;
            RequiredEnvironments = data.m_requiredEnvironments.ToArray();
            GroupSizeMin = data.m_groupSizeMin;
            GroupSizeMax = data.m_groupSizeMax;
            SpawnDistance = data.m_spawnDistance;
            MaxSpawned = data.m_maxSpawned;
            LevelUpMinCenterDistance = data.m_levelUpMinCenterDistance;
        }
    }
}
