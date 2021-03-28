using System;
using System.Collections.Generic;
using ValheimPlus.Utility;

namespace ValheimPlus
{
    [Serializable]
    public class SpawnSystemDataModifierModification : IZPackageable
    {
        public int? MinimumLevel;
        public int? MaximumLevel;
        public bool? HuntPlayer;
        public bool? SpawnAtDay;
        public bool? SpawnAtNight;
        public float? MinimumAltitude;
        public float? MaximumAltitude;
        public float? SpawnInterval;
        public float? SpawnChance;
        public float? SpawnRadiusMin;
        public float? SpawnRadiusMax;
        public string RequiredGlobalKey = null; // Only spawn if this key is set
        public List<string> RequiredEnvironments; // Only spawn if this environment is active
        public int? GroupSizeMin;
        public int? GroupSizeMax;
        public float? SpawnDistance;   // Minimum distance to another instance
        public int? MaxSpawned; //Total nr of instances (if near player is set, only instances within the max spawn radius is counted)
        public float? LevelUpMinCenterDistance;
        public List<string> Biomes;
        public List<string> BiomeAreas;

        public SpawnSystemDataModifierModification()
        {

        }

        public void Apply(SpawnSystem system, SpawnSystem.SpawnData data)
        {
            if ( MinimumLevel != null )             data.m_minLevel                 = MinimumLevel.Value;
            if ( MaximumLevel != null )             data.m_maxLevel                 = MaximumLevel.Value;
            if ( HuntPlayer != null )               data.m_huntPlayer               = HuntPlayer.Value;
            if ( SpawnAtDay != null )               data.m_spawnAtDay               = SpawnAtDay.Value;
            if ( SpawnAtNight != null )             data.m_spawnAtNight             = SpawnAtNight.Value;
            if ( MinimumAltitude != null )          data.m_minAltitude              = MinimumAltitude.Value;
            if ( MaximumAltitude != null )          data.m_maxAltitude              = MaximumAltitude.Value;
            if ( SpawnInterval != null )            data.m_spawnInterval            = SpawnInterval.Value;
            if ( SpawnChance != null )              data.m_spawnChance              = SpawnChance.Value;
            if ( SpawnRadiusMin != null )           data.m_spawnRadiusMin           = SpawnRadiusMin.Value;
            if ( SpawnRadiusMax != null )           data.m_spawnRadiusMax           = SpawnRadiusMax.Value;
            if ( RequiredGlobalKey != null )        data.m_requiredGlobalKey        = RequiredGlobalKey;
            if ( GroupSizeMin != null )             data.m_groupSizeMin             = GroupSizeMin.Value;
            if ( GroupSizeMax != null )             data.m_groupSizeMax             = GroupSizeMax.Value;
            if ( SpawnDistance != null )            data.m_spawnDistance            = SpawnDistance.Value;
            if ( MaxSpawned != null )               data.m_maxSpawned               = MaxSpawned.Value;
            if ( LevelUpMinCenterDistance != null ) data.m_levelUpMinCenterDistance = LevelUpMinCenterDistance.Value;
            if ( RequiredEnvironments != null )     data.m_requiredEnvironments     = RequiredEnvironments;

            if (Biomes != null)
            {
                Heightmap.Biome biome_flags = Heightmap.Biome.None;

                if (Biomes.Contains("*"))
                {
                    biome_flags |= Heightmap.Biome.Meadows;
                    biome_flags |= Heightmap.Biome.Swamp;
                    biome_flags |= Heightmap.Biome.Mountain;
                    biome_flags |= Heightmap.Biome.BlackForest;
                    biome_flags |= Heightmap.Biome.Plains;
                    biome_flags |= Heightmap.Biome.AshLands;
                    biome_flags |= Heightmap.Biome.DeepNorth;
                    biome_flags |= Heightmap.Biome.Ocean;
                    biome_flags |= Heightmap.Biome.Mistlands;
                }
                else
                {
                    foreach (string b in Biomes)
                    {
                        if (b.Equals(Heightmap.Biome.Meadows.ToString(), StringComparison.OrdinalIgnoreCase))           biome_flags |= Heightmap.Biome.Meadows;
                        else if (b.Equals(Heightmap.Biome.Swamp.ToString(), StringComparison.OrdinalIgnoreCase))        biome_flags |= Heightmap.Biome.Swamp;
                        else if (b.Equals(Heightmap.Biome.Mountain.ToString(), StringComparison.OrdinalIgnoreCase))     biome_flags |= Heightmap.Biome.Mountain;
                        else if (b.Equals(Heightmap.Biome.BlackForest.ToString(), StringComparison.OrdinalIgnoreCase))  biome_flags |= Heightmap.Biome.BlackForest;
                        else if (b.Equals(Heightmap.Biome.Plains.ToString(), StringComparison.OrdinalIgnoreCase))       biome_flags |= Heightmap.Biome.Plains;
                        else if (b.Equals(Heightmap.Biome.AshLands.ToString(), StringComparison.OrdinalIgnoreCase))     biome_flags |= Heightmap.Biome.AshLands;
                        else if (b.Equals(Heightmap.Biome.DeepNorth.ToString(), StringComparison.OrdinalIgnoreCase))    biome_flags |= Heightmap.Biome.DeepNorth;
                        else if (b.Equals(Heightmap.Biome.Ocean.ToString(), StringComparison.OrdinalIgnoreCase))        biome_flags |= Heightmap.Biome.Ocean;
                        else if (b.Equals(Heightmap.Biome.Mistlands.ToString(), StringComparison.OrdinalIgnoreCase))    biome_flags |= Heightmap.Biome.Mistlands;
                    }
                }                

                data.m_biome = biome_flags;
            }

            if (BiomeAreas != null)
            {
                Heightmap.BiomeArea biome_area_flags = 0;

                if (BiomeAreas.Contains("*"))
                {
                    biome_area_flags |= Heightmap.BiomeArea.Edge;
                    biome_area_flags |= Heightmap.BiomeArea.Median;
                    biome_area_flags |= Heightmap.BiomeArea.Everything;
                }
                else
                {
                    foreach (string b in BiomeAreas)
                    {
                        if (b.Equals(Heightmap.BiomeArea.Edge.ToString(), StringComparison.OrdinalIgnoreCase))              biome_area_flags |= Heightmap.BiomeArea.Edge;
                        else if (b.Equals(Heightmap.BiomeArea.Median.ToString(), StringComparison.OrdinalIgnoreCase))       biome_area_flags |= Heightmap.BiomeArea.Median;
                        else if (b.Equals(Heightmap.BiomeArea.Everything.ToString(), StringComparison.OrdinalIgnoreCase))   biome_area_flags |= Heightmap.BiomeArea.Everything;
                    }
                }

                data.m_biomeArea = biome_area_flags;
            }            
        }

