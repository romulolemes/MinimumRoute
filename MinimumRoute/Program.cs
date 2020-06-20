using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MinimumRoute.Data;
using MinimumRoute.Repository;
using MinimumRoute.Search;
using MinimumRoute.Search.Dijkstra;
using MinimumRoute.Serialization;
using MinimumRoute.Service;
using Serilog;
using Serilog.Extensions.Logging;
using System;

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

            try
            {
                Log.Information("Starting application");
                RegisterServices();

                var runner = _serviceProvider.GetService<Runner>();
                runner.Run();

                Log.Information("Finished application");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed");
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
                .AddSingleton<Runner>()
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
