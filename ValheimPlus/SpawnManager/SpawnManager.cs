using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValheimPlus
{
    public class SpawnManager
    {
        protected SpawnConfig           Config = new SpawnConfig();
        public List<SpawnSystem>        SpawnSystems = new List<SpawnSystem>();
        public List<SpawnArea>          SpawnAreas = new List<SpawnArea>();
        public List<CreatureSpawner>    CreatureSpawners = new List<CreatureSpawner>();
        
        public SpawnManager()
        {

        }
        ~SpawnManager()
        {

        }
        
        public void AddSpawnSystem(SpawnSystem spawn_system)
        {
            if (ZNet.instance != null && ZNet.instance.IsServer())  return;

            if (!SpawnSystems.Contains(spawn_system))
            {
                SpawnSystems.Add(spawn_system);
                UnityEngine.Debug.Log("Added Spawn System");
                Config.Systems.Add(new SpawnSystemConfigEntry(spawn_system));
            }
        }
        public void AddSpawnArea(SpawnArea spawn_area)
        {
            if (ZNet.instance != null && ZNet.instance.IsServer())  return;
            
            if (!SpawnAreas.Contains(spawn_area))
            {
                UnityEngine.Debug.Log("Added Spawn Area");
                SpawnAreas.Add(spawn_area);
                Config.Areas.Add(new SpawnAreaConfigEntry(spawn_area));
            }
        }
        public void AddCreatureSpawner(CreatureSpawner creature_spawner)
        {
            if (ZNet.instance != null && ZNet.instance.IsServer())  return;
            
            if (!CreatureSpawners.Contains(creature_spawner))
            {
                UnityEngine.Debug.Log("Added Creature Spawner");
                CreatureSpawners.Add(creature_spawner);
                Config.CreatureSpawners.Add(new CreatureSpawnerConfigEntry(creature_spawner));
            }
        }
        public void OnGameStart()
        {

        }
        public void OnGameEnd()
        {

        }
    }
}
