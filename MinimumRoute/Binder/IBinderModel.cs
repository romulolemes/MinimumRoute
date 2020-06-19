using MinimumRoute.Entity;
using System.Collections.Generic;

namespace MinimumRoute.Binder
{
    public interface IBinderModel
    {
        List<T> SerializeList<T>(string allText);
        T BindModel<T>(string line);
        string Deserialize(IEnumerable<PathEntity> pathEntities);
    }
}