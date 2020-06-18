using MinimumRoute.Model;
using System.Collections.Generic;

namespace MinimumRoute.Data
{
    public interface IContext
    {
        List<CityEntity> Cities { get; }
        List<RouteEntity> Routes { get; }
    }
}