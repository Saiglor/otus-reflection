using System;
using System.Reflection;
using System.Text;

namespace Otus.Reflection.CsvSerializer
{
    public class CsvSerializer
    {
        /// <summary>
        /// Сериализация объекта в csv строку
        /// </summary>
        /// <param name="obj">Передаваемый объект</param>
        /// <param name="separator">Разделитель</param>
        public static string Serialize<T>(T obj, string separator) where T : class
        {
            var type = obj.GetType();

            var csv = new StringBuilder();

            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            var fieldInfos = type.GetFields(bindingFlags);
            csv.AppendLine(FieldsToCsv(fieldInfos, obj, separator));

            var propertyInfos = type.GetProperties(bindingFlags);
            csv.Append(PropertiesToCsv(propertyInfos, obj, separator));

            return csv.ToString();
        }
        
        private static string FieldsToCsv(FieldInfo[] fieldInfos, object obj, string separator)
        {
            var csv = new StringBuilder();

            for (var i = 0; i < fieldInfos.Length; i++)
            {
                var field = fieldInfos[i];
                csv.Append(field.Name)
                    .Append(':')
                    .Append(field.GetValue(obj));

                if (i < fieldInfos.Length - 1)
                    csv.Append(separator);
            }

            return csv.ToString();
        }

        private static string PropertiesToCsv(PropertyInfo[] propertyInfos, object obj, string separator)
        {
            var csv = new StringBuilder();

            for (var i = 0; i < propertyInfos.Length; i++)
            {
                var property = propertyInfos[i];
                csv.Append(property.Name)
                    .Append(':')
                    .Append(property.GetValue(obj));

                if (i < propertyInfos.Length - 1)
                    csv.Append(separator);
            }

            return csv.ToString();
        }

        /// <summary>
        /// Десериализация csv строки в объект
        /// </summary>
        public static void Deserialize()
        {
            throw new NotImplementedException();
        }
    }
}
