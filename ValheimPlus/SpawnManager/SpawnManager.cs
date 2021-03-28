using BepInEx;
using fastJSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ValheimPlus.Configurations;

namespace ValheimPlus
{
    public class SpawnManager
    {
        public SpawnConfig Config { get; internal set; } = new SpawnConfig();  
        public static SpawnManager instance { get; internal set; } = null;

        public SpawnManager()
        {

        }
        ~SpawnManager()
        {

        }
        /// <summary>
        /// Initializes the global instance
        /// </summary>
        public static void Initialize()
        {
            if (SpawnManager.instance == null)
                SpawnManager.instance = new SpawnManager();
        }

        /// <summary>
        /// DeInitializes the global instance
        /// </summary>
        public static void DeInitialize()
        {
            if (SpawnManager.instance != null)
                SpawnManager.instance = null;
        }

        /// <summary>
        /// Get a game object by its name from ObjectDB
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GameObject GetGameObject(String name)
        {
            return GameObjectAssistant.GetGameObjectPrefabCached(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromConfig"></param>
        /// <returns></returns>
        public SpawnSystem.SpawnData CreateSpawnSystemData(SpawnSystemDataAddition fromConfig)
        {
            SpawnSystem.SpawnData data = new SpawnSystem.SpawnData();

            data.m_name = fromConfig.Name;
            data.m_minLevel = fromConfig.MinimumLevel;
            data.m_maxLevel = fromConfig.MaximumLevel;
            data.m_huntPlayer = fromConfig.HuntPlayer;
            data.m_spawnAtDay = fromConfig.SpawnAtDay;
            data.m_spawnAtNight = fromConfig.SpawnAtNight;
            data.m_minAltitude = fromConfig.MinimumAltitude;
            data.m_maxAltitude = fromConfig.MaximumAltitude;
            data.m_spawnInterval = fromConfig.SpawnInterval;
            data.m_spawnChance = fromConfig.SpawnChance;
            data.m_spawnRadiusMin = fromConfig.SpawnRadiusMin;
            data.m_spawnRadiusMax = fromConfig.SpawnRadiusMax;
            data.m_requiredGlobalKey = fromConfig.RequiredGlobalKey;
            data.m_requiredEnvironments = fromConfig.RequiredEnvironments;
            data.m_groupSizeMin = fromConfig.GroupSizeMin;
            data.m_groupSizeMax = fromConfig.GroupSizeMax;
            data.m_spawnDistance = fromConfig.SpawnDistance;
            data.m_maxSpawned = fromConfig.MaxSpawned;
            data.m_levelUpMinCenterDistance = fromConfig.LevelUpMinCenterDistance;

            data.m_prefab = GetGameObject(fromConfig.ObjectName);

            if (data.m_prefab == null)
            {
                throw new Exception($"Error creating spawn system data. Object {fromConfig.ObjectName} not found");
            }
            
            Heightmap.Biome biome_flags = Heightmap.Biome.None;
            Heightmap.BiomeArea biome_area_flags = 0;

            if (fromConfig.Biomes.Contains("*"))
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
                foreach (string b in fromConfig.Biomes)
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
                    else
                    {
                        throw new Exception($"Error creating spawn system data. Biome {b} is invalid");
                    }
                }
            }
            

            data.m_biome     = biome_flags;

            if (fromConfig.BiomeAreas.Contains("*"))
            {
                biome_area_flags |= Heightmap.BiomeArea.Edge;
                biome_area_flags |= Heightmap.BiomeArea.Median;
                biome_area_flags |= Heightmap.BiomeArea.Everything;
            }
            else
            {
                foreach (string b in fromConfig.BiomeAreas)
                {
                    if (b.Equals(Heightmap.BiomeArea.Edge.ToString(), StringComparison.OrdinalIgnoreCase))              biome_area_flags |= Heightmap.BiomeArea.Edge;
                    else if (b.Equals(Heightmap.BiomeArea.Median.ToString(), StringComparison.OrdinalIgnoreCase))       biome_area_flags |= Heightmap.BiomeArea.Median;
                    else if (b.Equals(Heightmap.BiomeArea.Everything.ToString(), StringComparison.OrdinalIgnoreCase))   biome_area_flags |= Heightmap.BiomeArea.Everything;
                    else
                    {
                        throw new Exception($"Error creating spawn system data. BiomeArea {b} is invalid");
                    }
                }
            }
            
            data.m_biomeArea = biome_area_flags;

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromConfig"></param>
        /// <returns></returns>
        public SpawnArea.SpawnData CreateSpawnAreaData(SpawnAreaDataAddition fromConfig)
        {
            GameObject obj = GetGameObject(fromConfig.ObjectName);

            if (obj == null)
            {
                throw new Exception($"Object {fromConfig.ObjectName} not found");
            }

            SpawnArea.SpawnData result = new SpawnArea.SpawnData();

            result.m_prefab = obj;
            result.m_minLevel = fromConfig.MinLevel;
            result.m_maxLevel = fromConfig.MaxLevel;
            result.m_weight = fromConfig.Weight;

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="system"></param>
        public void SyncSpawnSystem(SpawnSystem system)
        {
            Debug.Log($"Syncing Spawn System [{system.GetInstanceID()}] @ {system.GetComponent<Transform>().position.ToString()}");

            // Apply Modifications
            foreach(SpawnSystemModifier modifier in Config.SpawnSystemModifiers)
            {                
                if (!modifier.Matches(system))
                {
                    continue;
                }

                //Debug.Log($"Matched Spawn System - Processing {modifier.SpawnModifiers.Count} spawn modifiers on {system.m_spawners.Count} spawn data instances");

                modifier.Modification.Apply(system);

                foreach(SpawnSystem.SpawnData data in system.m_spawners)
                {
                    foreach(SpawnSystemDataModifier dataModifier in modifier.SpawnModifiers)
                    {
                        
                        if (!dataModifier.Matches(system, data))
                        {
                            continue;
                        }

                        //Debug.Log($"Applied Modification To Data Name: `{data.m_name}` | Object Name: `{data.m_prefab.name}` | Required Key: `{data.m_requiredGlobalKey}`");

                        dataModifier.Modification.Apply(system, data);                       
                    }
                }

                // Add New
                foreach(SpawnSystemDataAddition addition in modifier.SpawnAdditions)
                {
                    try
                    {
                        system.m_spawners.Add(CreateSpawnSystemData(addition));
                        Debug.Log($"Added new data to SpawnSystem. New Count: {system.m_spawners.Count}");
                    } 
                    catch(Exception e)
                    {
                        Debug.Log($"Error adding new data to SpawnSystem: {e.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spawner"></param>
        public void SyncCreatureSpawner(CreatureSpawner spawner)
        {
            Debug.Log($"Syncing CreatureSpawner [{spawner.GetInstanceID()}] @ {spawner.GetComponent<Transform>().position.ToString()}");

            // Apply Modifications
            foreach (CreatureSpawnerModifier modifier in Config.CreatureSpawnerModifiers)
            {
                if (!modifier.Matches(spawner))
                {
                    continue;
                }

                Debug.Log($"Applying On CreatureSpawner {spawner.name} w/ {spawner.m_creaturePrefab.name}");

                modifier.Modifications.Apply(spawner);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        public void SyncSpawnArea(SpawnArea area)
        {
            Transform transform = area.GetComponent<Transform>();
            string currentBiome = Heightmap.FindBiome(transform.position).ToString();

            Debug.Log($"Syncing SpawnArea [{area.GetInstanceID()}] @ {transform.position.ToString()} - In Biome {currentBiome}");

            // Apply Modifications
            foreach (SpawnAreaModifier modifier in Config.SpawnAreaModifiers)
            {
                if (!modifier.Matches(area))
                {
                    continue;
                }

                modifier.Modification.Apply(area);

                foreach(SpawnArea.SpawnData data in area.m_prefabs)
                {
                    foreach(SpawnAreaDataModifier dataModifier in modifier.SpawnModifiers)
                    {
                        if (!dataModifier.Matches(area, data))
                        {
                            continue;
                        }
                        
                        Debug.Log($"Applying On AreaSpawner {area.name} w/ {data.m_prefab.name}");

                        dataModifier.Modification.Apply(area, data);
                    }
                }

                foreach(SpawnAreaDataAddition addition in modifier.SpawnAdditions)
                {
                    try
                    {
                        area.m_prefabs.Add(CreateSpawnAreaData(addition));
                        Debug.Log($"Added new data to SpawnArea. New Count: {area.m_prefabs.Count}");
                    }
                    catch(Exception e)
                    {
                        Debug.Log($"Error adding new data to SpawnArea: {e.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// Executed on game start
        /// </summary>
        public void OnGameStart()
        {
            if (ZNet.instance == null || (ZNet.instance.IsServerInstance() || ZNet.instance.IsLocalInstance()))
            {
                foreach (String _database in Configuration.Current.SpawnManager.databaseFile.Split(Path.PathSeparator))
                {
                    String database = _database.Trim();
                    String path = Path.Combine(Paths.ConfigPath, database);

                    if (Path.IsPathRooted(database))
                    {
                        Debug.Log($"Error loading spawn database @ {path} - Must be relative");
                        continue;
                    }

                    Debug.Log($"Loading Spawn Database: {path}");

                    try
                    {
                        String json = File.ReadAllText(path); 
               
                        JSONParameters parameters = new fastJSON.JSONParameters();
                        parameters.UseExtensions = false;
                        parameters.UseFastGuid = false;

                        UpdateFrom(JSON.ToObject<SpawnConfig>(json, parameters));
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log($"Error parsing json file `{path}` - {e.Message}");
                    }
                }
            }
        }

        public void UpdateFrom(SpawnConfig fromConfig)
        {
            Config = fromConfig;
            
            Debug.Log($"SpawnManager Config Loaded {Config.SpawnSystemModifiers.Count} System Modifiers");
            Debug.Log($"SpawnManager Config Loaded {Config.SpawnAreaModifiers.Count} Area Modifiers");
            Debug.Log($"SpawnManager Config Loaded {Config.CreatureSpawnerModifiers.Count} CreatureSpawner Modifiers");

            SortConfig();
        }

        protected void SortConfig()
        {
            // Sort by priority
            Config.SpawnSystemModifiers.Sort(delegate(SpawnSystemModifier a, SpawnSystemModifier b)
            {
                if (a.Priority == b.Priority)   return 0;
                return a.Priority > b.Priority ? 1 : -1;
            });
            
            Config.SpawnAreaModifiers.Sort(delegate(SpawnAreaModifier a, SpawnAreaModifier b)
            {
                if (a.Priority == b.Priority)   return 0;
                return a.Priority > b.Priority ? 1 : -1;
            });
            
            Config.CreatureSpawnerModifiers.Sort(delegate(CreatureSpawnerModifier a, CreatureSpawnerModifier b)
            {
                if (a.Priority == b.Priority)   return 0;
                return a.Priority > b.Priority ? 1 : -1;
            });
            
            // Sort by nested modifiers by priority

            foreach(SpawnSystemModifier modifier in Config.SpawnSystemModifiers)
            {
                modifier.SpawnModifiers.Sort(delegate(SpawnSystemDataModifier a, SpawnSystemDataModifier b)
                {
                    if (a.Priority == b.Priority)   return 0;
                    return a.Priority > b.Priority ? 1 : -1;
                });            
            }
            
            foreach(SpawnAreaModifier modifier in Config.SpawnAreaModifiers)
            {
                modifier.SpawnModifiers.Sort(delegate(SpawnAreaDataModifier a, SpawnAreaDataModifier b)
                {
                    if (a.Priority == b.Priority)   return 0;
                    return a.Priority > b.Priority ? 1 : -1;
                });            
            }
        }
    }
}
