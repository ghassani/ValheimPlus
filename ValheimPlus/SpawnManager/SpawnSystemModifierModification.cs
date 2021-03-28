using System;
using ValheimPlus.Utility;

namespace ValheimPlus
{
    [Serializable]
    public class SpawnSystemModifierModification : IZPackageable
    {
        public float? LevelUpChance;

        public SpawnSystemModifierModification()
        {

        }

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
            package.Write(LevelUpChance);
        }
        
        /// <summary>
        /// Unserialize from a ZPackage into this object
        /// </summary>
        /// <param name="package"></param>
        public void Unserialize(ZPackage package)
        {
            LevelUpChance = package.ReadSingle();
        }
    }
}
