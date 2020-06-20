using Microsoft.Extensions.Logging;
using MinimumRoute.Model;
using MinimumRoute.Search;
using MinimumRoute.Search.Dijkstra;
using MinimumRoute.Serialization;
using MinimumRoute.Service;
using MinimumRoute.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimumRoute
{
    public class Runner
    {
        protected FileService _fileService;
        protected RouteService _routeService;
        protected IShortestPathFinder _shortestPathFinder;
        protected CityService _cityService;
        protected TextSerializer _textSerializer;
        protected ILogger _logger;

        public Runner(FileService fileService,
            RouteService routeService,
            IShortestPathFinder shortestPathFinder,
            CityService cityService,
            TextSerializer textSerializer,
            ILogger<Runner> logger)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _routeService = routeService ?? throw new ArgumentNullException(nameof(routeService));
            _shortestPathFinder = shortestPathFinder ?? throw new ArgumentNullException(nameof(shortestPathFinder));
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
            _textSerializer = textSerializer ?? throw new ArgumentNullException(nameof(textSerializer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public void Run()
        {
            List<RouteViewModel> routesViewModel = ReadAndSerializer<RouteViewModel>("./trechos.txt");
            List<OrderViewModel> ordersViewModel = ReadAndSerializer<OrderViewModel>("./encomendas.txt");

            var routes = _routeService.CreateListRoutes(routesViewModel);

            var paths = ordersViewModel.Select(order =>
            {
                var cityOrigin = _cityService.FindByCode(order.CityOrigin);
                var cityDestination = _cityService.FindByCode(order.CityDestination);
                return _shortestPathFinder.FindShortestPath(cityOrigin, cityDestination, s => GetNeighbors(s, routes));
            });

            var textPaths = _textSerializer.SerializeList(paths);
            _fileService.WriteAllText("./rotas.txt", textPaths);

            _logger.LogDebug("{paths}", textPaths);
        }

        private List<T> ReadAndSerializer<T>(string path)
        {
            string contentRoutes = _fileService.ReadAllText(path);
            return _textSerializer.DeserializeList<T>(contentRoutes);
        }

        public IEnumerable<NeighborhoodInfo> GetNeighbors(Node visitingNode, IEnumerable<RouteEntity> routes)
        {
            return routes.Where(r => r.CityOrigin.Equals(visitingNode))
                .Select(r => new NeighborhoodInfo(r.CityDestination, r.Distance));
        }
    }
}
