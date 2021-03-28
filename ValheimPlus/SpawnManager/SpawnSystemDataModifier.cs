using System;
using System.Collections.Generic;
using UnityEngine;
using ValheimPlus.Utility;

namespace ValheimPlus
{
    [Serializable]
    public class SpawnSystemDataModifier : IZPackageable
    {
        public SpawnSystemDataModifierCondition Condition = new SpawnSystemDataModifierCondition();
        public SpawnSystemDataModifierModification Modification = new SpawnSystemDataModifierModification();
        public int Priority = 0;

        public bool IsValid()
        {
            return  Condition != null && Condition.IsValid();
        }

        public bool Matches(SpawnSystem system, SpawnSystem.SpawnData data)
        {
            if (!IsValid())
            {
                Debug.Log($"Skipping SpawnSystem Data Mod: Invalid");
                return false;
            }

            if (Condition.MatchingDataNames.Count > 0)
            {
                if (!Condition.MatchingDataNames.Contains("*"))
                {
                    if (data.m_name.Length > 0 && !Condition.MatchingDataNames.Contains(data.m_name))
                    {
                        //Debug.Log($"Skipping SpawnSystem Data Mod: A");
                        return false;
                    }
                }
            }

            if (Condition.MatchingObjectNames.Count > 0)
            {
                if (!Condition.MatchingObjectNames.Contains("*"))
                {
                    if (!Condition.MatchingObjectNames.Contains(data.m_prefab.name))
                    {
                        //Debug.Log($"Skipping Spawn Data Mod: B");
                        return false;
                    }
                }
            }

            if (Condition.ExcludingDataNames.Count > 0 && data.m_name.Length > 0 && Condition.MatchingDataNames.Contains(data.m_name))
            {
                //Debug.Log($"Skipping Spawn Data Mod: C");
                return false;
            }

            if (Condition.ExcludingObjectNames.Count > 0 && Condition.ExcludingObjectNames.Contains(data.m_prefab.name))
            {
                //Debug.Log($"Skipping Spawn Data Mod: D");
                return false;
            }

            if (Condition.MatchingGlobalKeys.Count > 0)
            {
                if (data.m_requiredGlobalKey.Length > 0 && !Condition.MatchingGlobalKeys.Contains(data.m_requiredGlobalKey))
                {
                    //Debug.Log($"Skipping Spawn Data Mod: E");
                    return false;
                }
            }

            if (Condition.ExcludingGlobalKeys.Count > 0)
            {
                if (data.m_requiredGlobalKey.Length > 0 && Condition.MatchingGlobalKeys.Contains(data.m_requiredGlobalKey))
                {
                    //Debug.Log($"Skipping Spawn Data Mod: F");
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
            package.Write(Priority);
        }
        
        /// <summary>
        /// Unserialize from a ZPackage into this object
        /// </summary>
        /// <param name="package"></param>
        public void Unserialize(ZPackage package)
        {            
            Condition       = package.ReadPackageable<SpawnSystemDataModifierCondition>();
            Modification    = package.ReadPackageable<SpawnSystemDataModifierModification>();
            Priority        = package.ReadInt();
        }
    }
}
