using MinimumRoute.Data;
using MinimumRoute.Model;
using MinimumRoute.ViewModel;
using System;
using System.Collections.Generic;

namespace MinimumRoute.Service
{
    public class RouteService
    {
        protected Context _context;
        protected CityService _cityService;

        public RouteService(Context context, CityService cityService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
        }

        public void CreateListRoutes(List<RouteViewModel> routes)
        {
            foreach (var route in routes)
            {
                var routeEntity = CreateRoute(route);
                _context.Routes.Add(routeEntity);
            }
        }

        public RouteEntity CreateRoute(RouteViewModel routeViewModel)
        {
            var cityOrigin = _cityService.FindByCode(routeViewModel.CityOrigin);
            var cityDestination = _cityService.FindByCode(routeViewModel.CityDestination);
            var distance = routeViewModel.Distance;

            var route = new RouteEntity(cityOrigin, cityDestination, distance);
            cityOrigin.RouteOrigin.Add(route);
            cityDestination.RouteDestination.Add(route);
            return route;
        }
    }
}
