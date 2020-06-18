using MinimumRoute.Model;
using System.Collections.Generic;

namespace MinimumRoute.Builder
{
    public interface IBuilderListRoutes
    {
        List<RouteEntity> CreateListRoutes(List<string> rowsRoutes);
    }
}