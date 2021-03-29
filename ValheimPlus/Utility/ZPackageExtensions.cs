using System;
using UnityEngine;
using System.Collections.Generic;

using static ValheimPlus.VPlusDataObjects;

namespace ValheimPlus.Utility
{
    public static class ZPackageExtensions
    {
        public static MapRange ReadVPlusMapRange(this ZPackage pkg) => new MapRange()
        {
            StartingX = pkg.m_reader.ReadInt32(),
            EndingX = pkg.m_reader.ReadInt32(),
            Y = pkg.m_reader.ReadInt32()
        };

        public static void WriteVPlusMapRange(this ZPackage pkg, MapRange mapRange)
        {
            pkg.m_writer.Write(mapRange.StartingX);
            pkg.m_writer.Write(mapRange.EndingX);
            pkg.m_writer.Write(mapRange.Y);
        }

        /// <summary>
        /// Write a string list to a ZPackage
        /// </summary>
        /// <param name="pkg"></param>
        /// <param name="stringList"></param>
        public static void Write(this ZPackage pkg, List<string> stringList)
        {
            if ( stringList == null )
            {
                pkg.Write(0);
                return;
            }

            pkg.Write(stringList.Count);

            foreach (string s in stringList)
            {
                pkg.Write(s);
            }
        }
        public static void WriteNullable(this ZPackage pkg, List<string> stringList)
        {
            pkg.Write(stringList != null);

            if (stringList != null)
            {                
                Write(pkg, stringList);;
            }          
        }

        /// <summary>
        /// Read a StringList from a package
        /// </summary>
        /// <param name="pkg"></param>
        /// <returns></returns>
        public static List<string> ReadStringList(this ZPackage pkg)
        {
            List<string> result = new List<string>();
            int count = pkg.ReadInt();

            for (int i = 0; i < count; i++)
            {
                result.Add(pkg.ReadString());
            }

            return result;
        }
        public static List<string> ReadNullableStringList(this ZPackage pkg)
        {
            if (pkg.ReadBool())
            {
                return ReadStringList(pkg);
            }

            return null;
        }

        public static void WriteNullable(this ZPackage pkg, int? value)
        {
            pkg.Write(value.HasValue);
            
            if (value.HasValue)
            {
                pkg.Write(value.Value);
            }
        }
        public static int? ReadNullableInt(this ZPackage pkg)
        {
            if (pkg.ReadBool())
            {
                return pkg.ReadInt();
            }

            return null;
        }

        public static void WriteNullable(this ZPackage pkg, uint? value)
     {
            pkg.Write(value.HasValue);
            
            if (value.HasValue)
            {
                pkg.Write(value.Value);
            }
        }
        public static uint? ReadNullableUInt(this ZPackage pkg)
        {            
            if (pkg.ReadBool())
            {
                return pkg.ReadUInt();
            }

            return null;
        }

        public static void WriteNullable(this ZPackage pkg, long? value)
        {
            pkg.Write(value.HasValue);
            
            if (value.HasValue)
            {
                pkg.Write(value.Value);
            }
        }
        public static long? ReadNullableLong(this ZPackage pkg)
        {            
            if (pkg.ReadBool())
            {
                return pkg.ReadLong();
            }

            return null;
        }
        
        public static void WriteNullable(this ZPackage pkg, ulong? value)
        {
            pkg.Write(value.HasValue);
            
            if (value.HasValue)
            {
                pkg.Write(value.Value);
            }
        }
        public static ulong? ReadNullableULong(this ZPackage pkg)
        {            
            if (pkg.ReadBool())
            {
                return pkg.ReadULong();
            }

            return null;
        }

        public static void WriteNullable(this ZPackage pkg, float? value)
        {
            pkg.Write(value.HasValue);
            
            if (value.HasValue)
            {
                pkg.Write(value.Value);
            }
        }
        public static float? ReadNullableSingle(this ZPackage pkg)
        {           
            if (pkg.ReadBool())
            {
                return pkg.ReadSingle();
            }

            return null;
        }

        public static void WriteNullable(this ZPackage pkg, double? value)
        {
            pkg.Write(value.HasValue);
            
            if (value.HasValue)
            {
                pkg.Write(value.Value);
            }
        }
        
        public static double? ReadNullableDouble(this ZPackage pkg)
        {            
            if (pkg.ReadBool())
            {
                return pkg.ReadDouble();
            }

            return null;
        }
        public static void WriteNullable(this ZPackage pkg, bool? value)
        {
            pkg.Write(value.HasValue);
            
            if (value.HasValue)
            {
                pkg.Write(value.Value);
            }
        }
        
        public static bool? ReadNullableBool(this ZPackage pkg)
        {            
            if (pkg.ReadBool())
            {
                return pkg.ReadBool();
            }

            return null;
        }

        public static void WriteNullable(this ZPackage pkg, string value)
        {
            pkg.Write(value != null);
            
            if (value != null)
            {
                pkg.Write(value);
            }
        }
        
        public static string ReadNullableString(this ZPackage pkg)
        {            
            if (pkg.ReadBool())
            {
                return pkg.ReadString();
            }

            return null;
        }

        public static void Write<T>(this ZPackage pkg, T obj) where T : IZPackageable
        {
            obj.Serialize(pkg);
        }

        public static void Write<T>(this ZPackage pkg, List<T> objects) where T : IZPackageable
        {
            if ( objects == null )
            {
                pkg.Write(0);
                return;
            }

            pkg.Write(objects.Count);

            foreach(T obj in objects)
            {
                pkg.Write(obj);
            }
        }

        public static T ReadPackageable<T>(this ZPackage pkg) where T : IZPackageable, new()
        {
            T result = new T();
            
            result.Unserialize(pkg);

            return result;
        }

        public static List<T> ReadPackageableList<T>(this ZPackage pkg) where T : IZPackageable, new()
        {
            List<T> result = new List<T>();
            
            int count = pkg.ReadInt();

            for (int i = 0; i < count; i++)
            {
                result.Add(pkg.ReadPackageable<T>());
            }

            return result;
        }

        public static T ReadPackageable<T>(this ZPackage pkg, T into) where T : IZPackageable
        {
            into.Unserialize(pkg);

            return into;
        }
    }
}