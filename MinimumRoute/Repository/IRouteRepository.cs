using MinimumRoute.Model;
using System.Collections.Generic;

namespace MinimumRoute.Repository
{
    public interface IRouteRepository
    {
        void AddRange(IEnumerable<RouteEntity> routes);
    }
}