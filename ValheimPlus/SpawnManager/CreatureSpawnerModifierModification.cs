using System;
using UnityEngine;
using ValheimPlus.Utility;

namespace ValheimPlus
{
    [Serializable]
    public class CreatureSpawnerModifierModification : IZPackageable
    {
        public string ObjectName = null;
        public bool? RequireSpawnArea;
        public bool? SpawnAtDay;
        public bool? SpawnAtNight;
        public bool? SpawnInPlayerBase;
        public float? TriggerDistance;
        public float? RespawnTimeMinutes;
        public float? LevelupChance;
        public int? MinLevel;
        public int? MaxLevel;
        public float? TriggerNoise;

        public void Apply(CreatureSpawner spawner)
        {
            GameObject go = null;
            
            if ( ObjectName != null && ObjectName.Length > 0 )
            {
                go = SpawnManager.instance.GetGameObject(ObjectName);

                if (go == null)
                {
                    Debug.LogWarning($"Could not find game object {ObjectName} - Not modifying existing game object");
                }
            }

            if (RequireSpawnArea != null )      spawner.m_requireSpawnArea  = RequireSpawnArea.Value;
            if (SpawnAtDay != null )            spawner.m_spawnAtDay        = SpawnAtDay.Value;
            if (SpawnAtNight != null )          spawner.m_spawnAtNight      = SpawnAtNight.Value;
            if (SpawnInPlayerBase != null )     spawner.m_spawnInPlayerBase = SpawnInPlayerBase.Value;
            if (TriggerDistance != null )       spawner.m_triggerDistance   = TriggerDistance.Value;
            if (RespawnTimeMinutes != null )    spawner.m_respawnTimeMinuts = RespawnTimeMinutes.Value;
            if (LevelupChance != null )         spawner.m_levelupChance     = LevelupChance.Value;
            if (MinLevel != null )              spawner.m_minLevel          = MinLevel.Value;
            if (MaxLevel != null )              spawner.m_maxLevel          = MaxLevel.Value;
            if (TriggerNoise != null )          spawner.m_triggerNoise      = TriggerNoise.Value;

            if (go != null)
            {
                spawner.m_creaturePrefab = go;
            }
        }

        /// <summary>
        /// Serialize the object into a ZPackage
        /// </summary>
        /// <param name="package"></param>
        public void Serialize(ZPackage package)
        {
            package.WriteNullableString(ObjectName);
            package.Write(RequireSpawnArea);
            package.Write(SpawnAtDay);
            package.Write(SpawnAtNight);
            package.Write(SpawnInPlayerBase);
            package.Write(TriggerDistance);
            package.Write(RespawnTimeMinutes);
            package.Write(LevelupChance);
            package.Write(MinLevel);
            package.Write(MaxLevel);
            package.Write(TriggerNoise);
        }
        
        /// <summary>
        /// Unserialize from a ZPackage into this object
        /// </summary>
        /// <param name="package"></param>
        public void Unserialize(ZPackage package)
        {
            ObjectName = package.ReadNullableString();
            RequireSpawnArea = package.ReadBool();
            SpawnAtDay = package.ReadBool();
            SpawnAtNight = package.ReadBool();
            SpawnInPlayerBase = package.ReadBool();
            TriggerDistance = package.ReadInt();
            RespawnTimeMinutes = package.ReadInt();
            LevelupChance = package.ReadInt();
            MinLevel = package.ReadInt();
            MaxLevel = package.ReadInt();
            TriggerNoise = package.ReadInt();
        }
    }
}
