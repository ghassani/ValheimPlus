using System;
using System.Collections.Generic;
using ValheimPlus.Utility;

namespace ValheimPlus
{
    [Serializable]
    public class SpawnAreaModifierCondition : IZPackageable
    {
        public List<String> MatchingNames = new List<String>();
        public List<string> MatchingBiomes = new List<string>();
        public List<String> ExcludingNames = new List<String>();
        public List<string> ExcludingBiomes = new List<string>();
        public float MinimumDistanceFromCenter = 0f;
        public float MaximumDistanceFromCenter = 0f;
        public int MinimumDayCount = 0;
        public int MaximumDayCount = 0;
   
        public bool IsValid()
        {
            return  (MatchingNames != null && MatchingNames.Count > 0) || // valid for matching specific object names (i.e SpawnSystem,SpawnArea, or CreatureSpawner object name)
                    (MatchingBiomes != null && MatchingBiomes.Count > 0) || // valid for matching specific biomes
                    (MinimumDistanceFromCenter > 0 && MaximumDistanceFromCenter > MinimumDistanceFromCenter) || // valid for matching min and max from center
                    (MinimumDistanceFromCenter >= 0 && MaximumDistanceFromCenter <= 0) || // valid for matching min from center
                    (MinimumDayCount >= 0 && MinimumDayCount <= MaximumDayCount); // valid for matching day min/max
        }

        /// <summary>
        /// Serialize the object into a ZPackage
        /// </summary>
        /// <param name="package"></param>
        public void Serialize(ZPackage package)
        {          
            package.Write(MatchingNames);
            package.Write(MatchingBiomes);
            package.Write(ExcludingNames);
            package.Write(ExcludingBiomes);
            package.Write(MinimumDistanceFromCenter);
            package.Write(MaximumDistanceFromCenter);
            package.Write(MinimumDayCount);
            package.Write(MaximumDayCount);
        }
        
        /// <summary>
        /// Unserialize from a ZPackage into this object
        /// </summary>
        /// <param name="package"></param>
        public void Unserialize(ZPackage package)
        {
            MatchingNames = package.ReadStringList();
            MatchingBiomes = package.ReadStringList();
            ExcludingNames = package.ReadStringList();
            ExcludingBiomes = package.ReadStringList();
            MinimumDistanceFromCenter = package.ReadSingle();
            MaximumDistanceFromCenter = package.ReadSingle();
            MinimumDayCount = package.ReadInt();
            MaximumDayCount = package.ReadInt();
        }
    }
}
