using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MinimumRoute.Builder;
using MinimumRoute.Data;
using MinimumRoute.Repository;
using MinimumRoute.Service;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using System.IO;

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
                var builderRoute = _serviceProvider.GetService<IBuilderListRoutes>();

                var rowsRoutes = fileService.ReadFile("./trechos.txt");
                var routes = builderRoute.CreateListRoutes(rowsRoutes);

                

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
                .AddTransient<IFileService, FileService>()
                .AddTransient<IBuilderListRoutes, BuilderListRoutes>()
                .AddTransient<IFileSystem, FileSystem>()
                .BuildServiceProvider();
        }
    }
}
