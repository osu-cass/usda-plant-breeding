using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Usda.PlantBreeding.Data.Util
{
    /// <summary>
    /// Helper class for caching serializers and reflection information to improve performance. Cache is stored in memory as static private members.  Thread safe.
    /// </summary>
    /// <remarks>Taken from Mulnomah falls.</remarks>
    public static class CacheManager
    {
        private static Dictionary<Type, XmlSerializer> m_serializerCache;
        private static object m_locker = new object();
        private static Dictionary<Type, PropertyInfo[]> m_reflectionCache;

        /// <summary>
        /// Gets the cached serializer for the type specified, or creates one if it is not yet cached.
        /// </summary>
        /// <param name="typeToSerialize">The type to serialize.</param>
        /// <returns>An XmlSerializer instance for the type specified.</returns>
        public static XmlSerializer GetSerializerForType(Type typeToSerialize)
        {
            XmlSerializer result = null;

            lock (m_locker)
            {
                if (m_serializerCache == null)
                {
                    m_serializerCache = new Dictionary<Type, XmlSerializer>();
                }

                if (m_serializerCache.ContainsKey(typeToSerialize))
                {
                    result = m_serializerCache[typeToSerialize];
                }
                else
                {
                    result = new XmlSerializer(typeToSerialize);
                    m_serializerCache.Add(typeToSerialize, result);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the type reflected properties for the specified type, including all private/public properties.
        /// </summary>
        /// <param name="typeToReflect">The type for which to get properties.</param>
        /// <returns>An array of PropertyInfo instances representing the properties of the specified type.</returns>
        public static PropertyInfo[] GetPropertyInfoForType(Type typeToReflect)
        {
            PropertyInfo[] result = null;

            lock (m_locker)
            {
                if (m_reflectionCache == null)
                {
                    m_reflectionCache = new Dictionary<Type, PropertyInfo[]>();
                }

                if (m_reflectionCache.ContainsKey(typeToReflect))
                {
                    result = m_reflectionCache[typeToReflect];
                }
                else
                {
                    result = typeToReflect.GetProperties();
                    m_reflectionCache.Add(typeToReflect, result);
                }
            }

            return result;
        }
    }
}
