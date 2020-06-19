using MinimumRoute.ViewModel;
using System.Collections.Generic;

namespace MinimumRoute.Service
{
    public interface IRouteService
    {
        void CreateListRoutes(List<RouteViewModel> routes);
    }
}