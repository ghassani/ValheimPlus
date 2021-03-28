using System;
using System.Collections.Generic;
using ValheimPlus.Utility;
using UnityEngine;

namespace ValheimPlus
{
    [Serializable]
    public class SpawnSystemModifier : IZPackageable
    {
        public SpawnSystemModifierCondition     Condition       = new SpawnSystemModifierCondition();
        public SpawnSystemModifierModification  Modification    = new SpawnSystemModifierModification();
        public List<SpawnSystemDataModifier>    SpawnModifiers  = new List<SpawnSystemDataModifier>();
        public List<SpawnSystemDataAddition>    SpawnAdditions  = new List<SpawnSystemDataAddition>();
        public int Priority = 0;

        public bool IsValid()
        {
            return  Condition != null && Condition.IsValid();
        }

        public bool Matches(SpawnSystem system)
        {
            Transform transform = system.GetComponent<Transform>();
            string biome        = system.m_heightmap.GetBiome(transform.position).ToString();
            int dayNumber       = EnvMan.instance.GetCurrentDay();
            string normalizedName = GameObjectAssistant.NormalizeObjectName(system.name);

            if (!IsValid())
            {
                Debug.Log("Skipping SpawnSystem Mod: Invalid Modifier");
                return false;
            }

            if (Condition.MatchingNames.Count > 0)
            {
                if (!Condition.MatchingNames.Contains("*"))
                {
                    if (Condition.MatchingNames.Find(e => e.Equals(normalizedName, StringComparison.OrdinalIgnoreCase)) == null)
                    {
                        Debug.Log($"Skipping SpawnArea Mod: Name `{normalizedName}` not found in {String.Join("", Condition.MatchingNames)}");
                        return false;
                    }
                }
            }
            
            if (Condition.ExcludingNames.Count > 0 && Condition.ExcludingNames.Find(e => e.Equals(normalizedName, StringComparison.OrdinalIgnoreCase)) != null)
            {
                Debug.Log($"Skipping SpawnArea Mod: Name `{normalizedName}` matched exclusion in {String.Join("", Condition.ExcludingNames)}");
                return false;
            }

            if (Condition.MatchingBiomes != null && Condition.MatchingBiomes.Count > 0)
            {
                if (!Condition.MatchingBiomes.Contains("*"))
                {
                    if (Condition.MatchingBiomes.Find(e => e.Equals(biome, StringComparison.OrdinalIgnoreCase)) == null)
                    {
                        Debug.Log($"Skipping SpawnSystem Mod: Biome `{biome}` not found in {String.Join("", Condition.MatchingBiomes)}");
                        return false;
                    }
                }
            }

            if (Condition.ExcludingBiomes != null && Condition.ExcludingBiomes.Count > 0)
            {
                if (Condition.ExcludingBiomes.Find(e => e.Equals(biome, StringComparison.OrdinalIgnoreCase)) != null)
                {
                    Debug.Log($"Skipping SpawnSystem Mod: Biome `{biome}` in exclusion {String.Join("", Condition.ExcludingBiomes)}");
                    return false;
                }
            }

            if (Condition.MinimumDistanceFromCenter >= 0 && Condition.MaximumDistanceFromCenter > Condition.MinimumDistanceFromCenter )
            {
                if (!Helper.IsPositionMinMaxFromCenter(transform.position, Condition.MinimumDistanceFromCenter, Condition.MaximumDistanceFromCenter))
                {
                    Debug.Log($"Skipping SpawnSystem: Position From Center Min/Max {Condition.MinimumDistanceFromCenter}/{Condition.MaximumDistanceFromCenter} larger than magnitude {transform.position.magnitude}");
                    return false;
                }
            }
            else if (Condition.MinimumDistanceFromCenter >= 0 && Condition.MaximumDistanceFromCenter <= 0)
            {
                if ( !Helper.IsPositionMinFromCenter(transform.position, Condition.MinimumDistanceFromCenter)  )
                {
                    Debug.Log($"Skipping SpawnSystem: Position From Center Min {Condition.MinimumDistanceFromCenter} larger than magnitude {transform.position.magnitude}");
                    return false;
                }
            }

            if (Condition.MinimumDayCount >= 0 && Condition.MaximumDayCount > Condition.MinimumDayCount)
            {
                if (dayNumber < Condition.MinimumDayCount || dayNumber > Condition.MaximumDayCount)
                {
                    Debug.Log($"Skipping SpawnSystem: Day {dayNumber} does not meet constraint {Condition.MinimumDayCount}/{Condition.MaximumDayCount}");

                    return false;
                }
            }
            else if (Condition.MinimumDistanceFromCenter >= 0 && Condition.MaximumDistanceFromCenter <= 0)
            {
                if (dayNumber < Condition.MinimumDayCount)
                {
                    Debug.Log($"Skipping SpawnSystem: Day {dayNumber} does not meet constraint {Condition.MinimumDayCount}");
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
            package.Write(Modification);
            package.Write(SpawnModifiers);
            package.Write(Priority);
        }
        
        /// <summary>
        /// Unserialize from a ZPackage into this object
        /// </summary>
        /// <param name="package"></param>
        public void Unserialize(ZPackage package)
        {
            Condition = package.ReadPackageable<SpawnSystemModifierCondition>();
            Modification = package.ReadPackageable<SpawnSystemModifierModification>();
            SpawnModifiers = package.ReadPackageableList<SpawnSystemDataModifier>();
            Priority = package.ReadInt();
        }
    }
}
