using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MinimumRoute.Data;
using MinimumRoute.Repository;
using MinimumRoute.Search;
using MinimumRoute.Search.Dijkstra;
using MinimumRoute.Serialization;
using MinimumRoute.Service;
using MinimumRoute.ViewModel;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using System.Linq;

namespace MinimumRoute
{
    public class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
              .WriteTo.File(@"Log\MinimumRoute.log")
              .WriteTo.Console()
              .MinimumLevel.Debug()
              .CreateLogger();

            RegisterServices();

            try
            {
                Log.Information("Starting application");
                var fileService = _serviceProvider.GetService<FileService>();
                var routeService = _serviceProvider.GetService<RouteService>();
                var finder = _serviceProvider.GetService<IShortestPathFinder>();
                var cityService = _serviceProvider.GetService<CityService>();
                var textSerializer = _serviceProvider.GetService<TextSerializer>();

                var contentRoutes = fileService.ReadFile("./trechos.txt");
                var contentOrders = fileService.ReadFile("./encomendas.txt");

                var routesViewModel = textSerializer.SerializeList<RouteViewModel>(contentRoutes);
                var ordersViewModel = textSerializer.SerializeList<OrderViewModel>(contentOrders);
                
                routeService.CreateListRoutes(routesViewModel);
                var paths = ordersViewModel.Select(order => finder.FindShortestPath(cityService.FindByCode(order.CityOrigin), cityService.FindByCode(order.CityDestination)));

                var textPaths = textSerializer.DeserializeList(paths);
                fileService.WriteFile("./rotas.txt", textPaths);

                Log.Information("{paths}", textPaths);

                Log.Information("Finished application");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void RegisterServices()
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton(services => (ILoggerFactory)new SerilogLoggerFactory(Log.Logger, true))
                .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug)
                .AddSingleton<Context>()

                .AddTransient<CityRepository>()
                .AddTransient<RouteRepository>()
                
                .AddTransient<FileService>()
                .AddTransient<RouteService>()
                .AddTransient<CityService>()
                
                .AddTransient<FileSystem>()
                .AddTransient<TextSerializer>()
                .AddTransient<IShortestPathFinder, DijkstraAlgorithm>()
                .BuildServiceProvider();
        }
    }
}
