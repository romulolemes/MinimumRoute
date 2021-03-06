﻿using MinimumRoute.Data;
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

        public IEnumerable<RouteEntity> CreateListRoutes(List<RouteViewModel> routes)
        {
            var routesCreated = new List<RouteEntity>();
            foreach (var route in routes)
            {
                routesCreated.Add(CreateRoute(route));
            }
            return routesCreated;
        }

        public RouteEntity CreateRoute(RouteViewModel routeViewModel)
        {
            var cityOrigin = _cityService.FindByCode(routeViewModel.CityOrigin);
            var cityDestination = _cityService.FindByCode(routeViewModel.CityDestination);
            var distance = routeViewModel.Distance;

            var routeEntity = new RouteEntity(cityOrigin, cityDestination, distance);
            _context.Routes.Add(routeEntity);
            return routeEntity;
        }
    }
}
