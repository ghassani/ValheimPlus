using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace ValheimPlus
{
    static class GameObjectAssistant
    {
        private static ConcurrentDictionary<float, Stopwatch> stopwatches = new ConcurrentDictionary<float, Stopwatch>();
        
        private static Dictionary<string, GameObject> PrefabObjectCache = new Dictionary<string, GameObject>();

        public static Stopwatch GetStopwatch(GameObject o)
        {
            float hash = GetGameObjectPosHash(o);
            Stopwatch stopwatch = null;

            if (!stopwatches.TryGetValue(hash, out stopwatch))
            {
                stopwatch = new Stopwatch();
                stopwatches.TryAdd(hash, stopwatch);
            }

            return stopwatch;
        }

        private static float GetGameObjectPosHash(GameObject o)
        {
            return (1000f * o.transform.position.x) + o.transform.position.y + (.001f * o.transform.position.z);
        }

        public static T GetChildComponentByName<T>(string name, GameObject objected) where T : Component
        {
            foreach (T component in objected.GetComponentsInChildren<T>(true))
            {
                if (component.gameObject.name == name)
                {
                    return component;
                }
            }
            return null;
        }

        /// <summary>
        /// Get a game object prefab by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject GetGameObjectPrefab(string name)
        {
            GameObject result = null;

            if ( ( result = ObjectDB.instance.GetItemPrefab(name) ) == null )
            {
                foreach(GameObject go in Resources.FindObjectsOfTypeAll<GameObject>())
                {
                    if (go.name == name)
                    {
                        return go;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get a game object by its name, with cached lookup
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject GetGameObjectPrefabCached(string name)
        {
            if (PrefabObjectCache.ContainsKey(name))
            {
                return PrefabObjectCache[name];
            }

            GameObject result = GetGameObjectPrefab(name);

            if (result != null)
            {
                PrefabObjectCache[name] = result;
            }

            return result;
        }

        static public string NormalizeObjectName(string name)
        {
            return name.Replace("(Clone)", "")
                .Replace("(clone)", "");
        }
    }
}
