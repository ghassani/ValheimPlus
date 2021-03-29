using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ValheimPlus.Utility;

namespace ValheimPlus
{
    [Serializable]
    public class SpawnAreaDataModifierModification : IZPackageable
    {
        public string ObjectName = null;
        public int? MinLevel;
        public int? MaxLevel;
        public float? Weight;

        public void Apply(SpawnArea system, SpawnArea.SpawnData data)
        {
            GameObject go = null;

            if (ObjectName != null && ObjectName.Length > 0)
            {
                go = RecipeManager.instance.GetGameObject(ObjectName);

                if (go == null)
                {
                    Debug.LogWarning("");
                }
            }

            if ( MinLevel != null ) data.m_minLevel = MinLevel.Value;    
            if ( MaxLevel != null ) data.m_maxLevel = MaxLevel.Value;    
            if ( Weight != null )   data.m_weight   = Weight.Value;

            if ( go != null )
            {
                data.m_prefab = go;
            }
        }

        /// <summary>
        /// Serialize the object into a ZPackage
        /// </summary>
        /// <param name="package"></param>
        public void Serialize(ZPackage package)
        {
            package.WriteNullable(ObjectName);
            package.WriteNullable(MinLevel);
            package.WriteNullable(MaxLevel);
            package.WriteNullable(Weight);
        }
        
        /// <summary>
        /// Unserialize from a ZPackage into this object
        /// </summary>
        /// <param name="package"></param>
        public void Unserialize(ZPackage package)
        {
            ObjectName  = package.ReadNullableString();
            MinLevel    = package.ReadNullableInt();
            MaxLevel    = package.ReadNullableInt();
            Weight      = package.ReadNullableSingle();
        }
    }
}