        /// <summary>
        /// Serialize the object into a ZPackage
        /// </summary>
        /// <param name="package"></param>
        public void Serialize(ZPackage package)
        {
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
            package.WriteNullableString(RequiredGlobalKey);
            package.WriteNullableStringList(RequiredEnvironments);
            package.Write(GroupSizeMin);
            package.Write(GroupSizeMax);
            package.Write(SpawnDistance);
            package.Write(MaxSpawned);
            package.Write(LevelUpMinCenterDistance);
            package.WriteNullableStringList(Biomes);
            package.WriteNullableStringList(BiomeAreas);
        }
        
        /// <summary>
        /// Unserialize from a ZPackage into this object
        /// </summary>
        /// <param name="package"></param>
        public void Unserialize(ZPackage package)
        {
            MinimumLevel = package.ReadInt();
            MaximumLevel = package.ReadInt();
            HuntPlayer = package.ReadBool();
            SpawnAtDay = package.ReadBool();
            SpawnAtNight = package.ReadBool();
            MinimumAltitude = package.ReadSingle();
            MaximumAltitude = package.ReadSingle();
            SpawnInterval = package.ReadSingle();
            SpawnChance = package.ReadSingle();
            SpawnRadiusMin = package.ReadSingle();
            SpawnRadiusMax = package.ReadSingle();
            RequiredGlobalKey = package.ReadNullableString();
            RequiredEnvironments = package.ReadNullableStringList();
            GroupSizeMin = package.ReadInt();
            GroupSizeMax = package.ReadInt();
            SpawnDistance = package.ReadSingle();
            MaxSpawned = package.ReadInt();
            LevelUpMinCenterDistance = package.ReadSingle();  
            Biomes = package.ReadNullableStringList();
            BiomeAreas = package.ReadNullableStringList();
        }
    }
}
