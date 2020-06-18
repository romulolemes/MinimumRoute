using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MinimumRoute.Binder
{
    public class BinderModel : IBinderModel
    {
        private const string SEPARATE = @"[\s]+";

        public List<T> BindList<T>(List<string> rows)
        {
            return rows.Select(BindEntity<T>).ToList();
        }

        public T BindEntity<T>(string row)
        {
            var resultSplit = Regex.Split(row, SEPARATE, RegexOptions.IgnoreCase).ToList();
            T instance = Activator.CreateInstance<T>();

            var propPublics = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var (propertyInfo, index) in propPublics.Select((x, i) => (x, i)))
            {
                propertyInfo.SetValue(instance, Convert.ChangeType(resultSplit.ElementAt(index), propertyInfo.PropertyType));
            }

            return instance;
        }
    }
}
