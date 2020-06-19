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
              .WriteTo.File(@"Log\MinimumRoute.log")
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

                var routesViewModel = binderModel.SerializeList<RouteViewModel>(streamRoutes);
                var ordersViewModel = binderModel.SerializeList<OrderViewModel>(streamOrders);
                

                routeService.CreateListRoutes(routesViewModel);
                var paths = ordersViewModel.Select(order => finder.FindShortestPath(cityService.FindByCode(order.CityOrigin), cityService.FindByCode(order.CityDestination)));

                var textPaths = binderModel.Deserialize(paths);
                fileService.WriteFile("./rotas.txt", textPaths);

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
