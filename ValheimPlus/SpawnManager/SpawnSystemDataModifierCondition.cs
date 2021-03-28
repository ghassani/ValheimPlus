using System;
using System.Collections.Generic;
using ValheimPlus.Utility;

namespace ValheimPlus
{
    [Serializable]
    public class SpawnSystemDataModifierCondition : IZPackageable
    {
        public List<string> MatchingDataNames = new List<string>();
        public List<string> MatchingObjectNames = new List<string>();        
        public List<string> MatchingGlobalKeys = new List<string>();
        public List<string> ExcludingDataNames = new List<string>();
        public List<string> ExcludingObjectNames = new List<string>();
        public List<string> ExcludingGlobalKeys = new List<string>();
        public bool IsValid()
        {
            return  (MatchingDataNames != null && MatchingDataNames.Count > 0) || // valid for matching specific object names (i.e SpawnSystem,SpawnArea, or CreatureSpawner object name)
                    (MatchingObjectNames != null && MatchingObjectNames.Count > 0 ) || // valid for matching spawned object names
                    (MatchingGlobalKeys != null && MatchingGlobalKeys.Count > 0); // valid for matching data with specific global key requirement
        }
        
        /// <summary>
        /// Serialize the object into a ZPackage
        /// </summary>
        /// <param name="package"></param>
        public void Serialize(ZPackage package)
        {          
            package.Write(MatchingDataNames);
            package.Write(MatchingObjectNames);
            package.Write(MatchingGlobalKeys);
            package.Write(ExcludingDataNames);
            package.Write(ExcludingObjectNames);
            package.Write(ExcludingGlobalKeys);
        }
        
        /// <summary>
        /// Unserialize from a ZPackage into this object
        /// </summary>
        /// <param name="package"></param>
        public void Unserialize(ZPackage package)
        {
            MatchingDataNames = package.ReadStringList();
            MatchingObjectNames = package.ReadStringList();
            MatchingGlobalKeys = package.ReadStringList();
            ExcludingDataNames = package.ReadStringList();
            ExcludingObjectNames = package.ReadStringList();
            ExcludingGlobalKeys = package.ReadStringList();
        }
    }
}
