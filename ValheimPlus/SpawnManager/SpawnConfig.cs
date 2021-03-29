using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using fastJSON;
using ValheimPlus.Utility;

namespace ValheimPlus
{
    /// <summary>
    /// SpawnSystemConfigEntry
    /// </summary>
    [Serializable]
    public class SpawnConfig : IZPackageable
    {
        public List<SpawnSystemModifier> SpawnSystemModifiers           = new List<SpawnSystemModifier>();
        public List<SpawnAreaModifier> SpawnAreaModifiers               = new List<SpawnAreaModifier>();
        public List<CreatureSpawnerModifier> CreatureSpawnerModifiers   = new List<CreatureSpawnerModifier>();

        /// <summary>
        /// Serialize the object into a ZPackage
        /// </summary>
        /// <param name="package"></param>
        public void Serialize(ZPackage package)
        {
            package.Write(SpawnSystemModifiers);
            package.Write(SpawnAreaModifiers);
            package.Write(CreatureSpawnerModifiers);
        }
        
        /// <summary>
        /// Unserialize from a ZPackage into this object
        /// </summary>
        /// <param name="package"></param>
        public void Unserialize(ZPackage package)
        {
            SpawnSystemModifiers        = package.ReadPackageableList<SpawnSystemModifier>();
            SpawnAreaModifiers          = package.ReadPackageableList<SpawnAreaModifier>();
            CreatureSpawnerModifiers    = package.ReadPackageableList<CreatureSpawnerModifier>();
        }

        /// <summary>
        /// Save the current configuration to json
        /// </summary>
        /// <param name="out_json_path"></param>
        /// <returns></returns>
        public bool SaveTo(String out_json_path)
        {
            try
            {
                JSONParameters parameters = new JSONParameters();
                parameters.UseExtensions = false;
                parameters.UseFastGuid = false;
   
                File.WriteAllText(out_json_path, JSON.ToNiceJSON(this, parameters));
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.Log($"Error parsing json: {e.Message}");
                return false;
            }

            return true;
        }
    }
}
