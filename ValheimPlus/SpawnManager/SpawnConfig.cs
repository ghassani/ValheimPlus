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
    public class SpawnConfig
    {

        public List<SpawnSystemConfigEntry> Systems = new List<SpawnSystemConfigEntry>();
        public List<SpawnAreaConfigEntry> Areas = new List<SpawnAreaConfigEntry>();
        public List<CreatureSpawnerConfigEntry> CreatureSpawners = new List<CreatureSpawnerConfigEntry>();

        public SpawnConfig()
        {

        }
    }
}
