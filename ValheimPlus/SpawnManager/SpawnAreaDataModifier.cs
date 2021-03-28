using System;
using System.Collections.Generic;
using UnityEngine;
using ValheimPlus.Utility;

namespace ValheimPlus
{
    [Serializable]
    public class SpawnAreaDataModifier : IZPackageable
    {
        public SpawnAreaDataModifierCondition Condition = new SpawnAreaDataModifierCondition();
        public SpawnAreaDataModifierModification Modification = new SpawnAreaDataModifierModification();
        public int Priority = 0;

        public bool IsValid()
        {
            return Condition != null && Condition.IsValid();
        }

        public bool Matches(SpawnArea area, SpawnArea.SpawnData data)
        {
            if (!IsValid())
            {
                Debug.Log($"Skipping SpawnArea Data Mod: Invalid");
                return false;
            }

            if (Condition.MatchingObjectNames.Count > 0)
            {
                if (!Condition.MatchingObjectNames.Contains("*"))
                {
                    if (!Condition.MatchingObjectNames.Contains(data.m_prefab.name))
                    {
                        Debug.Log($"Skipping SpawnArea Data Mod: Name `{data.m_prefab.name}` not found in {String.Join("", Condition.MatchingObjectNames)}");
                        return false;
                    }
                }
            }

            if (Condition.ExcludingObjectNames.Count > 0 && Condition.ExcludingObjectNames.Contains(data.m_prefab.name))
            {
                Debug.Log($"Skipping SpawnArea Data Mod: Object Name `{data.m_prefab.name}` matched exclusion in {String.Join("", Condition.ExcludingObjectNames)}");
                return false;
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
            Condition = package.ReadPackageable<SpawnAreaDataModifierCondition>();
            Modification = package.ReadPackageable<SpawnAreaDataModifierModification>();
            Priority = package.ReadInt();
        }
    };
}
