using Microsoft.VisualBasic;
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
        private const string SEPARATE_FIELD = @"[\s]+";

        public List<T> BindListModel<T>(string allText)
        {
            string[] lines = allText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return lines.Select(line => BindModel<T>(line)).ToList();
        }

        public T BindModel<T>(string line)
        {
            var type = typeof(T);
            var resultSplit = Regex.Split(line, SEPARATE_FIELD, RegexOptions.IgnoreCase).ToList();
            object instance = Activator.CreateInstance<T>();
            var propPublics = type.GetProperties(BindingFlags.Public | BindingFlags.Instance
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
    }
}
