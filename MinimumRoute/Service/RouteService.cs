using MinimumRoute.Model;
using MinimumRoute.Repository;
using MinimumRoute.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimumRoute.Service
{
    public class RouteService 
    {
        protected CityRepository _cityRepository;
        protected RouteRepository _routeRepository;

        public RouteService(CityRepository cityRepository, RouteRepository routeRepository)
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
