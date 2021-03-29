using System;
using ValheimPlus.Utility;

namespace ValheimPlus
{
    [Serializable]
    public class SpawnSystemModifierModification : IZPackageable
    {
        public float? LevelUpChance;

        public void Apply(SpawnSystem system)
        {
            if ( LevelUpChance != null ) system.m_levelupChance = LevelUpChance.Value;            
        }

        /// <summary>
        /// Serialize the object into a ZPackage
        /// </summary>
        /// <param name="package"></param>
        public void Serialize(ZPackage package)
        {
            package.WriteNullable(LevelUpChance);
        }
        
        /// <summary>
        /// Unserialize from a ZPackage into this object
        /// </summary>
        /// <param name="package"></param>
        public void Unserialize(ZPackage package)
        {
            LevelUpChance = package.ReadNullableSingle();
        }
    }
}
