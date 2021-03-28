using System;
using ValheimPlus.Utility;

namespace ValheimPlus
{
    [Serializable]
    public class SpawnAreaDataAddition : IZPackageable
    {
        public string ObjectName;
        public int MinLevel;
        public int MaxLevel;
        public float Weight;
        public float MinimumDistanceFromCenter = 0f;
        public float MaximumDistanceFromCenter = 0f;
        public int MinimumDayCount = 0;
        public int MaximumDayCount = 0;

        public SpawnAreaDataAddition()
        {

        }
        public SpawnAreaDataAddition(SpawnArea.SpawnData data)
        {
            ObjectName = data.m_prefab.name;
            MinLevel = data.m_minLevel;
            MaxLevel = data.m_maxLevel;
            Weight = data.m_weight;
        }

        /// <summary>
        /// Serialize the object into a ZPackage
        /// </summary>
        /// <param name="package"></param>
        public void Serialize(ZPackage package)
        {
            package.Write(ObjectName);
            package.Write(MinLevel);
            package.Write(MaxLevel);
            package.Write(Weight);
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
            ObjectName = package.ReadString();
            MinLevel = package.ReadInt();
            MaxLevel = package.ReadInt();
            Weight = package.ReadSingle();
            MinimumDistanceFromCenter = package.ReadSingle();
            MaximumDistanceFromCenter = package.ReadSingle();
            MinimumDayCount = package.ReadInt();
            MaximumDayCount = package.ReadInt();

        }
    }
}
