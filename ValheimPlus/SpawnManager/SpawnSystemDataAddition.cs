using System;
using System.Collections.Generic;
using ValheimPlus.Utility;

namespace ValheimPlus
{
    [Serializable]
    public class SpawnSystemDataAddition : IZPackageable
    {
        public string Name;
        public string ObjectName;
        public int MinimumLevel;
        public int MaximumLevel;
        public bool HuntPlayer;
        public bool SpawnAtDay;
        public bool SpawnAtNight;
        public float MinimumAltitude;
        public float MaximumAltitude;
        public float SpawnInterval;
        public float SpawnChance;
        public float SpawnRadiusMin;
        public float SpawnRadiusMax;
        public string RequiredGlobalKey; // Only spawn if this key is set
        public List<string> RequiredEnvironments = new List<string>(); // Only spawn if this environment is active
        public int GroupSizeMin;
        public int GroupSizeMax;
        public float SpawnDistance;   // Minimum distance to another instance
        public int MaxSpawned; //Total nr of instances (if near player is set, only instances within the max spawn radius is counted)
        public float LevelUpMinCenterDistance;
        public List<string> Biomes = new List<string>();
        public List<string> BiomeAreas = new List<string>();
       
        public SpawnSystemDataAddition()
        {

        }

        public SpawnSystemDataAddition(SpawnSystem.SpawnData data)
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
            RequiredEnvironments = data.m_requiredEnvironments;
            GroupSizeMin = data.m_groupSizeMin;
            GroupSizeMax = data.m_groupSizeMax;
            SpawnDistance = data.m_spawnDistance;
            MaxSpawned = data.m_maxSpawned;
            LevelUpMinCenterDistance = data.m_levelUpMinCenterDistance;

            if (data.m_biome.HasFlag(Heightmap.Biome.Meadows))      Biomes.Add(Heightmap.Biome.Meadows.ToString());
            if (data.m_biome.HasFlag(Heightmap.Biome.Swamp))        Biomes.Add(Heightmap.Biome.Swamp.ToString());
            if (data.m_biome.HasFlag(Heightmap.Biome.Mountain))     Biomes.Add(Heightmap.Biome.Mountain.ToString());
            if (data.m_biome.HasFlag(Heightmap.Biome.BlackForest))  Biomes.Add(Heightmap.Biome.BlackForest.ToString());
            if (data.m_biome.HasFlag(Heightmap.Biome.Plains))       Biomes.Add(Heightmap.Biome.Plains.ToString());
            if (data.m_biome.HasFlag(Heightmap.Biome.AshLands))     Biomes.Add(Heightmap.Biome.AshLands.ToString());
            if (data.m_biome.HasFlag(Heightmap.Biome.DeepNorth))    Biomes.Add(Heightmap.Biome.DeepNorth.ToString());
            if (data.m_biome.HasFlag(Heightmap.Biome.Ocean))        Biomes.Add(Heightmap.Biome.Ocean.ToString());
            if (data.m_biome.HasFlag(Heightmap.Biome.Mistlands))    Biomes.Add(Heightmap.Biome.Mistlands.ToString());

            if (data.m_biomeArea.HasFlag(Heightmap.BiomeArea.Edge))          BiomeAreas.Add(Heightmap.BiomeArea.Edge.ToString());
            if (data.m_biomeArea.HasFlag(Heightmap.BiomeArea.Median))        BiomeAreas.Add(Heightmap.BiomeArea.Median.ToString());
            if (data.m_biomeArea.HasFlag(Heightmap.BiomeArea.Everything))    BiomeAreas.Add(Heightmap.BiomeArea.Everything.ToString());
        }

        /// <summary>
        /// Serialize the object into a ZPackage
        /// </summary>
        /// <param name="package"></param>
        public void Serialize(ZPackage package)
        {
            package.Write(Name);
            package.Write(ObjectName);
            package.Write(MinimumLevel);
            package.Write(MaximumLevel);
            package.Write(HuntPlayer);
            package.Write(SpawnAtDay);
            package.Write(SpawnAtNight);
            package.Write(MinimumAltitude);
            package.Write(MaximumAltitude);
            package.Write(SpawnInterval);
            package.Write(SpawnChance);
            package.Write(SpawnRadiusMin);
            package.Write(SpawnRadiusMax);
            package.Write(RequiredGlobalKey);
            package.Write(RequiredEnvironments);
            package.Write(GroupSizeMin);
            package.Write(GroupSizeMax);
            package.Write(SpawnDistance);
            package.Write(MaxSpawned);
            package.Write(LevelUpMinCenterDistance);
            package.Write(Biomes);
            package.Write(BiomeAreas);
        }
        
        /// <summary>
        /// Unserialize from a ZPackage into this object
        /// </summary>
        /// <param name="package"></param>
        public void Unserialize(ZPackage package)
        {
            Name                        = package.ReadString();
            ObjectName                  = package.ReadString();
            MinimumLevel                = package.ReadInt();
            MaximumLevel                = package.ReadInt();
            HuntPlayer                  = package.ReadBool();
            SpawnAtDay                  = package.ReadBool();
            SpawnAtNight                = package.ReadBool();
            MinimumAltitude             = package.ReadSingle();
            MaximumAltitude             = package.ReadSingle();
            SpawnInterval               = package.ReadSingle();
            SpawnChance                 = package.ReadSingle();
            SpawnRadiusMin              = package.ReadSingle();
            SpawnRadiusMax              = package.ReadSingle();
            RequiredGlobalKey           = package.ReadString();
            RequiredEnvironments        = package.ReadStringList();
            GroupSizeMin                = package.ReadInt();
            GroupSizeMax                = package.ReadInt();
            SpawnDistance               = package.ReadSingle();
            MaxSpawned                  = package.ReadInt();
            LevelUpMinCenterDistance    = package.ReadSingle();
            Biomes                      = package.ReadStringList();
            BiomeAreas                  = package.ReadStringList();
        }
    }
}
