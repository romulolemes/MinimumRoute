using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MinimumRoute.Algoritmo;
using MinimumRoute.Binder;
using MinimumRoute.Data;
using MinimumRoute.Repository;
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
              .WriteTo.File("MinimumRoute.log")
              .WriteTo.Console()
              .MinimumLevel.Debug()
              .CreateLogger();

            RegisterServices();

            try
            {
                Log.Information("Starting application");
                var fileService = _serviceProvider.GetService<IFileService>();
                var binderModel = _serviceProvider.GetService<IBinderModel>();
                var routeService = _serviceProvider.GetService<IRouteService>();
                var context = _serviceProvider.GetService<IContext>();
                var finder = _serviceProvider.GetService<IShortestPathFinder>();
                var cityService = _serviceProvider.GetService<ICityService>();

                var streamRoutes = fileService.ReadFile("./trechos.txt");
                var streamOrders = fileService.ReadFile("./encomendas.txt");

                var routesViewModel = binderModel.BindListModel<RouteViewModel>(streamRoutes);
                var ordersViewModel = binderModel.BindListModel<OrderViewModel>(streamOrders);
                

                routeService.CreateListRoutes(routesViewModel);

                foreach (var order in ordersViewModel)
                {
                    var path = finder.FindShortestPath(cityService.FindByCode(order.CityOrigin), cityService.FindByCode(order.CityDestination));
                    Log.Information("Origin:{Origin} Destination:{Destination} Path:{@Path}", order.CityOrigin, order.CityDestination, path.Select(c => c.Code));
                }

                Log.Information("All done!");
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
                .AddSingleton<IContext, Context>()

                .AddTransient<ICityRepository, CityRepository>()
                .AddTransient<IRouteRepository, RouteRepository>()
                
                .AddTransient<IFileService, FileService>()
                .AddTransient<IRouteService, RouteService>()
                .AddTransient<ICityService, CityService>()
                
                .AddTransient<IFileSystem, FileSystem>()
                .AddTransient<IBinderModel, BinderModel>()
                .AddTransient<IShortestPathFinder, Dijkstra>()
                .BuildServiceProvider();
        }
    }
}
