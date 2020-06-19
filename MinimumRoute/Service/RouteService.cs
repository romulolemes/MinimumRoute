using MinimumRoute.Model;
using MinimumRoute.Repository;
using MinimumRoute.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimumRoute.Service
{
    public class RouteService : IRouteService
    {
        protected ICityRepository _cityRepository;
        protected IRouteRepository _routeRepository;

        public RouteService(ICityRepository cityRepository, IRouteRepository routeRepository)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _routeRepository = routeRepository ?? throw new ArgumentNullException(nameof(routeRepository));
        }

        public void CreateListRoutes(List<RouteViewModel> routes)
        {
            _routeRepository.AddRange(routes.Select(CreateRoutes));
        }

        private RouteEntity CreateRoutes(RouteViewModel routeViewModel)
        {
            var cityOrigin = _cityRepository.FindByCode(routeViewModel.CityOrigin);
            var cityDestination = _cityRepository.FindByCode(routeViewModel.CityDestination);
            var distance = routeViewModel.Distance;

            var route = new RouteEntity(cityOrigin, cityDestination, distance);
            cityOrigin.RouteOrigin.Add(route);
            cityDestination.RouteDestination.Add(route);
            return route;
        }
    }
}
