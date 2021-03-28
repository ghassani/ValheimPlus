using System;
using System.Collections.Generic;
using UnityEngine;
using ValheimPlus.Utility;

namespace ValheimPlus
{
    [Serializable]
    public class CreatureSpawnerModifier : IZPackageable
    {
        public CreatureSpawnerModifierCondition Condition = new CreatureSpawnerModifierCondition();
        public CreatureSpawnerModifierModification Modifications = new CreatureSpawnerModifierModification();
        public int Priority = 0;

        public bool IsValid()
        {
            return Condition != null && Condition.IsValid();
        }

        public bool Matches(CreatureSpawner spawner)
        {
            Transform transform = spawner.GetComponent<Transform>();
            string biome = Heightmap.FindBiome(transform.position).ToString();
            int dayNumber = EnvMan.instance.GetCurrentDay();
            string normalizedName = GameObjectAssistant.NormalizeObjectName(spawner.name);

            if (!IsValid())                
            {
                Debug.Log("Skipping CreatureSpawner Mod: Invalid Modifier");
                return false;
            }

            if ( Condition.MatchingBiomes != null && Condition.MatchingBiomes.Count > 0)
            {
                if (!Condition.MatchingBiomes.Contains("*"))
                {
                    if (Condition.MatchingBiomes.Find(e => e.Equals(biome, StringComparison.OrdinalIgnoreCase)) == null)
                    {
                        Debug.Log($"Skipping CreatureSpawner Mod: Biome `{biome}` not found in {String.Join("", Condition.MatchingBiomes)}");
                        return false;
                    }
                }
            }

            if ( Condition.ExcludingBiomes != null && Condition.ExcludingBiomes.Count > 0)
            {
                if (Condition.ExcludingBiomes.Find(e => e.Equals(biome, StringComparison.OrdinalIgnoreCase)) != null)
                {
                    Debug.Log($"Skipping CreatureSpawner Mod: Biome `{biome}` in exclusion {String.Join("", Condition.ExcludingBiomes)}");
                    return false;
                }
            }

            if (Condition.MatchingNames.Count > 0)
            {
                if (!Condition.MatchingNames.Contains("*"))
                {
                    if (Condition.MatchingNames.Find(e => e.Equals(normalizedName, StringComparison.OrdinalIgnoreCase)) == null)
                    {
                        Debug.Log($"Skipping CreatureSpawner Mod: Name `{normalizedName}` not found in {String.Join("", Condition.MatchingNames)}");
                        return false;
                    }
                }
            }

            if (Condition.MatchingObjectNames.Count > 0)
            {
                if (!Condition.MatchingObjectNames.Contains("*"))
                {
                    if (Condition.MatchingObjectNames.Find(e => e.Equals(spawner.m_creaturePrefab.name, StringComparison.OrdinalIgnoreCase)) == null)
                    {
                        Debug.Log($"Skipping CreatureSpawner Mod: ObjectName `{spawner.m_creaturePrefab.name}` not found in match {String.Join("", Condition.MatchingObjectNames)}");
                        return false;
                    }
                }
            }

            if (Condition.ExcludingNames.Count > 0 && Condition.ExcludingNames.Find(e => e.Equals(normalizedName, StringComparison.OrdinalIgnoreCase)) != null)
            {
                Debug.Log($"Skipping CreatureSpawner Mod: Name `{normalizedName}` matched exclusion in {String.Join("", Condition.ExcludingNames)}");
                return false;
            }

            if (Condition.ExcludingObjectNames.Count > 0 && Condition.ExcludingObjectNames.Contains(spawner.m_creaturePrefab.name))
            {
                Debug.Log($"Skipping CreatureSpawner Mod: Object Name `{spawner.m_creaturePrefab.name}` matched exclusion in {String.Join("", Condition.ExcludingObjectNames)}");
                return false;
            }

            if ( Condition.MinimumDistanceFromCenter >= 0 && Condition.MaximumDistanceFromCenter > Condition.MinimumDistanceFromCenter )
            {
                if (!Helper.IsPositionMinMaxFromCenter(transform.position, Condition.MinimumDistanceFromCenter, Condition.MaximumDistanceFromCenter))
                {
                    Debug.Log($"Skipping CreatureSpawner Mod: Position From Center Min/Max {Condition.MinimumDistanceFromCenter}/{Condition.MaximumDistanceFromCenter} larger than magnitude {transform.position.magnitude}");

                    return false;
                }
            }
            else if (Condition.MinimumDistanceFromCenter >= 0 && Condition.MaximumDistanceFromCenter <= 0)
            {
                if ( !Helper.IsPositionMinFromCenter(transform.position, Condition.MinimumDistanceFromCenter)  )
                {
                    Debug.Log($"Skipping CreatureSpawner Mod: Position From Center Min/Max {Condition.MinimumDistanceFromCenter}/{Condition.MaximumDistanceFromCenter} larger than magnitude {transform.position.magnitude}");

                    return false;
                }
            }

            if (Condition.MinimumDayCount >= 0 && Condition.MaximumDayCount > Condition.MinimumDayCount)
            {
                if (dayNumber < Condition.MinimumDayCount || dayNumber > Condition.MaximumDayCount)
                {
                    Debug.Log($"Skipping CreatureSpawner Mod: Day {dayNumber} does not meet constraint {Condition.MinimumDayCount}/{Condition.MaximumDayCount}");

                    return false;
                }
            }
            else if (Condition.MinimumDistanceFromCenter >= 0 && Condition.MaximumDistanceFromCenter <= 0)
            {
                if (dayNumber < Condition.MinimumDayCount)
                {
                    Debug.Log($"Skipping CreatureSpawner Mod: Day {dayNumber} does not meet constraint {Condition.MinimumDayCount}");

                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Serialize the object into a ZPackage
        /// </summary>
        /// <param name="package"></param>
        public void Serialize(ZPackage package)
        {
            package.Write(Condition);
            package.Write(Modifications);
            package.Write(Priority);
        }
        
        /// <summary>
        /// Unserialize from a ZPackage into this object
        /// </summary>
        /// <param name="package"></param>
        public void Unserialize(ZPackage package)
        {           
            Condition = package.ReadPackageable<CreatureSpawnerModifierCondition>();
            Modifications = package.ReadPackageable<CreatureSpawnerModifierModification>();
            Priority = package.ReadInt();
        }
    }
}
