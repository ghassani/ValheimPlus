using System;
using System.Collections.Generic;
using ValheimPlus.Utility;

namespace ValheimPlus
{
    [Serializable]
    public class SpawnAreaDataModifierCondition : IZPackageable
    {
        public List<String> MatchingObjectNames = new List<String>();
        public List<String> ExcludingObjectNames = new List<String>();
   
        public bool IsValid()
        {
            return (MatchingObjectNames != null && MatchingObjectNames.Count > 0);
        }

        /// <summary>
        /// Serialize the object into a ZPackage
        /// </summary>
        /// <param name="package"></param>
        public void Serialize(ZPackage package)
        {
            package.Write(MatchingObjectNames);
            package.Write(ExcludingObjectNames);
        }
        
        /// <summary>
        /// Unserialize from a ZPackage into this object
        /// </summary>
        /// <param name="package"></param>
        public void Unserialize(ZPackage package)
        {
            MatchingObjectNames     = package.ReadStringList();
            ExcludingObjectNames    = package.ReadStringList();
        }
    }
}
