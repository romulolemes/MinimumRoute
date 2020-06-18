using System.Collections.Generic;

namespace MinimumRoute.Binder
{
    public interface IBinderModel
    {
        T BindEntity<T>(string row);
        List<T> BindList<T>(List<string> rows);
    }
}