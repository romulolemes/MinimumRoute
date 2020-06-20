using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace MinimumRoute.Serialization
{
    public class TextSerializer
    {
        private const string SEPARATE_FIELD = " ";
        private const string SEPARATE_FILED_REGEX = @"[\s]+";

        public List<T> DeserializeList<T>(string content)
        {
            string[] lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return lines.Select(line => DeserializeObject<T>(line)).ToList();
        }

        public T DeserializeObject<T>(string line)
        {
            var resultSplit = Regex.Split(line.Trim(), SEPARATE_FILED_REGEX, RegexOptions.IgnoreCase).ToList();
            object instance = Activator.CreateInstance<T>();
            var propPublics = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite);

            foreach (var (propertyInfo, index) in propPublics.Select((x, i) => (x, i)))
            {
                string field = null;
                if (index < resultSplit.Count)
                    field = resultSplit.ElementAt(index);

                propertyInfo.SetValue(instance, Convert.ChangeType(field, propertyInfo.PropertyType));
            }

            return (T)instance;
        }

        public string SerializeList<T>(IEnumerable<T> list)
        {
            return string.Join(Environment.NewLine, list.Select(SerializeObject));
        }

        public string SerializeObject<T>(T obj)
        {
            if (obj is ISerializer serializer)
            {
                return serializer.Serializer(SerializeField);
            }
            else
            {
                return SerializeAnyObject(obj);
            }
        }

        private string SerializeAnyObject<T>(T obj)
        {
            StringBuilder content = new StringBuilder();
            var propPublics = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead);

            foreach (var propertyInfo in propPublics)
            {
                content.Append(SerializeField(propertyInfo.GetValue(obj)));
            }

            return content.ToString().Trim();
        }

        private string SerializeField(object obj)
        {
            return obj?.ToString() + SEPARATE_FIELD;
        }
    }
}
