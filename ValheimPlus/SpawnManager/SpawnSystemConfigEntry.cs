using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValheimPlus
{
    

    /// <summary>
    /// SpawnSystemConfigEntry
    /// </summary>
    public class SpawnSystemConfigEntry
    {
        public String Name;
        public String ObjectName;
        public float LevelUpChance;
        public SpawnSystemDataConfigEntry[] Data;

        public SpawnSystemConfigEntry()
        {

        }

        public SpawnSystemConfigEntry(SpawnSystem system)
        {
            Name = system.name;
            ObjectName = system.gameObject != null ? system.gameObject.name : "";

            LevelUpChance = system.m_levelupChance;            
            List<SpawnSystemDataConfigEntry> ListData = new List<SpawnSystemDataConfigEntry>();

            foreach(SpawnSystem.SpawnData data in system.m_spawners)
            {
                ListData.Add(new SpawnSystemDataConfigEntry(data));
            }

            Data = ListData.ToArray();
        }
    }
}
