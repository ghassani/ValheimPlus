using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValheimPlus
{
    public class SpawnAreaDataConfigEntry
    {
        String ObjectName;
        public int MinLevel;
        public int MaxLevel;
        public float Weight;
        
        public SpawnAreaDataConfigEntry()
        {

        }
        public SpawnAreaDataConfigEntry(SpawnArea.SpawnData data)
        {
            ObjectName = data.m_prefab.name;
            MinLevel = data.m_minLevel;
            MaxLevel = data.m_maxLevel;
            Weight = data.m_weight;
        }
    }
}
