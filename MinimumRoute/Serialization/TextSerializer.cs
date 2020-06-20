using MinimumRoute.Entity;
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

        public List<T> SerializeList<T>(string content)
        {
            string[] lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return lines.Select(line => SerializeObject<T>(line)).ToList();
        }

        public T SerializeObject<T>(string line)
        {
            var resultSplit = Regex.Split(line, SEPARATE_FILED_REGEX, RegexOptions.IgnoreCase).ToList();
            object instance = Activator.CreateInstance<T>();
            var propPublics = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance
                /*| BindingFlags.SetField | BindingFlags.SetProperty*/);

            foreach (var (propertyInfo, index) in propPublics.Select((x, i) => (x, i)))
            {
                string field = null;
                if (index < resultSplit.Count)
                    field = resultSplit.ElementAt(index);

                propertyInfo.SetValue(instance, Convert.ChangeType(field, propertyInfo.PropertyType));
            }

            return (T)instance;
        }

        public string Deserialize(IEnumerable<PathEntity> pathEntities)
        {
            StringBuilder textDeserialize = new StringBuilder();
            foreach (var path in pathEntities)
            {
                foreach (var city in path.CitiesVisit)
                {
                    textDeserialize.Append(DeserializeField(city.Code));
                }
                textDeserialize.Append(DeserializeField(path.Distance));
                textDeserialize.Append(Environment.NewLine);
            }
            return textDeserialize.ToString();
        }

        private string DeserializeField(object @object)
        {
            return @object.ToString() + SEPARATE_FIELD;
        }
    }
}
