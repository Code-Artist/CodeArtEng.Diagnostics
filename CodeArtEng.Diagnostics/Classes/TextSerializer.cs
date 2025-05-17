using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeArtEng.Diagnostics
{
    /// <summary>
    /// Interface for objects that can be serialized.
    /// </summary>
    public interface ISerializable
    {
        /// <summary>
        /// Serializes the object to string.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        string Serialize();
    }

    /// <summary>
    /// Serialize object into text.
    /// </summary>
    public class TextSerializer
    {
        /// <summary>
        /// Serializes an object's properties to a comma-separated string (CSV), properties name not included.
        /// </summary>
        /// <param name="obj">The object to serialize</param>
        /// <param name="excludedProperties">Optional array of property names to exclude from serialization</param>
        /// <returns>A comma-separated string of the object's property values</returns>
        public static string Serialize(object obj, params string[] excludedProperties)
        {
            if (obj == null)
                return string.Empty;

            HashSet<string> excludedPropertiesSet = new HashSet<string>(excludedProperties ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase);
            PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<string> values = new List<string>();

            foreach (PropertyInfo property in properties)
            {
                // Skip excluded properties
                if (excludedPropertiesSet.Contains(property.Name))
                    continue;

                object value = property.GetValue(obj);
                Type type = property.PropertyType;
                if (value == null) continue;

                if (type.IsPrimitive || type == typeof(string) || property.PropertyType.IsEnum)
                {
                    string v = value.ToString();
                    if (v.Contains(',')) v = $"\"{v}\"";
                    values.Add(v);
                }
                else if (type.IsArray)
                {
                    Type elementType = type.GetElementType();
                    if (elementType.IsPrimitive || elementType.IsEnum || elementType == typeof(string))
                    {
                        IList items = value as IList;
                        foreach (object i in items)
                            values.Add(i.ToString());
                    }
                }
                else if (type.IsGenericType)
                {
                    Type genericTypeDef = type.GetGenericTypeDefinition();
                    if (genericTypeDef == typeof(Dictionary<,>) ||
                       genericTypeDef == typeof(IDictionary<,>) ||
                       typeof(IDictionary).IsAssignableFrom(type)) continue;

                    Type elementType = type.GetGenericArguments()[0];
                    if (elementType.IsPrimitive || elementType.IsEnum || elementType == typeof(string))
                    {
                        IEnumerable items = value as IEnumerable;
                        foreach (object i in items)
                            values.Add(i.ToString());
                    }

                }
                else if (value is ISerializable serializable)
                {
                    values.Add(serializable.Serialize());
                }
            }
            return string.Join(",", values.Where(n => n.Length > 0));
        }
    }
}
