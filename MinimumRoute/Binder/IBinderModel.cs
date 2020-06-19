using System.Collections.Generic;

namespace MinimumRoute.Binder
{
    public interface IBinderModel
    {
        List<T> BindListModel<T>(string allText);
        T BindModel<T>(string line);
    }
}