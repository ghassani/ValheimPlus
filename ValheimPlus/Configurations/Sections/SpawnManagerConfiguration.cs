using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValheimPlus.Configurations.Sections
{
    public class SpawnManagerConfiguration : BaseConfig<SpawnManagerConfiguration>
    {
        public string databaseFile { get; set; } = "spawns.json";
    }
}
