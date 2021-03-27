using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValheimPlus
{
public class SpawnAreaConfigEntry
    {
        public String Name;
        public String ObjectName;
       // public float? SpawnTimer;
        public bool? OnGroundOnly;
        public int? MaxTotal;
        public int? MaxNear;
        public float? FarRadius;
        public float? SpawnRadius;
        public float? TriggerDistance;
        public float? SpawnIntervalSec;
        public float? LevelupChance;
        public float? NearRadius;
        public SpawnAreaDataConfigEntry[] Data;

        public SpawnAreaConfigEntry()
        {

        }

        public SpawnAreaConfigEntry(SpawnArea area)
        {
            Name = area.name;
            ObjectName = "";
            //SpawnTimer = area.m_spawnTimer;
            OnGroundOnly = area.m_onGroundOnly;
            MaxTotal = area.m_maxTotal;
            MaxNear = area.m_maxNear;
            FarRadius = area.m_farRadius;
            SpawnRadius = area.m_spawnRadius;
            TriggerDistance = area.m_triggerDistance;
            SpawnIntervalSec = area.m_spawnIntervalSec;
            LevelupChance = area.m_levelupChance;
            NearRadius = area.m_nearRadius;

            List<SpawnAreaDataConfigEntry> ListData = new List<SpawnAreaDataConfigEntry>();

            foreach(SpawnArea.SpawnData data in area.m_prefabs)
            {
                ListData.Add(new SpawnAreaDataConfigEntry(data));
            }

            Data = ListData.ToArray();
        }
    }
}
